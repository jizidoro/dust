using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;
using Xerife.Persistence;
using Xerife.Entities.Util;

namespace Xerife.Business
{
    /// <summary>
    /// Data Access Object de Historico da vpn
    /// </summary>
    public class LogIntegracaoBus
    {
        /// <summary>
        /// Data Access Object de Historico
        /// </summary>
        public LogIntegracaoDao dao = new LogIntegracaoDao();

        /// <summary>
        /// Consultar historicos de vpns
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<LogIntegracao> ConsultarLogIntegracao(LogIntegracaoFiltro filtro)
        {
            return dao.ConsultarLogIntegracao(filtro);
        }
    }
}