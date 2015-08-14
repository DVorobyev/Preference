using System;
using System.Collections.Generic;
using System.Linq;

namespace Preference.Engine.AI.Bidding
{
    /// <summary>
    /// Provides extension methods that help to
    /// </summary>
    internal static class ProbabilityHelper
    {
        internal static IEnumerable<TrickProbability> Combine(
            this IEnumerable<TrickProbability> valuesA,
            IEnumerable<TrickProbability> valuesB)
        {
            var result = valuesA
                .SelectMany(a => valuesB.Select(a.And))
                .GroupBy(
                    g => g.Tricks,
                    (t, g) => g.Aggregate((v1, v2) => new TrickProbability(t, v1.Probability + v2.Probability)))
                .Where(p => p.Probability > .0);

            return result;
        }

        /// <summary>
        /// Returns a mathematical expectation of the outcome given a collection of <see cref="TrickProbability"/> values.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="trickCost"></param>
        /// <returns></returns>
        internal static double Expectation(this IEnumerable<TrickProbability> values, Func<int, double> trickCost)
        {
            return values.Sum(value => trickCost(value.Tricks) * value.Probability);
        }
    }
}
