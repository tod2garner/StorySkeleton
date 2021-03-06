﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        ChangeInTrust theOutcome;

        [TestInitialize]
        public void TestInitialize()
        {
            int givenMagnitude = 3;
            var role1 = new Role("r1");
            var role2 = new Role("r2");
            theOutcome = new ChangeInTrust(givenMagnitude, role1, role2, "testOutcome");

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
            var trustBefore = the1.GetTrustTowardsId(theOther.Id);

            theOutcome.BeingChanged.Participants.Add(the1);
            theOutcome.Towards.Participants.Add(theOther);

            theOutcome.ExecuteAndGiveSummary();

            var trustAfter = the1.GetTrustTowardsId(theOther.Id);
            Assert.AreNotEqual(trustBefore, trustAfter);
            Assert.IsNotNull(trustAfter);
        }

        [TestMethod]
        public void WhenExecuted_ThenTrustIsChanged()
        {
            var the1 = new Character(1, "the1");
            var theOther = new Character(2, "theOther");
            the1.CreateRelationshipWith(theOther, null);
            var trustBefore = the1.GetTrustTowardsId(theOther.Id);

            theOutcome.BeingChanged.Participants.Add(the1);
            theOutcome.Towards.Participants.Add(theOther);

            theOutcome.ExecuteAndGiveSummary();

            var trustAfter = the1.GetTrustTowardsId(theOther.Id);
            Assert.AreNotEqual(trustBefore, trustAfter);
        }

        [TestMethod]
        public void WhenExecuted_ThenTrustIsChanged_MultipleCharacters()
        {
            var the1 = new Character(1, "the1");
            var the2 = new Character(2, "the2");
            var theOther = new Character(3, "theOther");
            var theOther2 = new Character(4, "theOther2");
            the1.CreateRelationshipWith(theOther, null);
            the1.CreateRelationshipWith(theOther2, null);
            the2.CreateRelationshipWith(theOther, null);
            the2.CreateRelationshipWith(theOther2, null);
            var trustBefore11 = the1.GetTrustTowardsId(theOther.Id);
            var trustBefore12 = the1.GetTrustTowardsId(theOther2.Id);
            var trustBefore21 = the2.GetTrustTowardsId(theOther.Id);
            var trustBefore22 = the2.GetTrustTowardsId(theOther2.Id);

            theOutcome.BeingChanged.Participants.Add(the1);
            theOutcome.BeingChanged.Participants.Add(the2);
            theOutcome.Towards.Participants.Add(theOther);
            theOutcome.Towards.Participants.Add(theOther2);

            theOutcome.ExecuteAndGiveSummary();

            var trustAfter11 = the1.GetTrustTowardsId(theOther.Id);
            var trustAfter12 = the1.GetTrustTowardsId(theOther2.Id);
            var trustAfter21 = the2.GetTrustTowardsId(theOther.Id);
            var trustAfter22 = the2.GetTrustTowardsId(theOther2.Id);
            Assert.AreNotEqual(trustBefore11, trustAfter11);
            Assert.AreNotEqual(trustBefore12, trustAfter12);
            Assert.AreNotEqual(trustBefore21, trustAfter21);
            Assert.AreNotEqual(trustBefore22, trustAfter22);
        }

        [TestMethod]
        public void CopyOutcome()
        { 
            var altRole1 = new Role("three");
            var altRole2 = new Role("four");
            var replacementList = new List<Role>(2) { altRole1, altRole2 };

            var theCopy = theOutcome.Copy(replacementList) as ChangeInTrust;

            Assert.AreEqual(theOutcome.OutcomeName, theCopy.OutcomeName);
            Assert.AreEqual(theOutcome.Magnitude, theCopy.Magnitude);
            Assert.AreNotEqual(theOutcome.BeingChanged, theCopy.BeingChanged);
            Assert.AreNotEqual(theOutcome.Towards, theCopy.Towards);
        }

        [TestMethod]
        public void DescribeOutcomeParticipants()
        {
            throw new NotImplementedException();        
        }
    }
}
