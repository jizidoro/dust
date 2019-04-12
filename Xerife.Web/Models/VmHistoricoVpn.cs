using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xerife.Web.Models
{
    /// <summary>
    /// ViewModel do historico de vpn
    /// </summary>
    public class VmHistoricoVpn
    {
        /// <summary>
        /// ID do historico
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Data de Inicio
        /// </summary>
        public string Inicio { get; set; }
        /// <summary>
        /// Data Final
        /// </summary>
        public string Fim { get; set; }
        /// <summary>
        /// Data em que a ação foi realizada
        /// </summary>
        public string DataAcao { get; set; }
        /// <summary>
        /// Ação realizada
        /// </summary>
        public string Acao { get; set; }
        /// <summary>
        /// Responsavel
        /// </summary>
        public string Responsavel { get; set; }
        /// <summary>
        /// Usuario
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Justificativa
        /// </summary>
        public string Justificativa { get; set; }
    }
}