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
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(12));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(13));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(15));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(25));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(35));

            Assert.IsTrue(theIncident.IsOutcomeTotal100Percent());
        }

        [TestMethod]
        public void IsOutcomeTotal100Percent_IsFalse()
        {
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(12));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(13));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(15));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(25));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(34));

            Assert.IsFalse(theIncident.IsOutcomeTotal100Percent());
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
