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
            theRelationship = new Relationship(0,1,EthicsScale.Coexist, EthicsScale.Coexist);

            Assert.AreEqual(theRelationship.SelfId, 0);
            Assert.AreEqual(theRelationship.OtherId, 1);
            Assert.AreEqual(theRelationship.Trust, EthicsScale.Coexist);
            Assert.AreEqual(theRelationship.Ethics, EthicsScale.Coexist);
        }

        [TestMethod]
        public void WhenRelationshipIsCopied_PropertiesMatch()
        {
            throw new NotImplementedException();
        }
        
        [TestMethod]
        public void RelationshipChangeTrust_Increase()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void RelationshipChangeTrust_Decrease()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void RelationshipChangeTrust_ZeroMagnitude()
        {
            throw new NotImplementedException();
        }
    }
}
