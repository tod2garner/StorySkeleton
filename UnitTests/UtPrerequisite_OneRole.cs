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
    public class UtPrerequisite_OneRole
    {
        Mocks.Mock_Prereq_MutualTrust_Min minMutualTrust;

        [TestInitialize]
        public void TestInitialize()
        {
            EthicsScale givenMin = EthicsScale.Coexist;
            Role theRole = new Role();
            minMutualTrust = new Mocks.Mock_Prereq_MutualTrust_Min(givenMin, theRole);

            Assert.AreEqual(minMutualTrust.GetBenchmark(), givenMin);
            Assert.IsNotNull(minMutualTrust.GetRole());
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
        public void HaveMutualTrustThatPassesBenchmark_IsTrue()
        {            
            throw new NotImplementedException();
        }

        [TestMethod]
        public void HaveMutualTrustThatPassesBenchmark_IsFalse()
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
