using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xerife.Entities.Util
{
    public class HistoricoVpnFiltro
    {
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Usuario { get; set; }
        public string Responsavel { get; set; }
    }
    public class LogIntegracaoFiltro
    {
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public int Status { get; set; }
        public string Projeto { get; set; }
    }
}