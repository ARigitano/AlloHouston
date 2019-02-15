using System;
using System.Collections.Generic;
using System.Linq;

namespace CRI.HelloHouston
{
    public static class IListExtensions
    {
        public static IList<T> Shuffle<T>(this IList<T> ts)
        {
            return ts.Shuffle(new Random());
        }

        public static IList<T> Shuffle<T>(this IList<T> ts, int randomSeed)
        {
            return ts.Shuffle(new Random(randomSeed));
        }

        public static IList<T> Shuffle<T>(this IList<T> ts, Random rand)
        {
            IList<T> list = ts.ToList();
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = rand.Next(i, count);
                var tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }
            return list;
        }
    }
}
