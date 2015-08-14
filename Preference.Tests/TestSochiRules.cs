using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Preference.Engine.Rules;

namespace Preference.Tests
{
    [TestClass]
    public class TestSochiRules
    {
        [TestMethod]
        public void TestGameCost()
        {
            var rules = new SochiRules();

            Assert.AreEqual(2, rules.GetGameCost());
        }
    }
}
