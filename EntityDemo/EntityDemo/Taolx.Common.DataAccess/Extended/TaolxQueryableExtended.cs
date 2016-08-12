using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess
{
    public static class TaolxQueryableExtended
    {
        public static TSource Aggregate<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TSource, TSource>> func)
        {
            return source.InternalQueryable.Aggregate(func);
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(this TaolxQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func)
        {
            return source.InternalQueryable.Aggregate(seed, func);
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(this TaolxQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func, Expression<Func<TAccumulate, TResult>> selector)
        {
            return source.InternalQueryable.Aggregate(seed, func, selector);
        }

        public static bool All<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.All(predicate);
        }

        public static bool Any<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.Any();
        }

        public static bool Any<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.Any(predicate);
        }

        public static float? Average(this TaolxQueryable<float?> source)
        {
            return source.InternalQueryable.Average();
        }

        public static double Average(this TaolxQueryable<int> source)
        {
            return source.InternalQueryable.Average();
        }

        public static double? Average(this TaolxQueryable<int?> source)
        {
            return source.InternalQueryable.Average();
        }

        public static double Average(this TaolxQueryable<long> source)
        {
            return source.InternalQueryable.Average();
        }

        public static double? Average(this TaolxQueryable<long?> source)
        {
            return source.InternalQueryable.Average();
        }

        public static float Average(this TaolxQueryable<float> source)
        {
            return source.InternalQueryable.Average();
        }

        public static double Average(this TaolxQueryable<double> source)
        {
            return source.InternalQueryable.Average();
        }

        public static double? Average(this TaolxQueryable<double?> source)
        {
            return source.InternalQueryable.Average();
        }

        public static decimal Average(this TaolxQueryable<decimal> source)
        {
            return source.InternalQueryable.Average();
        }

        public static decimal? Average(this TaolxQueryable<decimal?> source)
        {
            return source.InternalQueryable.Average();
        }

        public static double Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static float Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, float>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static float? Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static double Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, long>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static double? Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static double Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, double>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static double? Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static decimal Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static decimal? Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static double? Average<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
        {
            return source.InternalQueryable.Average(selector);
        }

        public static TaolxQueryable<TSource> Concat<TSource>(this TaolxQueryable<TSource> source1, IEnumerable<TSource> source2)
        {
            var qb = source1.InternalQueryable.Concat(source2);
            return source1.Clone(qb);
        }

        public static bool Contains<TSource>(this TaolxQueryable<TSource> source, TSource item)
        {
            return source.InternalQueryable.Contains(item);
        }

        public static bool Contains<TSource>(this TaolxQueryable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
        {
            return source.InternalQueryable.Contains(item);
        }

        public static int Count<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.Count();
        }

        public static int Count<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.Count(predicate);
        }

        public static TaolxQueryable<TSource> DefaultIfEmpty<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.Clone(source.InternalQueryable.DefaultIfEmpty());
        }

        public static TaolxQueryable<TSource> DefaultIfEmpty<TSource>(this TaolxQueryable<TSource> source, TSource defaultValue)
        {
            return source.Clone(source.InternalQueryable.DefaultIfEmpty(defaultValue));
        }

        public static TaolxQueryable<TSource> Distinct<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.Clone(source.InternalQueryable.Distinct());
        }

        public static TaolxQueryable<TSource> Distinct<TSource>(this TaolxQueryable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            return source.Clone(source.InternalQueryable.Distinct(comparer));
        }

        public static TSource ElementAt<TSource>(this TaolxQueryable<TSource> source, int index)
        {
            return source.InternalQueryable.ElementAt(index);
        }

        public static TSource ElementAtOrDefault<TSource>(this TaolxQueryable<TSource> source, int index)
        {
            return source.InternalQueryable.ElementAtOrDefault(index);
        }

        public static TaolxQueryable<TSource> Except<TSource>(this TaolxQueryable<TSource> source1, IEnumerable<TSource> source2)
        {
            return source1.Clone(source1.InternalQueryable.Except(source2));
        }


        public static TaolxQueryable<TSource> Except<TSource>(this TaolxQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
        {
            return source1.Clone(source1.InternalQueryable.Except(source2, comparer));
        }

        public static TSource First<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.First();
        }
        public static TSource First<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.First(predicate);
        }

        public static TSource FirstOrDefault<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.FirstOrDefault();
        }

        public static TSource FirstOrDefault<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.FirstOrDefault(predicate);
        }

        public static TaolxQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            var qb = source.InternalQueryable.GroupBy(keySelector);
            return source.CloneGroup(qb);
        }

        public static TaolxQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey> comparer)
        {
            var qb = source.InternalQueryable.GroupBy(keySelector, comparer);
            return source.CloneGroup(qb);
        }

        public static TaolxQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector)
        {
            var qb = source.InternalQueryable.GroupBy(keySelector, elementSelector);
            return source.CloneGroup(qb);
        }

        public static TaolxQueryable<TResult> GroupBy<TSource, TKey, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector)
        {
            var qb = source.InternalQueryable.GroupBy(keySelector, resultSelector);
            return source.CloneGroup(qb);
        }
        public static TaolxQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, IEqualityComparer<TKey> comparer)
        {
            var qb = source.InternalQueryable.GroupBy(keySelector, elementSelector, comparer);
            return source.CloneGroup(qb);
        }

        public static TaolxQueryable<TResult> GroupBy<TSource, TKey, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            var qb = source.InternalQueryable.GroupBy(keySelector, resultSelector, comparer);
            return source.CloneGroup(qb);
        }

        public static TaolxQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector)
        {
            var qb = source.InternalQueryable.GroupBy(keySelector, elementSelector, resultSelector);
            return source.CloneGroup(qb);
        }

        public static TaolxQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            var qb = source.InternalQueryable.GroupBy(keySelector, elementSelector, resultSelector);
            return source.CloneGroup(qb);
        }


        public static TaolxQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this TaolxQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector)
        {
            var qb = outer.InternalQueryable.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector);
            return outer.CloneGroupJoin(qb);
        }
        public static TaolxQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this TaolxQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            var qb = outer.InternalQueryable.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
            return outer.CloneGroupJoin(qb);
        }
        public static TaolxQueryable<TSource> Intersect<TSource>(this TaolxQueryable<TSource> source1, IEnumerable<TSource> source2)
        {
            var qb = source1.InternalQueryable.Intersect(source2);
            return source1.Clone(qb);
        }

        public static TaolxQueryable<TSource> Intersect<TSource>(this TaolxQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
        {
            var qb = source1.InternalQueryable.Intersect(source2, comparer);
            return source1.Clone(qb);
        }


        public static TaolxQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this TaolxQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {
            var qb = outer.InternalQueryable.Join(inner, outerKeySelector, innerKeySelector, resultSelector);
            return outer.Clone(qb);
        }
        public static TaolxQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this TaolxQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            var qb = outer.InternalQueryable.Join(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
            return outer.Clone(qb);
        }

        public static TSource Last<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.Last();
        }

        public static TSource Last<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.Last(predicate);
        }


        public static TSource LastOrDefault<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.LastOrDefault();
        }
        public static TSource LastOrDefault<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.LastOrDefault(predicate);
        }

        public static long LongCount<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.LongCount();
        }

        public static long LongCount<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.LongCount(predicate);
        }

        public static TSource Max<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.Max();
        }

        public static TResult Max<TSource, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            return source.InternalQueryable.Max(selector);
        }

        public static TSource Min<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.Min();
        }

        public static TResult Min<TSource, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            return source.InternalQueryable.Min(selector);
        }


        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            return source.InternalQueryable.OrderBy(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            return source.InternalQueryable.OrderBy(keySelector, comparer);
        }


        public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            return source.InternalQueryable.OrderByDescending(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            return source.InternalQueryable.OrderByDescending(keySelector, comparer);
        }

        public static TaolxQueryable<TSource> Reverse<TSource>(this TaolxQueryable<TSource> source)
        {
            var qp = source.InternalQueryable.Reverse();
            return source.Clone(qp);
        }

        public static TaolxQueryable<TResult> Select<TSource, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int, TResult>> selector)
        {
            var qp = source.InternalQueryable.Select(selector);
            return source.Clone(qp);
        }

        public static TaolxQueryable<TResult> Select<TSource, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            var qp = source.InternalQueryable.Select(selector);
            return source.Clone(qp);
        }


        public static TaolxQueryable<TResult> SelectMany<TSource, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TResult>>> selector)
        {
            var qp = source.InternalQueryable.SelectMany(selector);
            return source.Clone(qp);
        }

        public static TaolxQueryable<TResult> SelectMany<TSource, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TResult>>> selector)
        {
            var qp = source.InternalQueryable.SelectMany(selector);
            return source.Clone(qp);
        }
        public static TaolxQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
        {
            var qp = source.InternalQueryable.SelectMany(collectionSelector, resultSelector);
            return source.Clone(qp);
        }

        public static TaolxQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this TaolxQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
        {
            var qp = source.InternalQueryable.SelectMany(collectionSelector, resultSelector);
            return source.Clone(qp);
        }

        public static bool SequenceEqual<TSource>(this TaolxQueryable<TSource> source1, IEnumerable<TSource> source2)
        {
            return source1.InternalQueryable.SequenceEqual(source2);
        }

        public static bool SequenceEqual<TSource>(this TaolxQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
        {
            return source1.InternalQueryable.SequenceEqual(source2, comparer);
        }

        public static TSource Single<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.Single();
        }

        public static TSource Single<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.Single(predicate);
        }

        public static TSource SingleOrDefault<TSource>(this TaolxQueryable<TSource> source)
        {
            return source.InternalQueryable.SingleOrDefault();
        }
        public static TSource SingleOrDefault<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.InternalQueryable.SingleOrDefault(predicate);
        }

        public static TaolxQueryable<TSource> Skip<TSource>(this TaolxQueryable<TSource> source, int count)
        {
            var qp = source.InternalQueryable.Skip(count);
            return source.Clone(qp);
        }

        public static TaolxQueryable<TSource> SkipWhile<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
        {
            var qp = source.InternalQueryable.SkipWhile(predicate);
            return source.Clone(qp);
        }

        public static TaolxQueryable<TSource> SkipWhile<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            var qp = source.InternalQueryable.SkipWhile(predicate);
            return source.Clone(qp);
        }

        public static int Sum(this TaolxQueryable<int> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static int? Sum(this TaolxQueryable<int?> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static long Sum(this TaolxQueryable<long> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static long? Sum(this TaolxQueryable<long?> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static float Sum(this TaolxQueryable<float> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static float? Sum(this TaolxQueryable<float?> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static double Sum(this TaolxQueryable<double> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static double? Sum(this TaolxQueryable<double?> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static decimal Sum(this TaolxQueryable<decimal> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static decimal? Sum(this TaolxQueryable<decimal?> source)
        {
            return source.InternalQueryable.Sum();
        }

        public static int? Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static long Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, long>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static double Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, double>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static long? Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static float Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, float>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static int Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static float? Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static decimal? Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static decimal Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static double? Sum<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
        {
            return source.InternalQueryable.Sum(selector);
        }

        public static TaolxQueryable<TSource> Take<TSource>(this TaolxQueryable<TSource> source, int count)
        {
            var qp = source.InternalQueryable.Take(count);
            return source.Clone(qp);
        }

        public static TaolxQueryable<TSource> TakeWhile<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            var qp = source.InternalQueryable.TakeWhile(predicate);
            return source.Clone(qp);
        }

        public static TaolxQueryable<TSource> TakeWhile<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
        {
            var qp = source.InternalQueryable.TakeWhile(predicate);
            return source.Clone(qp);
        }

        public static TaolxQueryable<TSource> Union<TSource>(this TaolxQueryable<TSource> source1, IEnumerable<TSource> source2)
        {
            var qp = source1.InternalQueryable.Union(source2);
            return source1.Clone(qp);
        }

        public static TaolxQueryable<TSource> Union<TSource>(this TaolxQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
        {
            var qp = source1.InternalQueryable.Union(source2, comparer);
            return source1.Clone(qp);
        }

        public static TaolxQueryable<TSource> Where<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
        {
            return source.Clone(source.InternalQueryable.Where(predicate));
        }

        public static TaolxQueryable<TSource> Where<TSource>(this TaolxQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.Clone(source.InternalQueryable.Where(predicate));
        }

    }
}
