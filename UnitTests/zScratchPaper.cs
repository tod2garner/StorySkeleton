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
        public void aaa__aSaveTemplateToFile()
        {
            var t1 = createTemplateManually_AccidentalOffense();
            var t2 = createTemplateManually_Agression_Social();
            var t3 = createTemplateManually_Cooperation_Social();

            var c1 = new CollectionOfIncidentTemplates();
            c1.TheTemplates.Add(t1);
            c1.TheTemplates.Add(t2);
            c1.TheTemplates.Add(t3);

            t1.SaveToXML("C:\\temp\\t1.xml");
            t2.SaveToXML("C:\\temp\\t2.xml");
            t3.SaveToXML("C:\\temp\\t3.xml");
            c1.SaveToXML("C:\\temp\\c1.xml");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void aaa__ScratchPaper_PlotFromScratch()
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
            //NOTE - must create NEW instace of event for each use (or create a way to reset participants)

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
            socialAgression = createIncidentManually_Agression_Social();
            ableToFillRoles = socialAgression.TryToPopulateIncident(currentCast);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialAgression, currentCast, rng);

            //Event #5
            socialCooperation = createIncidentManually_Cooperation_Social();
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
            return createTemplateManually_AccidentalOffense().CreateIncident();
        }
         
        private TemplateForIncident createTemplateManually_AccidentalOffense()
        {
            //Assign name
            var accidentalOffense = new TemplateForIncident("Accidental Offense");
            
            //Add roles
            var partyGivingOffense = new Role("Party Giving Offense");
            var partyOffended = new Role("Party Offended");
            partyGivingOffense.MinCount = 1;
            partyOffended.MinCount = 1;

            accidentalOffense.TheRoles.Add(partyGivingOffense);
            accidentalOffense.TheRoles.Add(partyOffended);

            //No prereqs

            //Add outcomes
            Outcome_ChangeTrust smallTrustLoss = new Outcome_ChangeTrust(-1, partyOffended, partyGivingOffense, "Small Trust Loss");
            Outcome_ChangeTrust largeTrustLoss = new Outcome_ChangeTrust(-2, partyOffended, partyGivingOffense, "Large Trust Loss");
            Outcome_ChangeTrust majorTrustLoss = new Outcome_ChangeTrust(-3, partyOffended, partyGivingOffense, "Major Trust Loss");
            Outcome_ChangeTrust reverseTrustLoss = new Outcome_ChangeTrust(-1, partyGivingOffense, partyOffended, "Reciprocal Trust Loss");

            PossibleResult commonDecrease = new PossibleResult(70);
            commonDecrease.TheOutcomes.Add(smallTrustLoss);

            PossibleResult unlikelyDecrease = new PossibleResult(20);
            unlikelyDecrease.TheOutcomes.Add(largeTrustLoss);

            PossibleResult rareDecrease = new PossibleResult(10);
            rareDecrease.TheOutcomes.Add(majorTrustLoss);
            rareDecrease.TheOutcomes.Add(reverseTrustLoss);

            accidentalOffense.ThePossibleResults.Add(commonDecrease);
            accidentalOffense.ThePossibleResults.Add(unlikelyDecrease);
            accidentalOffense.ThePossibleResults.Add(rareDecrease);

            return accidentalOffense;
        }

        private IIncident createIncidentManually_Agression_Social()
        {
            return createTemplateManually_Agression_Social().CreateIncident();
        }

        private TemplateForIncident createTemplateManually_Agression_Social()
        {
            var socialAgression = new TemplateForIncident("Social Agression");
            
            //Add roles
            var partyAttacking = new Role("partyAttacking");
            var partyDefending = new Role("partyDefending");
            partyAttacking.MinCount = 1;
            partyDefending.MinCount = 1;

            socialAgression.TheRoles.Add(partyAttacking);
            socialAgression.TheRoles.Add(partyDefending);

            //Add prereqs
            DirectionalEthics_Max prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Exploit, partyAttacking, partyDefending);
            MutualTrust_Min prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Exploit, partyAttacking);
            MutualTrust_Min prereq_DefenderMinTrust = new MutualTrust_Min(EthicsScale.Exploit, partyDefending);

            socialAgression.ThePrerequisites.Add(prereqEthicsMax);
            socialAgression.ThePrerequisites.Add(prereq_AttackerMinTrust);
            socialAgression.ThePrerequisites.Add(prereq_DefenderMinTrust);

            //Add outcomes
            Outcome_ChangeTrust smallTrustLoss = new Outcome_ChangeTrust(-1, partyDefending, partyAttacking, "Small Trust Loss");
            Outcome_ChangeTrust largeTrustLoss = new Outcome_ChangeTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            Outcome_ChangeTrust majorTrustLoss = new Outcome_ChangeTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");
            Outcome_ChangeTrust reverseTrustLoss = new Outcome_ChangeTrust(-1, partyAttacking, partyDefending, "Reciprocal Trust Loss");
            Outcome_ChangeTrust defendersBonding_Small = new Outcome_ChangeTrust(1, partyDefending, partyDefending, "Small Defender Bonding");
            Outcome_ChangeTrust defendersBonding_Large = new Outcome_ChangeTrust(2, partyDefending, partyDefending, "Large Defender Bonding");
            Outcome_ChangeTrust defendersBonding_Major = new Outcome_ChangeTrust(3, partyDefending, partyDefending, "Major Defender Bonding");

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

            socialAgression.ThePossibleResults.Add(commonDecrease);
            socialAgression.ThePossibleResults.Add(unlikelyDecrease);
            socialAgression.ThePossibleResults.Add(rareDecrease);

            return socialAgression;
        }

        private IIncident createIncidentManually_Cooperation_Social()
        {
            return createTemplateManually_Cooperation_Social().CreateIncident();
        }

        private TemplateForIncident createTemplateManually_Cooperation_Social()
        {
            var socialCooperation = new TemplateForIncident("Social Cooperation");
            
            //Add roles
            var cooperatives = new Role("cooperatives");
            cooperatives.MinCount = 2;

            socialCooperation.TheRoles.Add(cooperatives);

            //Add prereqs
            MutualTrust_Min prereq_CooperativesMinTrust = new MutualTrust_Min(EthicsScale.Exploit, cooperatives);

            socialCooperation.ThePrerequisites.Add(prereq_CooperativesMinTrust);

            //Add outcomes
            Outcome_ChangeTrust cooperativesBonding_Small = new Outcome_ChangeTrust(1, cooperatives, cooperatives, "Small Bonding");
            Outcome_ChangeTrust cooperativesBonding_Large = new Outcome_ChangeTrust(2, cooperatives, cooperatives, "Large Bonding");
            Outcome_ChangeTrust cooperativesBonding_Major = new Outcome_ChangeTrust(3, cooperatives, cooperatives, "Major Bonding");

            PossibleResult commonBonding = new PossibleResult(70);
            commonBonding.TheOutcomes.Add(cooperativesBonding_Small);

            PossibleResult unlikelyBonding = new PossibleResult(20);
            unlikelyBonding.TheOutcomes.Add(cooperativesBonding_Large);

            PossibleResult rareBonding = new PossibleResult(10);
            rareBonding.TheOutcomes.Add(cooperativesBonding_Major);

            socialCooperation.ThePossibleResults.Add(commonBonding);
            socialCooperation.ThePossibleResults.Add(unlikelyBonding);
            socialCooperation.ThePossibleResults.Add(rareBonding);

            return socialCooperation;
        }
    }
}
