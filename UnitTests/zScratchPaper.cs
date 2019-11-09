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

            int characterCount = 10;
            StartingCastGenerator_Default characterFactory = new StartingCastGenerator_Default();
            SocietySnapshot currentCast = characterFactory.CreateStartingCast(characterCount);
            Plot thePlot = new Plot(currentCast);

            //Prepare psuedo "event library"            
            var accidentalOffense = createIncidentManually_AccidentalOffense();
            var socialAgression = createIncidentManually_Agression_Social();
            var socialCooperation = createIncidentManually_Cooperation_Social();

            //Event #1
            var ableToFillRoles = accidentalOffense.TryToFulfillAllPrerequisites(currentCast);
            Assert.IsTrue(ableToFillRoles);            
            thePlot.ExecuteIncidentAndStoreAfter(accidentalOffense, currentCast);

            //Event #2
            ableToFillRoles = socialAgression.TryToFulfillAllPrerequisites(currentCast);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialAgression, currentCast);

            //Event #3
            ableToFillRoles = socialCooperation.TryToFulfillAllPrerequisites(currentCast);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialCooperation, currentCast);

            //Event #4
            ableToFillRoles = socialAgression.TryToFulfillAllPrerequisites(currentCast);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialAgression, currentCast);

            //Event #5
            ableToFillRoles = socialCooperation.TryToFulfillAllPrerequisites(currentCast);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialCooperation, currentCast);

            //Display narrative
            var theTextNarrative = thePlot.CompileTextNarrative();
            foreach(string s in theTextNarrative)
                Console.Error.Write(s);

            Assert.IsTrue(false);
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

        private IIncident createIncidentManually_Agression_Social()
        {
            var socialAgression = new Incident();
            
            //Add roles
            var partyAttacking = new Role();
            var partyDefending = new Role();
            partyAttacking.MinCount = 1;
            partyDefending.MinCount = 1;

            socialAgression.AllParticipants.Add(partyAttacking);
            socialAgression.AllParticipants.Add(partyDefending);

            //Add prereqs
            DirectionalEthics_Max prereqEthicsMax = new DirectionalEthics_Max(partyAttacking, partyDefending, EthicsScale.Exploit);
            MutualTrust_Min prereq_AttackerMinTrust = new MutualTrust_Min(EthicsSCale.Exploit, partyAttacking);
            MutualTrust_Min prereq_DefenderMinTrust = new MutualTrust_Min(EthicsSCale.Exploit, partyDefending);

            socialAgression.MyPrerequisites.Add(prereqEthicsMax);
            socialAgression.MyPrerequisites.Add(prereq_AttackerMinTrust);
            socialAgression.MyPrerequisites.Add(prereq_DefenderMinTrust);

            //Add outcomes
            Outcome_ChangeTrust smallTrustLoss = new Outcome_ChangeTrust(-1, partyDefending, partyAttacking);
            Outcome_ChangeTrust largeTrustLoss = new Outcome_ChangeTrust(-2, partyDefending, partyAttacking);
            Outcome_ChangeTrust majorTrustLoss = new Outcome_ChangeTrust(-3, partyDefending, partyAttacking);
            Outcome_ChangeTrust reverseTrustLoss = new Outcome_ChangeTrust(-1, partyAttacking, partyDefending);
            Outcome_ChangeTrust defendersBonding_Small = new Outcome_ChangeTrust(1, partyDefending, partyDefending);
            Outcome_ChangeTrust defendersBonding_Large = new Outcome_ChangeTrust(2, partyDefending, partyDefending);
            Outcome_ChangeTrust defendersBonding_Major = new Outcome_ChangeTrust(3, partyDefending, partyDefending);

            PossibleResult commonDecrease = new PossibleResult(70);
            commonDecrease.TheOutcomes.Add(smallTrustLoss);
            commonDecrease.TheOutcomes.Add(defendersBonding_Small);

            PossibleResult unlikelyDecrease = new PossibleResult(20);
            unlikelyDecrease.TheOutcomes.Add(largeTrustLoss);
            unlikelyDecrease.TheOutcomes.Add(defendersBonding_Large);

            PossibleResult rareDecrease = new PossibleResult(10);
            rareDecrease.TheOutcomes.Add(majorTrustLoss);
            rareDecrease.TheOutcomes.Add(reverseTrustLoss);
            rareDecrease.TheOutcomes.Add(defendersBonding_Major);

            socialAgression.AllPossibleOutcomes.Add(commonDecrease);
            socialAgression.AllPossibleOutcomes.Add(unlikelyDecrease);
            socialAgression.AllPossibleOutcomes.Add(rareDecrease);

            return socialAgression;
        }

        private IIncident createIncidentManually_Cooperation_Social()
        {
            var socialCooperation = new Incident();
            
            //Add roles
            var cooperatives = new Role();
            cooperatives.MinCount = 2;

            socialCooperation.AllParticipants.Add(cooperatives);

            //Add prereqs
            MutualTrust_Min prereq_CooperativesMinTrust = new MutualTrust_Min(EthicsSCale.Exploit, cooperatives);

            socialCooperation.MyPrerequisites.Add(prereq_CooperativesMinTrust);

            //Add outcomes
            Outcome_ChangeTrust cooperativesBonding_Small = new Outcome_ChangeTrust(1, cooperatives, cooperatives);
            Outcome_ChangeTrust cooperativesBonding_Large = new Outcome_ChangeTrust(2, cooperatives, cooperatives);
            Outcome_ChangeTrust cooperativesBonding_Major = new Outcome_ChangeTrust(3, cooperatives, cooperatives);

            PossibleResult commonBonding = new PossibleResult(70);
            commonBonding.TheOutcomes.Add(defendersBonding_Small);

            PossibleResult unlikelyBonding = new PossibleResult(20);
            unlikelyBonding.TheOutcomes.Add(defendersBonding_Large);

            PossibleResult rareBonding = new PossibleResult(10);
            rareBonding.TheOutcomes.Add(defendersBonding_Major);

            socialCooperation.AllPossibleOutcomes.Add(commonBonding);
            socialCooperation.AllPossibleOutcomes.Add(unlikelyBonding);
            socialCooperation.AllPossibleOutcomes.Add(rareBonding);

            return socialCooperation;
        }
    }
}
