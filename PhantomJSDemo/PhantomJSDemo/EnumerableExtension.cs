using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhantomJSDemo
{
    public static class EnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> AvgGroup<T>(this IEnumerable<T> source, int groupCount)
        {
            var group = new List<IEnumerable<T>>();
            if (groupCount <= 1)
                group.Add(source);
            else
            {
                var avg = source.Count() / groupCount;
                if (avg <= 1)
                {
                    group.Add(source);
                    for (var index = 0; index < groupCount - 1; index++)
                        group.Add(source.Skip(0).Take(0).ToList());
                }

                else
                {

                    for (var index = 0; index < groupCount - 1; index++)
                        group.Add(source.Skip(index * avg).Take(avg).ToList());
                    group.Add(source.Skip((groupCount - 1) * avg).Take(source.Count() - (avg * (groupCount - 1))).ToList());
                }
            }
            return group.AsEnumerable();
        }
    }
}
