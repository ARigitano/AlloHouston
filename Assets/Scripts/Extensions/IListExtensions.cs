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
            for (int i = (ts.Count - 1); i > 1; i--)
            {
                int r = rand.Next(i + 1);
                T tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }
            return list;
        }
    }
}
