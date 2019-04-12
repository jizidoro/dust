using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;
using Xerife.Persistence.Util;

namespace Xerife.Persistence
{
    /// <summary>
    /// Data Access Object de Projeto
    /// </summary>
    public class ProjetoDao : BaseDao
    {
        /// <summary>
        /// Data Access Object de Tarefa
        /// </summary>
        private TarefaChannelDao tarefaDao = new TarefaChannelDao();

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ProjetoDao() : base()
        {

        }

        /// <summary>
        /// Incluir projeto
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public bool IncluirProjeto(Projeto projeto)
        {
            try
            {
                db.ProjetoSet.Add(projeto);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Altera um projeto
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public bool AlterarProjeto(Projeto projeto)
        {
            try
            {
                var projetoBanco = db.ProjetoSet.FirstOrDefault(x => x.Id.Equals(projeto.Id));

                if (projetoBanco == null)
                {
                    return false;
                }

                var listaTarefa = tarefaDao.ListarTarefasProjeto(projetoBanco.Id);

                for (int i = 0; i < listaTarefa.Count; i++)
                {
                    var tarefaBanco = listaTarefa[i];
                    bool deletar = !projeto.TarefaChannel.Any(x => x.IdTarefaChannel.Equals(tarefaBanco.IdTarefaChannel));

                    if (deletar)
                    {
                        var tarefaDeletada = db.TarefaChannelSet.FirstOrDefault(x => x.Id == tarefaBanco.Id);

                        if (tarefaDeletada != null)
                        {
                            db.Entry(tarefaDeletada).State = EntityState.Deleted;
                            listaTarefa.Remove(tarefaBanco);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var tarefaModificada = db.TarefaChannelSet.FirstOrDefault(x => x.Id == tarefaBanco.Id);
                        var novaTarefa = projeto.TarefaChannel.FirstOrDefault(x => x.IdTarefaChannel == tarefaModificada.IdTarefaChannel);

                        if (tarefaModificada != null)
                        {
                            tarefaBanco.IdIterationTfs = tarefaModificada.IdIterationTfs = novaTarefa.IdIterationTfs;
                            tarefaBanco.IdTarefaChannel = tarefaModificada.IdTarefaChannel = novaTarefa.IdTarefaChannel;
                            db.Entry(tarefaModificada).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }

                var listaNovasTarefas = new List<TarefaChannel>();

                for (int i = 0; i < projeto.TarefaChannel.Count; i++)
                {
                    var tarefa = projeto.TarefaChannel.ToList()[i];

                    var existe = listaTarefa.Any(x => x.IdIterationTfs == tarefa.IdIterationTfs &&
                    x.IdTarefaChannel == tarefa.IdTarefaChannel);

                    if (!existe)
                    {
                        projetoBanco.TarefaChannel.Add(tarefa);
                    }
                }

                projetoBanco.IdChannel = projeto.IdChannel;
                projetoBanco.Nome = projeto.Nome;
                projetoBanco.Status = projeto.Status;
                projetoBanco.TfsAreaPath = projeto.TfsAreaPath;
                projetoBanco.TfsCollection = projeto.TfsCollection;
                projetoBanco.TfsProject = projeto.TfsProject;
                projetoBanco.TfsTeam = projeto.TfsTeam;
                projetoBanco.TfsUrl = projeto.TfsUrl;
                projetoBanco.UsuarioGerente = projeto.UsuarioGerente;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Excluir um projeto
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public bool ExcluirProjeto(Projeto projeto)
        {
            try
            {
                tarefaDao.DesassociarTarefasProjeto(projeto);

                db.ProjetoSet.Remove(projeto);

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Consulta os projetos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Projeto> ConsultarProjetos()
        {
            return db.ProjetoSet.OrderByDescending(x => x.Status).ThenBy(x => x.Nome);
        }

        /// <summary>
        /// Busca um projeto
        /// </summary>
        /// <param name="idProjeto"></param>
        /// <returns></returns>
        public Projeto GetProjeto(int idProjeto)
        {
            return db.ProjetoSet.FirstOrDefault(x => x.Id.Equals(idProjeto));
        }

        public void AlterarStatusProjeto(int idProjeto)
        {
            var pro = db.ProjetoSet.FirstOrDefault(x => x.Id.Equals(idProjeto));
            pro.Status = !pro.Status;

            db.SaveChanges();
        }
    }
}