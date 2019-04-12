using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xerife.Web.Models
{
    public class VmUsuario
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public int IdPerfil { get; set; }

        public string NomePerfil { get; set; }
    }
}