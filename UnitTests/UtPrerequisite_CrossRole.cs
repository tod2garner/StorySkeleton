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
    public class UtPrerequisite_CrossRole
    {
        Mocks.Mock_Prereq_DirectionalEthics_Max maxEthics;

        [TestInitialize]
        public void TestInitialize()
        {
            EthicsScale givenMax = EthicsScale.Exploit;
            Role role1 = new Role();
            Role role2 = new Role();
            maxEthics = new Mocks.Mock_Prereq_DirectionalEthics_Max(role1, role2, givenMax);

            Assert.AreEqual(maxEthics.GetBenchmark_AtoB(), givenMax);
            Assert.IsNotNull(maxEthics.GetRoleA());
            Assert.IsNotNull(maxEthics.GetRoleB());
        }

        [TestMethod]
        public void PassesBenchmark_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PassesBenchmark_IsFalse()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void HasDirectionalRelationThatPassesBenchmark_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void HasDirectionalRelationThatPassesBenchmark_IsFalse()
        {
            throw new NotImplementedException();
        }
        
        [TestMethod]
        public void AddParticipantsRandomly_WhenMaxIsZero_NoneAreAdded()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddParticipantsRandomly_WhenMaxIsNull_DefaultMaxApplies()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddParticipantsRandomly_WhenMinIsNull_StillAdds()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddParticipantsRandomly_ResultsIsWithinMinMaxRange()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AreRoleMinMaxCountsMet_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AreRoleMinMaxCountsMet_IsFalse()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void IsMetByCurrentParticipants_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void IsMetByCurrentParticipants_IsFalse()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TryToFulfillFromScratch_Succeeds()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TryToFulfillFromScratch_Fails()
        {
            throw new NotImplementedException();
        }
    }
}
