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

    public class TaolxQueryable<TEntity> : TaolxQueryable, IEnumerable<TEntity>   
    {

        internal IQueryable<TEntity> InternalQueryable { set; get; }

        /// <summary>
        /// 淘旅行dbContext
        /// </summary>
        internal TaolxDbContext TaolxDbContext { set; get; }
         

        #region impl  method


        public IEnumerator<TEntity> GetEnumerator()
        {
            return InternalQueryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InternalQueryable.GetEnumerator();
        }

        #endregion

        #region  Clone

        internal TaolxQueryable<CTEntity> Clone<CTEntity>(IQueryable<CTEntity> internalQueryable)
        {
            return new TaolxQueryable<CTEntity>()
            {
                InternalQueryable = internalQueryable,
                TaolxDbContext = this.TaolxDbContext
            };
        }

        internal TaolxQueryable<IGrouping<TKey, TSource>> CloneGroup<TKey, TSource>(IQueryable<IGrouping<TKey, TSource>> internalQueryable)
        {
            return new TaolxQueryable<IGrouping<TKey, TSource>>()
            {
                InternalQueryable = internalQueryable,
                TaolxDbContext = this.TaolxDbContext
            };
        }

        internal TaolxQueryable<TResult> CloneGroup<TResult>(IQueryable<TResult> internalQueryable)
        {
            return new TaolxQueryable<TResult>()
            {
                InternalQueryable = internalQueryable,
                TaolxDbContext = this.TaolxDbContext
            };
        }

        internal TaolxQueryable<TResult> CloneGroupJoin<TResult>(IQueryable<TResult> internalQueryable)
        {
            return new TaolxQueryable<TResult>()
            {
                InternalQueryable = internalQueryable,
                TaolxDbContext = this.TaolxDbContext
            };
        }


        #endregion
    }
}
