using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;
using Xerife.Entities.Util;

namespace Xerife.Persistence
{
    /// <summary>
    /// Data Access Object de VpnHistorico
    /// </summary>
    public class VpnHistoricoDao
    {
        /// <summary>
        /// Database
        /// </summary>
        private Model1Container db = new Model1Container();

        /// <summary>
        /// Consulta historico da vpn
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<VpnHistorico> ConsultarVpnHistorico(HistoricoVpnFiltro filtro)
        {
            var vpnHistorico = db.VpnHistoricoSet.ToList();
            if (!string.IsNullOrEmpty(filtro.Usuario))
            {
                vpnHistorico = vpnHistorico.Where(x => x.Usuario.Equals(filtro.Usuario)).ToList();
            }
            if (!string.IsNullOrEmpty(filtro.Responsavel))
            {
                vpnHistorico = vpnHistorico.Where(x => x.Responsavel.Equals(filtro.Responsavel)).ToList();
            }
            if (!string.IsNullOrEmpty(filtro.DataInicio))
            {
                vpnHistorico = vpnHistorico.Where(x => x.Inicio.HasValue && x.Inicio.Value >= Convert.ToDateTime(filtro.DataInicio)).ToList();
            }
            if (!string.IsNullOrEmpty(filtro.DataFim))
            {
                vpnHistorico = vpnHistorico.Where(x => x.Fim.HasValue && x.Inicio.Value <= Convert.ToDateTime(filtro.DataFim)).ToList();
            }

            return vpnHistorico;
        }
    }
}