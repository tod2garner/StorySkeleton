using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Incidents.DefaultLibrary
{
    public static class CreateTemplateManually
    {

        public static TemplateForIncident AccidentalOffense()
        {
            //Assign name
            var accidentalOffense = new TemplateForIncident("Accidental Offense");

            //Add roles
            var partyGivingOffense = new Role("Party Giving Offense") { MinCount = 1, MaxCount = null };
            var partyOffended = new Role("Party Offended") { MinCount = 1, MaxCount = null };

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
        
        public static TemplateForIncident Agression_Social()
        {
            var socialAgression = new TemplateForIncident("Social Agression");

            //Add roles
            var partyAttacking = new Role("Party Attacking") { MinCount = 1, MaxCount = null };
            var partyDefending = new Role("Party Defending") { MinCount = 1, MaxCount = null };

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

        public static TemplateForIncident Cooperation_Social()
        {
            var socialCooperation = new TemplateForIncident("Social Cooperation");

            //Roles
            var cooperatives = new Role("Cooperatives") { MinCount = 2, MaxCount = null };

            socialCooperation.TheRoles.Add(cooperatives);

            //Prereqs
            MutualTrust_Min prereq_CooperativesMinTrust = new MutualTrust_Min(EthicsScale.Exploit, cooperatives);

            socialCooperation.ThePrerequisites.Add(prereq_CooperativesMinTrust);

            //Outcomes
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
