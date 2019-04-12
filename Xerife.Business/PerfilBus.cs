using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;
using Xerife.Persistence;

namespace Xerife.Business
{
    public class PerfilBus
    {
        PerfilDao perfilDao = new PerfilDao();
        public IEnumerable<Perfil> GetPerfis()
        {
            return perfilDao.GetPerfis();
        }
    }
}
