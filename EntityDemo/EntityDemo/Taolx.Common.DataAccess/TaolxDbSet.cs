using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess
{

    /// <summary>
    /// TaolxDbSet
    /// </summary>
    internal class TaolxDbSet
    {
        internal static object Create(Type entityType, TaolxDbContext taolxDbContext)
        {
            Type t = typeof(TaolxDbSet);
            var method = t.GetMethod("CreateByTEntity", BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo mi = method.MakeGenericMethod(entityType);//加载泛型参数
            var obj = mi.Invoke(t, new object[] { taolxDbContext });
            return obj;
        }

        /// <summary>
        /// 创建TaolxDbSet实例
        /// </summary>
        /// <param name="taolxDbContext"></param>
        /// <returns></returns>
        private static TaolxDbSet<TEntity> CreateByTEntity<TEntity>(TaolxDbContext taolxDbContext) where TEntity : class
        {
            return new TaolxDbSet<TEntity>(taolxDbContext);
        }
    }
}
