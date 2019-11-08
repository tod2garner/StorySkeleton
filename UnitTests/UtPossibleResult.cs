using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UtPossibleResult
    {
        [TestInitialize]
        public void TestInitialize() { }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructPossibleResult_InvalidPercent_TooSmall()
        {
            var newPossibleResult = new PossibleResult(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructPossibleResult_InvalidPercent_TooLarge()
        {
            var newPossibleResult = new PossibleResult(101);
        }

        [TestMethod]
        public void ConstructPossibleResult_Successful()
        {
            int givenValue = 25;
            var theResult = new PossibleResult(givenValue);

            Assert.AreEqual(givenValue, theResult.PercentChance);
            Assert.IsNotNull(theResult.TheOutcomes);
        }
    }
}
