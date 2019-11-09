using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryEngine;
using StoryEngine.SocietyGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class zScratchPaper
    {
        [TestMethod]
        public void aaa__ScratchPaper()
        {
            //Create a simple plot from scratch

            StartingCastGenerator_Default characterFactory = new StartingCastGenerator_Default();
            SocietySnapshot currentCast = characterFactory.CreateStartingCast(10);
            Plot thePlot = new Plot(currentCast);


            var accidentalOffense = createIncidentManually_AccidentalOffense();
            var ableToFillRoles = accidentalOffense.TryToFulfillAllPrerequisites(currentCast);
            
            thePlot.ExecuteIncidentAndStoreAfter(accidentalOffense, currentCast);

            Assert.IsTrue(true);//useless assert
        }

        private IIncident createIncidentManually_AccidentalOffense()
        {
            var accidentalOffense = new Incident();
            //create event from scratch - no prerequisites

            var partyGivingOffense = new Role();
            var partyOffended = new Role();
            partyGivingOffense.MinCount = 1;
            partyOffended.MinCount = 1;

            accidentalOffense.AllParticipants.Add(partyGivingOffense);
            accidentalOffense.AllParticipants.Add(partyOffended);

            Outcome_ChangeTrust smallTrustLoss = new Outcome_ChangeTrust(-1, partyOffended, partyGivingOffense);
            Outcome_ChangeTrust largeTrustLoss = new Outcome_ChangeTrust(-2, partyOffended, partyGivingOffense);
            Outcome_ChangeTrust majorTrustLoss = new Outcome_ChangeTrust(-3, partyOffended, partyGivingOffense);
            Outcome_ChangeTrust reverseTrustLoss = new Outcome_ChangeTrust(-1, partyGivingOffense, partyOffended);

            PossibleResult commonDecrease = new PossibleResult(70);
            commonDecrease.TheOutcomes.Add(smallTrustLoss);

            PossibleResult unlikelyDecrease = new PossibleResult(20);
            unlikelyDecrease.TheOutcomes.Add(largeTrustLoss);

            PossibleResult rareDecrease = new PossibleResult(10);
            rareDecrease.TheOutcomes.Add(majorTrustLoss);
            rareDecrease.TheOutcomes.Add(reverseTrustLoss);

            accidentalOffense.AllPossibleOutcomes.Add(commonDecrease);
            accidentalOffense.AllPossibleOutcomes.Add(unlikelyDecrease);
            accidentalOffense.AllPossibleOutcomes.Add(rareDecrease);

            return accidentalOffense;
        }
    }
}
