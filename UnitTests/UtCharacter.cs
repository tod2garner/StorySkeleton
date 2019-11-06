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
    public class UtCharacter
    {

        private Character theCharacter;

        [TestInitialize]
        public void TestInitialize()
        {
            theCharacter = new Character(0, "given");

            Assert.AreEqual(theCharacter.Id, 0);
            Assert.AreEqual(theCharacter.Name, "given");

            var other = new Character(1, "other1");
            theCharacter.CreateRelationshipWith(other);

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
            theCharacter.CreateRelationshipWith(other);

            int countAfter = theCharacter.AllRelations.Count;

            Assert.AreEqual(1, countAfter - countBefore, "Relationship count did not increase by 1");
            Assert.AreEqual(theCharacter.AllRelations.Last().OtherId, other.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CharacterCreateRelationship_ErrorWithDuplicate()
        {
            var duplicate = new Character(1, "other2");
            theCharacter.CreateRelationshipWith(duplicate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CharacterCreateRelationship_ErrorWithSelf()
        {
            theCharacter.CreateRelationshipWith(theCharacter);
        }


        [TestMethod]
        public void CharacterChangeTrust_Increase()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void CharacterChangeTrust_Decrease()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CharacterChangeTrust_ZeroMagnitude()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CharacterChangeTrust_TargetSelf()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CharacterChangeTrust_NewRelationship()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TrustLevelIsMutual_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TrustLevelIsMutual_IsFalse()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void EthicsLevelIsMutual_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void EthicsLevelIsMutual_IsFalse()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetTrustTowards_NullIfUnknownCharacter()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetTrustTowards_CorrectValue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetEthicsTowards_NullIfUnknownCharacter()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetEthicsTowards_CorrectValue()
        {
            throw new NotImplementedException();
        }
    }
}
