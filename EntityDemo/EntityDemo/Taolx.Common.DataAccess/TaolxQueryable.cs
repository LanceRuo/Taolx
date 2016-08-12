using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess
{
    public class TaolxQueryable<TEntity> : IQueryable<TEntity>
    {
         
        internal IQueryable<TEntity> InternalQueryable { set; get; }

        Type IQueryable.ElementType
        {
            get
            {
                return InternalQueryable.ElementType;
            }
        }

        Expression IQueryable.Expression
        {
            get
            {
                return InternalQueryable.Expression;
            }
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return InternalQueryable.Provider;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InternalQueryable.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return InternalQueryable.GetEnumerator();
        }
    }
}
