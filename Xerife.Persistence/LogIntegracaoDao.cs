using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;
using Xerife.Entities.Util;

namespace Xerife.Persistence
{
    public class LogIntegracaoDao : BaseDao
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public LogIntegracaoDao() : base()
        {

        }
        public void RegistraLog(string projeto, string registro, bool erro)
        {
            LogIntegracao log = new LogIntegracao()
            {
                Data = DateTime.Now,
                Projeto = projeto,
                Registro = registro,
                Status = erro
            };
            db.LogIntegracaoSet.Add(log);
            db.SaveChanges();
        }


        /// <summary>
        /// Consulta historico da vpn
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<LogIntegracao> ConsultarLogIntegracao(LogIntegracaoFiltro filtro)
        {
            var logIntegracao = db.LogIntegracaoSet.ToList();

            if (filtro.Status == 0 || filtro.Status == 1)
            {
                logIntegracao = logIntegracao.Where(x => x.Status.Equals(Convert.ToBoolean(filtro.Status))).ToList();
            }
            if (!string.IsNullOrEmpty(filtro.Projeto))
            {
                logIntegracao = logIntegracao.Where(x => x.Projeto.Equals(filtro.Projeto)).ToList();
            }
            if (!string.IsNullOrEmpty(filtro.DataInicio))
            {
                logIntegracao = logIntegracao.Where(x => x.Data.Date >= Convert.ToDateTime(filtro.DataInicio).Date).ToList();
            }
            if (!string.IsNullOrEmpty(filtro.DataFim))
            {
                logIntegracao = logIntegracao.Where(x => x.Data.Date <= Convert.ToDateTime(filtro.DataFim).Date).ToList();
            }

            return logIntegracao;
        }
    }
}
