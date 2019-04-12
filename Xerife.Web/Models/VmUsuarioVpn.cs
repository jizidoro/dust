using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xerife.Web.Models
{
    public class VmUsuarioVpn
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string DataFim { get; set; }
        public string DataInicio { get; set; }
    }
}