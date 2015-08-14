using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Preference.Engine.AI.Bidding;

namespace Preference.Tests
{
    /// <summary>
    /// Tests the <see cref="ProbabilityHelper"/> class.
    /// </summary>
    [TestClass]
    public class TestProbabilityHelper
    {
        /// <summary>
        /// Tests the <see cref="ProbabilityHelper.Combine"/> method.
        /// </summary>
        [TestMethod]
        public void TestCombine()
        {
            var a = new List<TrickProbability>
            {
                new TrickProbability(1, .25),
                new TrickProbability(2, .75),
                new TrickProbability(3, .0),
            };            
            
            var b = new List<TrickProbability>
            {
                new TrickProbability(2, .5),
                new TrickProbability(3, .5),
            };

            var expected = new List<TrickProbability>
            {
                new TrickProbability(3, .125),
                new TrickProbability(4, .5),
                new TrickProbability(5, .375),
            };

            IEnumerable<TrickProbability> result = a.Combine(b);

            Assert.IsTrue(result.SequenceEqual(expected));
        }

        /// <summary>
        /// Tests the <see cref="ProbabilityHelper.Expectation"/> method.
        /// </summary>
        [TestMethod]
        public void TestExpectation()
        {
            var p = new List<TrickProbability>
            {
                new TrickProbability(0, .6),
                new TrickProbability(1, .1),
                new TrickProbability(2, .3),
                new TrickProbability(3, .0),
            };

            var trickCosts = new[] {75.0, -75.0, -150.0, -225.0};

            double expectation = p.Expectation(tricks => trickCosts[tricks]);

            Assert.AreEqual(-7.5, expectation);
        }
    }
}
