using EntityFramework.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess
{
    public static class BatchExtensions
    {
     
        public static int Delete<TEntity>(this IQueryable<TEntity> source) where TEntity : class
        {
            //ObjectQuery<TEntity> sourceQuery = source.ToObjectQuery();
            //if (sourceQuery == null)
            //    throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");

            //ObjectContext objectContext = sourceQuery.Context;
            //if (objectContext == null)
            //    throw new ArgumentException("The ObjectContext for the source query can not be null.", "source");

            //EntityMap entityMap = sourceQuery.GetEntityMap<TEntity>();
            //if (entityMap == null)
            //    throw new ArgumentException("Could not load the entity mapping information for the query ObjectSet.", "source");

            // var runner = ResolveRunner();
            //   return runner.Delete(objectContext, entityMap, sourceQuery);
            return 0;
        }
    }
}
