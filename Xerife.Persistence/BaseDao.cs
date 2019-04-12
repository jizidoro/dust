using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;

namespace Xerife.Persistence
{
    /// <summary>
    /// Data Access Object de Base
    /// </summary>
    public class BaseDao
    {
        /// <summary>
        /// Database
        /// </summary>
        public Model1Container db;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public BaseDao()
        {
            db = new Model1Container();
            db.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Consulta historicos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IEnumerable<BaseEntity> Consultar(Expression<Func<BaseEntity, bool>> filtro)
        {
            return db.Set<BaseEntity>().Where(filtro);
        }
    }
}