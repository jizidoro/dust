using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xerife.Web.Models
{
    public class VmLogIntegracao
    {
        public string Projeto { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Data de Inicio
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// Data Final
        /// </summary>
        public string Registro { get; set; }
    }
}