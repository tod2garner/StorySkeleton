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
    public class UnitTests1
    {
        [TestMethod]
        public void ScratchPaper()
        {
            IncidentRole hunters = new IncidentRole();
            hunters.AllPossibleOutcomes.Add(new PossibleResult(30, new Outcome_ChangeTrust_Mutual(1, hunters.Participants, hunters.Participants)));
            hunters.AllPossibleOutcomes.Add(new PossibleResult(30, new Outcome_ChangeTrust_Mutual(-1, hunters.Participants, hunters.Participants)));
            hunters.AllPossibleOutcomes.Add(new PossibleResult(40, new Outcome_ChangeTrust_Mutual(0, hunters.Participants, hunters.Participants)));

            List<IPrerequisite> thePrereqs = new List<IPrerequisite>();
            thePrereqs.Add(new MutualTrust_Min(EthicsScale.Cooperate, hunters));

            Incident_OneRole theIncident = new Incident_OneRole(hunters, thePrereqs);

            Assert.IsTrue(true);//useless assert, only for breakpoint
        }
    }
}
