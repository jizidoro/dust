using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xerife.Web.Models
{
    public class VmParametroSistema
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
        public string Sigla { get; set; }

        public string NomePerfil { get; set; }
    }
}