using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Preference.Engine.AI.Bidding;

namespace Preference.Tests
{
    [TestClass]
    public class TestHandEvaluator
    {
        [TestMethod]
        public void TestTwoCardsEnumerator()
        {
            const int cards = 1 | 4 | 8 | 32 | 64 | 128 | 512 | 1024 | 4096 | 8192;
            var results = new List<int>();
            
            var enumerator = new HandEvaluator.TwoCardsEnumerator(cards);
            while (enumerator.MoveNext())
                results.Add(enumerator.Current);

            Assert.AreEqual(45, results.Count);
        }        
        
        [TestMethod]
        public void TestTwoCardsEnumeratorZeroInput()
        {
            var results = new List<int>();
            
            var enumerator = new HandEvaluator.TwoCardsEnumerator(0);
            while (enumerator.MoveNext())
                results.Add(enumerator.Current);

            Assert.AreEqual(0, results.Count);
        }
    }
}
