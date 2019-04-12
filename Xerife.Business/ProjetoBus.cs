using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xerife.Business.Util;
using Xerife.Entities;
using Xerife.Entities.Enum;
using Xerife.Entities.Util;
using Xerife.Persistence;

namespace Xerife.Business
{
    /// <summary>
    /// Ações do projeto
    /// </summary>
    public class ProjetoBus
    {
        /// <summary>
        /// Data Access Object de Projeto
        /// </summary>
        private ProjetoDao projetoDao = new ProjetoDao();

        /// <summary>
        /// Data Access Object de Tarefa
        /// </summary>
        private TarefaChannelDao tarefaDao = new TarefaChannelDao();

        /// <summary>
        /// Inclui um novo projeto
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public bool Incluir(Projeto projeto)
        {
            return projetoDao.IncluirProjeto(projeto);
        }

        /// <summary>
        /// Altera um projeto
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public bool Alterar(Projeto projeto)
        {
            return projetoDao.AlterarProjeto(projeto);
        }

        /// <summary>
        /// Consulta os projetos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Projeto> ConsultarProjetos()
        {
            return projetoDao.ConsultarProjetos();
        }

        /// <summary>
        /// Busca um projeto
        /// </summary>
        /// <param name="idProjeto"></param>
        /// <returns></returns>
        public Projeto GetProjeto(int idProjeto)
        {
            return projetoDao.GetProjeto(idProjeto);
        }

        public void AlterarStatusProjeto(int idProjeto)
        {
            projetoDao.AlterarStatusProjeto(idProjeto);
        }

        /// <summary>
        /// Exclui um projeto
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public bool ExcluirProjeto(Projeto projeto)
        {
            return projetoDao.ExcluirProjeto(projeto);
        }

        /// <summary>
        /// Lista as tarefas associadas a um projeto
        /// </summary>
        /// <param name="idProjeto"></param>
        /// <returns></returns>
        public List<TarefaChannel> ListarTarefasDoProjeto(int idProjeto)
        {
            return tarefaDao.ListarTarefasProjeto(idProjeto);
        }

        /// <summary>
        /// Desfaz a associação de projeto e tarefas
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public bool DesassociarTarefas(Projeto projeto)
        {
            return tarefaDao.DesassociarTarefasProjeto(projeto);
        }

        /// <summary>
        /// Lista todos os projetos
        /// </summary>
        /// <returns></returns>
        public List<Projeto> ListarProjetos()
        {
            return projetoDao.ConsultarProjetos().ToList();
        }

        public bool ExecutarIntegracaoChannelTfs(ModoExecucaoIntegracao modoExecucao = ModoExecucaoIntegracao.Automatica, int idProjeto = 0)
        {
            bool retorno = true;
            IEnumerable<Projeto> projetos;
            var pb = new ProjetoBus();
            var logDao = new LogIntegracaoDao();

            if (idProjeto > 0)
            {
                projetos = new[] { pb.GetProjeto(idProjeto) };
            }
            else
            {
                projetos = pb.ConsultarProjetos().Where(x => x.Status == true);
            }

            foreach (var p in projetos)
            {
                try
                {
                    if (modoExecucao.Equals(ModoExecucaoIntegracao.Manual))
                    {
                        logDao.RegistraLog(p.Nome, "Iniciando integração " + modoExecucao.ToString(), false);
                    }
                    var tf = pb.ListarTarefasDoProjeto(p.Id);
                    foreach (var i in tf)
                    {
                        if (modoExecucao.Equals(ModoExecucaoIntegracao.Manual))
                        {
                            logDao.RegistraLog(p.Nome, "Iteração " + i.IdIterationTfs, false);
                        }
                        var workItemIdList = TfsUtil.GetWorkItemFromIteration(p.TfsUrl, p.TfsCollection, p.TfsProject, i.IdIterationTfs).workItems;
                        foreach (var workItemId in workItemIdList)
                        {
                            var workItem = TfsUtil.GetWorkItem(p.TfsUrl, p.TfsCollection, workItemId.id);

                            string strApontamento = (workItem.fields.TimesheetRawData);
                            strApontamento = TratarXML(strApontamento);

                            XDocument doc = XDocument.Parse(strApontamento);
                            IEnumerable<XElement> elList = doc.Descendants("TimeSheetEntry");

                            if (elList.Attributes("ApropriadoChannel").Count() > 0)
                            {
                                foreach (XElement el in elList.Where(x => x.Attribute("ApropriadoChannel").Value.Equals("false")))
                                {
                                    var apontamento = new ApontamentoChannel()
                                    {
                                        Colaborador = el.Attribute("CreatedBy").Value,
                                        DataApontamento = Convert.ToDateTime(el.Attribute("TimeSheetDate").Value),
                                        DataCriacao = Convert.ToDateTime(el.Attribute("CreatedDate").Value),
                                        Descricao = workItem.id.ToString() + " - " + workItem.fields.Title + " - " + el.Attribute("Comments").Value,
                                        ValorHora = el.Attribute("Minutes").Value
                                    };
                                    var retornoApontamento = ChannelUtil.Apontamento(i.IdTarefaChannel, apontamento);
                                    if (!retornoApontamento.Contains("ERRO"))
                                    {
                                        if (modoExecucao.Equals(ModoExecucaoIntegracao.Manual))
                                        {
                                            logDao.RegistraLog(p.Nome, "Apontamento de " + apontamento.Colaborador + " no dia " + apontamento.DataApontamento + " de " + apontamento.ValorHora + " horas realizado com " + retornoApontamento, false);
                                        }
                                        doc.Descendants("TimeSheetEntry").Attributes("ApropriadoChannel").ElementAt(el.ElementsBeforeSelf().Count()).Value = "true";

                                        var workItemFieldUpdateList = new WorkItemFieldsUpdate[1];
                                        var workItemFieldUpdate = new WorkItemFieldsUpdate()
                                        {
                                            op = "replace",
                                            path = "/fields/Custom.Timesheets.TimesheetRawData",
                                            value = doc.ToString()
                                        };
                                        workItemFieldUpdateList[0] = workItemFieldUpdate;

                                        var updateResult = TfsUtil.UpdateWorkItem(p.TfsUrl, p.TfsCollection, workItemFieldUpdateList, workItem.id);
                                    }
                                    else
                                    {
                                        retorno = false;
                                        logDao.RegistraLog(p.Nome, "Apontamento de " + apontamento.Colaborador + " no dia " + apontamento.DataApontamento + " de " + apontamento.ValorHora + " horas realizado com " + retornoApontamento, true);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logDao.RegistraLog(p.Nome, "Erro ao executar integração " + ex.Message, true);
                    retorno = false;
                }
            }
            return retorno;
        }
        private static string TratarXML(string sourceXml)
        {
            var xml = sourceXml;
            // Obtém cada registro TimeSheetEntry
            var pattern = "(?=<TimeSheetEntry [\\w\\W]+Comments=\").*?(?=\" />)";
            foreach (Match itemMatch in Regex.Matches(sourceXml, pattern))
            {
                var item = itemMatch.Groups[0];
                var code = string.Concat("$", itemMatch.Index);
                xml = xml.Replace(item.Value, code);

                // Separa em dois grupos, o registro e o texto do comentário
                pattern = "(<[\\w\\W]+Comments=\")(.+)";
                var match = Regex.Match(item.Value, pattern);

                var comments = match.Groups[2].Value;

                // Substitui os caracteres especiais pelo encode correspondente
                comments = comments.Replace("&", "");
                comments = comments.Replace("<", "");
                comments = comments.Replace(">", "");
                comments = comments.Replace("\"", "");
                comments = comments.Replace("'", "");

                var timeSheetEntry = Regex.Replace(item.Value, pattern, string.Concat("$1", comments));
                xml = xml.Replace(code, timeSheetEntry);
            }

            return xml;
        }
    }
}