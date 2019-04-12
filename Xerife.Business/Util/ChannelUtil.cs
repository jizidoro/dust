using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xerife.Business.ChannelOperacaoWebService;
using Xerife.Business.ChannelProjetoWebService;
using Xerife.Business.ChannelApontamentoWebService;
using Xerife.Entities.Util;

namespace Xerife.Business.Util
{
    /// <summary>
    /// Métodos relacionados ao channel
    /// </summary>
    public static class ChannelUtil
    {
        private static ChannelProjetoWebServicePortTypeClient wsProjeto = new ChannelProjetoWebServicePortTypeClient();
        private static ChannelOperacaoWebServicePortTypeClient wsOperacao = new ChannelOperacaoWebServicePortTypeClient();
        private static ChannelApontamentoWebServicePortTypeClient wsApontamento = new ChannelApontamentoWebServicePortTypeClient();

        /// <summary>
        /// Lista de projetos ativos
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static List<DadosProjeto> ObterProjetosAtivos()
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            //Existe um bug no WebService, independente do usuário informado ele retorna todos os projetos
            //Caso queira filtrar os projetos por gerente o filtro deve ser feito em memória
            return wsOperacao.listarProjetosAtivos(string.Empty).ToList();
        }

        /// <summary>
        /// Lista as tarefas do projeto
        /// </summary>
        /// <param name="idProjeto"></param>
        /// <returns></returns>
        public static IEnumerable<DadosAtividade> GetTarefasProjeto(int idProjeto)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            foreach (DadosAtividade da in wsProjeto.listarAtividadesProjetos(idProjeto))
            {
                yield return da;
            }
        }

        public static string Apontamento(int idChannel, ApontamentoChannel a)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            try
            {
                return wsApontamento.registrarApontamento(idChannel, a.ValorHora, a.DataApontamento.Date, a.Colaborador, a.Descricao).mensagem;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Atividade {0}, Erro: {1}", idChannel, ex.Message));
            }
        }
    }
}