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
        public void ConstructPossibleResult_Successful()
        {
            int givenValue = 25;
            var theResult = new PossibleResult(givenValue);

            Assert.AreEqual(givenValue, theResult.ProbabilityScore);
            Assert.IsNotNull(theResult.TheOutcomes);
        }

        [TestMethod]
        public void CopyPossibleResult()
        {
            int givenValue = 25;
            var theResult = new PossibleResult(givenValue);
            var myOutcome1 = new ChangeInTrust(1, new Role("one"), new Role("two"), "myOutcome1");
            var myOutcome2 = new ChangeInTrust(1, new Role("two"), new Role("one"), "myOutcome2");
            theResult.TheOutcomes.Add(myOutcome1);
            theResult.TheOutcomes.Add(myOutcome2);

            var altRole1 = new Role("three");
            var altRole2 = new Role("four");
            var replacementList = new List<Role>(2) { altRole1, altRole2 };

            var theCopy = theResult.Copy(replacementList);

            Assert.AreEqual(theResult.ProbabilityScore, theCopy.ProbabilityScore);
            Assert.AreEqual(theResult.TheOutcomes.Count, theCopy.TheOutcomes.Count);

            var copyOutcome1 = theCopy.TheOutcomes.First() as ChangeInTrust;
            Assert.AreEqual(myOutcome1.OutcomeName, copyOutcome1.OutcomeName);
            Assert.AreEqual(myOutcome1.Magnitude, copyOutcome1.Magnitude);
            Assert.AreNotEqual(myOutcome1.BeingChanged, copyOutcome1.BeingChanged);
            Assert.AreNotEqual(myOutcome1.Towards, copyOutcome1.Towards);
        }
    }
}
