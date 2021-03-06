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
    public class UtCharacter
    {

        private Character theCharacter;
        private Character otherOne;
        private Random rng;

        [TestInitialize]
        public void TestInitialize()
        {
            rng = new Random();
            theCharacter = new Character(0, "given");

            Assert.AreEqual(theCharacter.Id, 0);
            Assert.AreEqual(theCharacter.Name, "given");

            otherOne = new Character(1, "other1");
            theCharacter.CreateRelationshipWith(otherOne, rng);

            Assert.AreEqual(theCharacter.AllRelations.Count, 1);
        }

        [TestMethod]
        public void WhenCharacterIsCopied_TraitsMatch()
        {
            var theCopy = theCharacter.Copy();

            Assert.AreEqual(theCharacter.Id, theCopy.Id, "Id does not match");
            Assert.AreEqual(theCharacter.Name, theCopy.Name, "Name does not match");
            Assert.AreEqual(theCharacter.BaseMorality, theCopy.BaseMorality, "BaseMorality does not match");
            Assert.AreEqual(theCharacter.BaseSuspicion, theCopy.BaseSuspicion, "BaseSuspicion does not match");
        }

        [TestMethod]
        public void WhenCharacterIsCopied_RelationshipsMatch()
        {
            var theCopy = theCharacter.Copy();

            Assert.AreEqual(theCharacter.AllRelations.Count, theCopy.AllRelations.Count);

            for (int i = 0; i < theCharacter.AllRelations.Count; i++)
            {
                Assert.AreEqual(theCharacter.AllRelations[i].OtherId, theCopy.AllRelations[i].OtherId);
                Assert.AreEqual(theCharacter.AllRelations[i].SelfId, theCopy.AllRelations[i].SelfId);
                Assert.AreEqual(theCharacter.AllRelations[i].Trust, theCopy.AllRelations[i].Trust);
                Assert.AreEqual(theCharacter.AllRelations[i].Ethics, theCopy.AllRelations[i].Ethics);
            }
        }

        [TestMethod]
        public void CharacterIsAcquainted_IsTrue()
        {
            int id_HaveRelationWith = theCharacter.AllRelations.First().OtherId;

            Assert.IsTrue(theCharacter.IsAcquaintedWith(id_HaveRelationWith));
        }

        [TestMethod]
        public void CharacterIsAcquainted_IsFalse()
        {
            Assert.IsFalse(theCharacter.IsAcquaintedWith(theCharacter.Id), "Should not have relation with self");
            Assert.IsFalse(theCharacter.IsAcquaintedWith(99), "Should not have relation with otherId = 99");
        }

        [TestMethod]
        public void CharacterCreateRelationship_IsSuccesssful()
        {
            int countBefore = theCharacter.AllRelations.Count;

            var other = new Character(2, "other2");
            theCharacter.CreateRelationshipWith(other, rng);

            int countAfter = theCharacter.AllRelations.Count;

            Assert.AreEqual(1, countAfter - countBefore, "Relationship count did not increase by 1");
            Assert.AreEqual(theCharacter.AllRelations.Last().OtherId, other.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CharacterCreateRelationship_ErrorWithDuplicate()
        {
            var duplicate = new Character(1, "other2");
            theCharacter.CreateRelationshipWith(duplicate, rng);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CharacterCreateRelationship_ErrorWithSelf()
        {
            theCharacter.CreateRelationshipWith(theCharacter, rng);
        }


        [TestMethod]
        public void CharacterChangeTrust_Increase()
        {
            var theRelationship = theCharacter.AllRelations.First();
            var trustBefore = theRelationship.Trust;

            theCharacter.ChangeTrust(3, otherOne);

            var trustAfter = theRelationship.Trust;
            Assert.IsTrue(trustAfter > trustBefore);
        }

        [TestMethod]
        public void CharacterChangeTrust_Decrease()
        {
            var theRelationship = theCharacter.AllRelations.First();
            var trustBefore = theRelationship.Trust;

            theCharacter.ChangeTrust(-2, otherOne);

            var trustAfter = theRelationship.Trust;
            Assert.IsTrue(trustAfter < trustBefore);
        }

        [TestMethod]
        public void CharacterChangeTrust_ZeroMagnitude()
        {
            var theRelationship = theCharacter.AllRelations.First();
            var trustBefore = theRelationship.Trust;
            var trustProgressBefore = theRelationship.DurabilityOfTrust;

            theCharacter.ChangeTrust(0, otherOne);

            var trustAfter = theRelationship.Trust;
            var trustProgressAfter = theRelationship.DurabilityOfTrust;
            Assert.AreEqual(trustBefore, trustAfter);
            Assert.AreEqual(trustProgressBefore, trustProgressAfter);
        }

        [TestMethod]
        public void CharacterChangeTrust_TargetSelf_NoChange()
        {
            var theRelationship = theCharacter.AllRelations.First();
            var trustBefore = theRelationship.Trust;
            var trustProgressBefore = theRelationship.DurabilityOfTrust;

            theCharacter.ChangeTrust(-2, theCharacter);

            var trustAfter = theRelationship.Trust;
            var trustProgressAfter = theRelationship.DurabilityOfTrust;
            Assert.AreEqual(trustBefore, trustAfter);
            Assert.AreEqual(trustProgressBefore, trustProgressAfter);
        }

        [TestMethod]
        public void CharacterChangeTrust_NewRelationship()
        {
            int relationCountBefore = theCharacter.AllRelations.Count();
            var newChar2 = new Character(2, "char2");
            var trustBefore = theCharacter.GetTrustTowardsId(newChar2.Id);

            theCharacter.ChangeTrust(3, newChar2);

            int relationCountAfter = theCharacter.AllRelations.Count();
            var trustAfter = theCharacter.GetTrustTowardsId(newChar2.Id);

            Assert.AreEqual(1, relationCountAfter - relationCountBefore);
            Assert.AreNotEqual(trustBefore, trustAfter);
            Assert.IsNotNull(trustAfter);
        }

        [TestMethod]
        public void IsTrustLevelMutual_IsTrue()
        {
            otherOne.CreateRelationshipWith(theCharacter, rng);

            Assert.IsTrue(theCharacter.IsTrustLevelMutual(otherOne));
        }

        [TestMethod]
        public void IsTrustLevelMutual_IsFalse()
        {
            otherOne.BaseSuspicion = SuspicionScale.Relaxed;
            otherOne.CreateRelationshipWith(theCharacter, rng);

            Assert.IsFalse(theCharacter.IsTrustLevelMutual(otherOne));
        }

        [TestMethod]
        public void IsTrustLevelMutual_IsFalseForOneWayRelation()
        {
            Assert.IsFalse(theCharacter.IsTrustLevelMutual(otherOne));
        }

        [TestMethod]
        public void IsEthicsLevelMutual_IsTrue()
        {
            otherOne.CreateRelationshipWith(theCharacter, rng);

            Assert.IsTrue(theCharacter.IsEthicsLevelMutual(otherOne));
        }

        [TestMethod]
        public void IsEthicsLevelMutual_IsFalse()
        {
            otherOne.BaseMorality = Morality.Forgive;
            otherOne.CreateRelationshipWith(theCharacter, rng);

            Assert.IsFalse(theCharacter.IsEthicsLevelMutual(otherOne));
        }

        [TestMethod]
        public void IsEthicsLevelMutual_IsFalseForOneWayRelation()
        {
            Assert.IsFalse(theCharacter.IsEthicsLevelMutual(otherOne));
        }

        [TestMethod]
        public void GetTrustTowardsId_NullIfUnknownCharacter()
        {
            Assert.IsNull(otherOne.GetTrustTowardsId(theCharacter.Id));
        }

        [TestMethod]
        public void GetTrustTowardsId_CorrectValue()
        {
            Assert.AreEqual(EthicsScale.Exploit, theCharacter.GetTrustTowardsId(otherOne.Id));
        }

        [TestMethod]
        public void GetEthicsTowardsId_NullIfUnknownCharacter()
        {
            Assert.IsNull(otherOne.GetEthicsTowardsId(theCharacter.Id));
        }

        [TestMethod]
        public void GetEthicsTowardsId_CorrectValue()
        {
            Assert.AreEqual(EthicsScale.Coexist, theCharacter.GetEthicsTowardsId(otherOne.Id));
        }

        [TestMethod]
        public void GetTrustTowards_UnknownCharacter_CorrectValue()
        {
            Assert.AreEqual(EthicsScale.Exploit, otherOne.GetTrustTowards(theCharacter));
        }

        [TestMethod]
        public void GetTrustTowards_KnownCharacter_CorrectValue()
        {
            Assert.AreEqual(EthicsScale.Exploit, theCharacter.GetTrustTowards(otherOne));
        }

        [TestMethod]
        public void GetEthicsTowards_UnknownCharacter_CorrectValue()
        {
            Assert.AreEqual(EthicsScale.Coexist, otherOne.GetEthicsTowards(theCharacter));
        }

        [TestMethod]
        public void GetEthicsTowards_KnownCharacter_CorrectValue()
        {
            Assert.AreEqual(EthicsScale.Coexist, theCharacter.GetEthicsTowards(otherOne));
        }

        [TestMethod]
        public void DescribeTrustTowards_WhenUnknown()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DescribeTrustTowards_Whenknown()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DescribeEthicsTowards_WhenUnknown()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DescribeEthicsTowards_Whenknown()
        {
            throw new NotImplementedException();
        }
    }
}
