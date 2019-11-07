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
    public class UtIncident
    {
        Incident theIncident;

        [TestInitialize]
        public void TestInitialize()
        {
            Role theRole = new Role();
            List<IPrerequisite> prereqs = new List<IPrerequisite>();
            theIncident = new Incident();

            Assert.IsNotNull(theIncident.AllPossibleOutcomes);
            Assert.IsNotNull(theIncident.MyPrerequisites);
            Assert.IsNotNull(theIncident.AllParticipants);
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

        [TestMethod]
        public void CanAllPrerequisitesBeMet_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CanAllPrerequisitesBeMet_IsFalse_SinglePrereq()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CanAllPrerequisitesBeMet_IsFalse_OneOfThreeFails()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CanAllPrerequisitesBeMet_IsFalse_AllThreeFail()
        {
            throw new NotImplementedException();
        }

    }

}
