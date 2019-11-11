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
    public class UtRelationship
    {
        Relationship theRelationship;

        [TestInitialize]
        public void TestInitialize()
        {
            theRelationship = new Relationship(0, 1, EthicsScale.Coexist, EthicsScale.Coexist);

            Assert.AreEqual(0, theRelationship.SelfId);
            Assert.AreEqual(1, theRelationship.OtherId);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Ethics);
        }

        [TestMethod]
        public void WhenRelationshipIsCopied_PropertiesMatch()
        {
            var theCopy = theRelationship.Copy();

            Assert.IsTrue(theRelationship != theCopy); //distinct objects/pointers
            Assert.AreEqual(theRelationship.SelfId, theCopy.SelfId);
            Assert.AreEqual(theRelationship.OtherId, theCopy.OtherId);
            Assert.AreEqual(theRelationship.Trust, theCopy.Trust);
            Assert.AreEqual(theRelationship.Ethics, theCopy.Ethics);
            Assert.AreEqual(theRelationship.DurabilityOfEthics, theCopy.DurabilityOfEthics);
            Assert.AreEqual(theRelationship.DurabilityOfTrust, theCopy.DurabilityOfTrust);
        }
        
        [TestMethod]
        public void WhenRelationshipIsCopied_AfterChangeInTrust_PropertiesMatch()
        {
            theRelationship.ChangeTrust(2, SuspicionScale.Average, Morality.Reciprocate);
            theRelationship.ChangeTrust(-1, SuspicionScale.Average, Morality.Reciprocate);
            theRelationship.ChangeTrust(1, SuspicionScale.Average, Morality.Reciprocate);
            var theCopy = theRelationship.Copy();

            Assert.IsTrue(theRelationship != theCopy); //distinct objects/pointers
            Assert.AreEqual(theRelationship.SelfId, theCopy.SelfId);
            Assert.AreEqual(theRelationship.OtherId, theCopy.OtherId);
            Assert.AreEqual(theRelationship.Trust, theCopy.Trust);
            Assert.AreEqual(theRelationship.Ethics, theCopy.Ethics);
            Assert.AreEqual(theRelationship.DurabilityOfEthics, theCopy.DurabilityOfEthics);
            Assert.AreEqual(theRelationship.DurabilityOfTrust, theCopy.DurabilityOfTrust);
        }

        [TestMethod]
        public void RelationshipChangeTrust_ZeroMagnitude_NoChanges()
        {
            theRelationship.ChangeTrust(0, SuspicionScale.Paranoid, Morality.Forgive);
            Assert.AreEqual(0, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(0, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Increase1_AR()
        {
            theRelationship.ChangeTrust(1, SuspicionScale.Average, Morality.Reciprocate);

            Assert.AreEqual(33, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(33, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Increase1_PE()
        {
            theRelationship.ChangeTrust(1, SuspicionScale.Paranoid, Morality.Exploit);

            Assert.AreEqual(14, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(10, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Increase2_RE()
        {
            theRelationship.ChangeTrust(2, SuspicionScale.Relaxed, Morality.Exploit);

            Assert.AreEqual(0, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(75, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Cooperate, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Increase4_AR()
        {
            theRelationship.ChangeTrust(4, SuspicionScale.Average, Morality.Reciprocate);

            Assert.AreEqual(33, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(33, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Cooperate, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Cooperate, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Increase11_NE()
        {
            theRelationship.ChangeTrust(11, SuspicionScale.Naive, Morality.Exploit);

            Assert.AreEqual(100, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(525, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Confide, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Embrace, theRelationship.Ethics);
        }
        
        [TestMethod]
        public void RelationshipChangeTrust_Decrease1_AR()
        {
            theRelationship.ChangeTrust(-1, SuspicionScale.Average, Morality.Reciprocate);

            Assert.AreEqual(0, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(0, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Exploit, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Exploit, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Decrease1_PF()
        {
            theRelationship.ChangeTrust(-1, SuspicionScale.Paranoid, Morality.Forgive);

            Assert.AreEqual(0, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(-75, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Exploit, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Coexist, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Decrease2_RE()
        {
            theRelationship.ChangeTrust(-2, SuspicionScale.Relaxed, Morality.Exploit);

            Assert.AreEqual(-100, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(-100, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Exploit, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Exploit, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Decrease2_RF()
        {
            theRelationship.ChangeTrust(-2, SuspicionScale.Relaxed, Morality.Forgive);

            Assert.AreEqual(-100, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(-50, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Exploit, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Exploit, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Decrease3_AR()
        {
            theRelationship.ChangeTrust(-3, SuspicionScale.Average, Morality.Reciprocate);

            Assert.AreEqual(0, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(0, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Beat, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Beat, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Decrease3_GF()
        {
            theRelationship.ChangeTrust(-3, SuspicionScale.Guarded, Morality.Forgive);

            Assert.AreEqual(0, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(-125, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Beat, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Exploit, theRelationship.Ethics);
        }

        [TestMethod]
        public void RelationshipChangeTrust_Decrease11_AF()
        {
            theRelationship.ChangeTrust(-11, SuspicionScale.Average, Morality.Forgive);

            Assert.AreEqual(-100, theRelationship.DurabilityOfTrust);
            Assert.AreEqual(-525, theRelationship.DurabilityOfEthics);
            Assert.AreEqual(EthicsScale.Murder, theRelationship.Trust);
            Assert.AreEqual(EthicsScale.Beat, theRelationship.Ethics);
        }

        [TestMethod]
        public void DescribeTrustDurability()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DescribeEthicsDurability()
        {
            throw new NotImplementedException();
        }
    }
}
