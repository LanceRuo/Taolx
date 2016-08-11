using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess
{
    /// <summary>
    /// 淘旅行dbSet
    /// </summary>
    /// <typeparam name="TEntity">实体类</typeparam>
    public class TaolxDbSet<TEntity> : IQueryable<TEntity> where TEntity : class
    {
        /// <summary>
        /// TEntityType
        /// </summary>
        internal static Type TEntityType { get { return typeof(TEntity); } }

        /// <summary>
        /// 淘旅行dbContext
        /// </summary>
        internal TaolxDbContext TaolxDbContext { set; get; }

        /// <summary>
        /// EntityFramework read dbset 
        /// </summary>
        internal DbSet<TEntity> ReadDbSet { set; get; }

        /// <summary>
        ///  EntityFramework write dbset 
        /// </summary>
        internal DbSet<TEntity> WriteDbSet { set; get; }

        Expression IQueryable.Expression
        {
            get
            {
                return ((IQueryable<TEntity>)ReadDbSet).Expression;
            }
        }

        Type IQueryable.ElementType
        {
            get
            {
                return ((IQueryable<TEntity>)ReadDbSet).ElementType;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return ((IQueryable<TEntity>)ReadDbSet).Provider;
            }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IQueryable<TEntity>)ReadDbSet).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IQueryable<TEntity>)ReadDbSet).GetEnumerator();
        }
    }
}
