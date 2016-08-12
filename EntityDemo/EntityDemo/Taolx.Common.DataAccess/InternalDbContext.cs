using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess
{
    internal class InternalDbContext : DbContext
    {
        private Func<List<Type>> _getAllEntityTypes;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="existingConnection"></param>
        /// <param name="contextOwnsConnection"></param>
        public InternalDbContext(Func<List<Type>> getAllEntityTypes, DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
            _getAllEntityTypes = getAllEntityTypes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var types = _getAllEntityTypes();
            types.ForEach(type => modelBuilder.RegisterEntityType(type));
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taolxDbContextType"></param>
        /// <param name="getAllEntityTypes"></param>
        /// <param name="existingConnection"></param>
        /// <param name="contextOwnsConnection"></param>
        /// <returns></returns>
        internal static InternalDbContext Create(Type taolxDbContextType, Func<List<Type>> getAllEntityTypes, DbConnection existingConnection, bool contextOwnsConnection)
        {
            Type t = typeof(InternalDbContext);
            var method = t.GetMethod("CreateByTTaolxDbContext", BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo mi = method.MakeGenericMethod(taolxDbContextType);//加载泛型参数
            var obj = mi.Invoke(t, new object[] { getAllEntityTypes, existingConnection, contextOwnsConnection });
            return (InternalDbContext)obj;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getModelType"></param>
        /// <param name="existingConnection"></param>
        /// <param name="contextOwnsConnection"></param>
        /// <returns></returns>
        private static InternalDbContext CreateByTTaolxDbContext<TTaolxDbContext>(Func<List<Type>> getAllEntityTypes, DbConnection existingConnection, bool contextOwnsConnection)
              where TTaolxDbContext : TaolxDbContext
        {
            return new InternalDbContext<TTaolxDbContext>(getAllEntityTypes, existingConnection, contextOwnsConnection);
        }
    }
}
