using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;
using Xerife.Persistence;

namespace Xerife.Business
{
    public class BaseBus
    {
        public BaseDao dao;

        public IEnumerable<BaseEntity> Consultar(Expression<Func<BaseEntity, bool>> filtro)
        {
            return dao.Consultar(filtro);
        }
    }
}
