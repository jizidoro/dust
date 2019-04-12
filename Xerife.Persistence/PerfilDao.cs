using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;

namespace Xerife.Persistence
{
    /// <summary>
    /// Data Access Object de Perfil
    /// </summary>
    public class PerfilDao : BaseDao
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public PerfilDao() : base()
        {

        }

        public IEnumerable<Perfil> GetPerfis()
        {
            return db.PerfilSet;
        }
    }
}