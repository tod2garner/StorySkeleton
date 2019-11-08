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
    public class UtOutcome
    {
        Outcome_ChangeTrust theOutcome;

        [TestInitialize]
        public void TestInitialize()
        {
            int givenMagnitude = 3;
            var role1 = new Role();
            var role2 = new Role();
            theOutcome = new Outcome_ChangeTrust(givenMagnitude, role1, role2);

            Assert.AreEqual(givenMagnitude, theOutcome.Magnitude);
            Assert.IsNotNull(theOutcome.BeingChanged);
            Assert.AreEqual(role1, theOutcome.BeingChanged);
            Assert.IsNotNull(theOutcome.Towards);
        }

        [TestMethod]
        public void WhenExecuted_ThenTrustIsChanged_NewRelation()
        {
            var the1 = new Character(1, "the1");
            var theOther = new Character(2, "theOther");
            var trustBefore = the1.GetTrustTowards(theOther.Id);

            theOutcome.BeingChanged.Participants.Add(the1);
            theOutcome.Towards.Participants.Add(theOther);

            theOutcome.Execute();

            var trustAfter = the1.GetTrustTowards(theOther.Id);
            Assert.AreNotEqual(trustBefore, trustAfter);
            Assert.IsNotNull(trustAfter);
        }

        [TestMethod]
        public void WhenExecuted_ThenTrustIsChanged()
        {
            var the1 = new Character(1, "the1");
            var theOther = new Character(2, "theOther");
            the1.CreateRelationshipWith(theOther);
            var trustBefore = the1.GetTrustTowards(theOther.Id);

            theOutcome.BeingChanged.Participants.Add(the1);
            theOutcome.Towards.Participants.Add(theOther);

            theOutcome.Execute();

            var trustAfter = the1.GetTrustTowards(theOther.Id);
            Assert.AreNotEqual(trustBefore, trustAfter);
        }

    }
}
