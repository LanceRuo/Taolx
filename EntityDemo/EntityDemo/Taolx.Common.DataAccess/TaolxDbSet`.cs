using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess;

namespace Taolx.Common.DataAccess
{

    /// <summary>
    /// 淘旅行dbSet
    /// </summary>
    /// <typeparam name="TEntity">实体类</typeparam>
    public class TaolxDbSet<TEntity> : TaolxQueryable<TEntity>, ITaolxDbSet<TEntity> where TEntity : class
    {
        /// <summary>
        /// TEntityType
        /// </summary>
        internal static Type TEntityType { get { return typeof(TEntity); } }

        /// <summary>
        /// EntityFramework read dbset 
        /// </summary>
        internal DbSet<TEntity> ReadDbSet { set; get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="taolxDbContext"></param>
        internal TaolxDbSet(TaolxDbContext taolxDbContext)
        {
            TaolxDbContext = taolxDbContext;
            ReadDbSet = taolxDbContext.ReadDbContext.Set<TEntity>();
            InternalQueryable = ReadDbSet.AsNoTracking();
        }

        /// <summary>
        /// 用sql执行查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            ReadDbSet.
            return TaolxDbContext.ReadDbContext.Database.SqlQuery<TEntity>(sql, parameters).ToList(); 
        }

        /// <summary>
        /// 用sql执行查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<TView> SqlQuery<TView>(string sql, params object[] parameters)
        {
            return TaolxDbContext.ReadDbContext.Database.SqlQuery<TView>(sql, parameters).ToList();
        }
    }
}
