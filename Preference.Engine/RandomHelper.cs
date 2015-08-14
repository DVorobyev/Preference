using System;
using System.Collections.Generic;

namespace Preference.Engine
{
    internal static class RandomHelper
    {
        internal static int Next(int maxValue)
        {
            return gRandom.Next(maxValue);
        }

        internal static T Select<T>(IList<T> list)
        {
            return list[gRandom.Next(list.Count)];
        }

#if DEBUG
        private static readonly Random gRandom = new Random(123);
#else
        private static readonly  Random gRandom = new Random();
#endif
    }
}
