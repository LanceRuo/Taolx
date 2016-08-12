using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess
{
    public static class TaolxQueryableExtended
    {

        public static int Delete<TEntity>(this TaolxQueryable<TEntity> source, IQueryable<TEntity> query)
        {
            return 0;
        }
    }
}
