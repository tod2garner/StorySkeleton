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
    public class UtRole
    {
        IncidentRole theRole;

        [TestInitialize]
        public void TestInitialize()
        {
            theRole = new IncidentRole();

            Assert.IsNotNull(theRole.Participants);
            Assert.IsNotNull(theRole.AllPossibleOutcomes);
            Assert.IsNull(theRole.MinCount);
            Assert.IsNull(theRole.MaxCount);
        }

        [TestMethod]
        public void AreMinAndMaxMet_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AreMinAndMaxMet_IsFalse()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void IsOutcomeTotal100Percent_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void IsOutcomeTotal100Percent_IsFalse()
        {
            throw new NotImplementedException();
        }
    }
}
