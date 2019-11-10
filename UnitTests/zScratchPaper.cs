using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryEngine;
using StoryEngine.SocietyGenerators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            int characterCount = 15;
            StartingCastGenerator_Default characterFactory = new StartingCastGenerator_Default();
            SocietySnapshot currentCast = characterFactory.CreateStartingCast(characterCount);
            Plot thePlot = new Plot(currentCast);

            //Prepare psuedo "event library"            
            var accidentalOffense = createIncidentManually_AccidentalOffense();
            var socialAgression = createIncidentManually_Agression_Social();
            var socialCooperation = createIncidentManually_Cooperation_Social();

            //Create sequence of events
            Random rng = new Random();

            //Event #1
            var ableToFillRoles = accidentalOffense.TryToPopulateIncident(currentCast);
            Assert.IsTrue(ableToFillRoles);            
            thePlot.ExecuteIncidentAndStoreAfter(accidentalOffense, currentCast, rng);
            
            //Event #2
            ableToFillRoles = socialAgression.TryToPopulateIncident(currentCast);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialAgression, currentCast, rng);
            
            //Event #3
            ableToFillRoles = socialCooperation.TryToPopulateIncident(currentCast);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialCooperation, currentCast, rng);

            //Event #4
            ableToFillRoles = socialAgression.TryToPopulateIncident(currentCast);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialAgression, currentCast, rng);

            //Event #5
            ableToFillRoles = socialCooperation.TryToPopulateIncident(currentCast);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialCooperation, currentCast, rng);
            

            //Display narrative
            var theTextNarrative = thePlot.CompileTextNarrative();
            foreach(string s in theTextNarrative)
                Debug.WriteLine(s);

            Assert.IsTrue(true);
        }

        private IIncident createIncidentManually_AccidentalOffense()
        {
            var accidentalOffense = new Incident("Accidental Offense");
            //create event from scratch - no prerequisites

            var partyGivingOffense = new Role("partyGivingOffense");
            var partyOffended = new Role("partyOffended");
            partyGivingOffense.MinCount = 1;
            partyOffended.MinCount = 1;

            accidentalOffense.AllParticipantRoles.Add(partyGivingOffense);
            accidentalOffense.AllParticipantRoles.Add(partyOffended);

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
            var socialAgression = new Incident("Social Agression");
            
            //Add roles
            var partyAttacking = new Role("partyAttacking");
            var partyDefending = new Role("partyDefending");
            partyAttacking.MinCount = 1;
            partyDefending.MinCount = 1;

            socialAgression.AllParticipantRoles.Add(partyAttacking);
            socialAgression.AllParticipantRoles.Add(partyDefending);

            //Add prereqs
            DirectionalEthics_Max prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Exploit, partyAttacking, partyDefending);
            MutualTrust_Min prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Exploit, partyAttacking);
            MutualTrust_Min prereq_DefenderMinTrust = new MutualTrust_Min(EthicsScale.Exploit, partyDefending);

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
            var socialCooperation = new Incident("Social Cooperation");
            
            //Add roles
            var cooperatives = new Role("cooperatives");
            cooperatives.MinCount = 2;

            socialCooperation.AllParticipantRoles.Add(cooperatives);

            //Add prereqs
            MutualTrust_Min prereq_CooperativesMinTrust = new MutualTrust_Min(EthicsScale.Exploit, cooperatives);

            socialCooperation.MyPrerequisites.Add(prereq_CooperativesMinTrust);

            //Add outcomes
            Outcome_ChangeTrust cooperativesBonding_Small = new Outcome_ChangeTrust(1, cooperatives, cooperatives);
            Outcome_ChangeTrust cooperativesBonding_Large = new Outcome_ChangeTrust(2, cooperatives, cooperatives);
            Outcome_ChangeTrust cooperativesBonding_Major = new Outcome_ChangeTrust(3, cooperatives, cooperatives);

            PossibleResult commonBonding = new PossibleResult(70);
            commonBonding.TheOutcomes.Add(cooperativesBonding_Small);

            PossibleResult unlikelyBonding = new PossibleResult(20);
            unlikelyBonding.TheOutcomes.Add(cooperativesBonding_Large);

            PossibleResult rareBonding = new PossibleResult(10);
            rareBonding.TheOutcomes.Add(cooperativesBonding_Major);

            socialCooperation.AllPossibleOutcomes.Add(commonBonding);
            socialCooperation.AllPossibleOutcomes.Add(unlikelyBonding);
            socialCooperation.AllPossibleOutcomes.Add(rareBonding);

            return socialCooperation;
        }
    }
}
