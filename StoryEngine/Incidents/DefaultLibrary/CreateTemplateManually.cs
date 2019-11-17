using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Incidents.DefaultLibrary
{
    public static class CreateTemplateManually
    {
        #region CharacterDevelopment
        /*
        Future - NewCharacter?
        Future - RemoveCharacter (death, permanent relocation, etc)
        

        Accidental humiliation
        Internal struggle
        
        Agreement/Promise/contract - offered / accepted / broken
        Impulsive emotional decision
        Epiphany / self-discovery
        Compete for favor - competitors, person they are trying to impress

            3+ roles
            
       Targeted Deception/Revelation (lying, unmasking, gossip) - PartyWhoIsTelling, PartyWhoListens, PartyBeingLiedAbout_OrRevealed
       

        */

        public static TemplateForIncident Conversation_Personal()
        {
            var conversation = new TemplateForIncident("Personal Conversation");

            //Roles
            var conversants = new Role("Conversants") { MinCount = 2, MaxCount = 4 };

            conversation.TheRoles.Add(conversants);

            //Prereqs
            var prereq_CooperativesMinTrust = new MutualTrust_Min(EthicsScale.Exploit, conversants);

            conversation.ThePrerequisites.Add(prereq_CooperativesMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, conversants, conversants, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, conversants, conversants, "Large Bonding");

            var commonBonding = new PossibleResult(80);
            commonBonding.TheOutcomes.Add(bonding_Small);

            var unlikelyBonding = new PossibleResult(20);
            unlikelyBonding.TheOutcomes.Add(bonding_Large);
            
            conversation.ThePossibleResults.Add(commonBonding);
            conversation.ThePossibleResults.Add(unlikelyBonding);

            return conversation;
        }

        public static TemplateForIncident Argument_Personal()
        {
            var argument = new TemplateForIncident("Personal Argument");

            //Roles
            var conversants = new Role("Conversants") { MinCount = 2, MaxCount = 4 };

            argument.TheRoles.Add(conversants);

            //Prereqs
            MutualTrust_Min prereq_CooperativesMinTrust = new MutualTrust_Min(EthicsScale.Exploit, conversants);

            argument.ThePrerequisites.Add(prereq_CooperativesMinTrust);

            //Outcomes
            var distrust_Small = new ChangeInTrust(-1, conversants, conversants, "Small Distrust");
            var distrust_Large = new ChangeInTrust(-2, conversants, conversants, "Large Distrust");

            var common = new PossibleResult(80);
            common.TheOutcomes.Add(distrust_Small);

            var unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(distrust_Large);

            argument.ThePossibleResults.Add(common);
            argument.ThePossibleResults.Add(unlikely);

            return argument;
        }
        
        public static TemplateForIncident Cooperation_Social()
        {
            var socialCooperation = new TemplateForIncident("Social Cooperation");

            //Roles
            var cooperatives = new Role("Cooperatives") { MinCount = 2, MaxCount = null };

            socialCooperation.TheRoles.Add(cooperatives);

            //Prereqs
            var prereq_CooperativesMinTrust = new MutualTrust_Min(EthicsScale.Exploit, cooperatives);

            socialCooperation.ThePrerequisites.Add(prereq_CooperativesMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, cooperatives, cooperatives, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, cooperatives, cooperatives, "Large Bonding");
            var bonding_Major = new ChangeInTrust(3, cooperatives, cooperatives, "Major Bonding");

            var commonBonding = new PossibleResult(70);
            commonBonding.TheOutcomes.Add(bonding_Small);

            var unlikelyBonding = new PossibleResult(20);
            unlikelyBonding.TheOutcomes.Add(bonding_Large);

            var rareBonding = new PossibleResult(10);
            rareBonding.TheOutcomes.Add(bonding_Major);

            socialCooperation.ThePossibleResults.Add(commonBonding);
            socialCooperation.ThePossibleResults.Add(unlikelyBonding);
            socialCooperation.ThePossibleResults.Add(rareBonding);

            return socialCooperation;
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
            var prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Exploit, partyAttacking, partyDefending);
            var prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Exploit, partyAttacking);
            var prereq_DefenderMinTrust = new MutualTrust_Min(EthicsScale.Exploit, partyDefending);

            socialAgression.ThePrerequisites.Add(prereqEthicsMax);
            socialAgression.ThePrerequisites.Add(prereq_AttackerMinTrust);
            socialAgression.ThePrerequisites.Add(prereq_DefenderMinTrust);

            //Add outcomes
            ChangeInTrust smallTrustLoss = new ChangeInTrust(-1, partyDefending, partyAttacking, "Small Trust Loss");
            ChangeInTrust largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");
            ChangeInTrust reverseTrustLoss = new ChangeInTrust(-1, partyAttacking, partyDefending, "Reciprocal Trust Loss");
            ChangeInTrust defendersBonding_Small = new ChangeInTrust(1, partyDefending, partyDefending, "Small Defender Bonding");
            ChangeInTrust defendersBonding_Large = new ChangeInTrust(2, partyDefending, partyDefending, "Large Defender Bonding");
            ChangeInTrust defendersBonding_Major = new ChangeInTrust(3, partyDefending, partyDefending, "Major Defender Bonding");

            PossibleResult common = new PossibleResult(70);
            common.TheOutcomes.Add(smallTrustLoss);
            common.TheOutcomes.Add(defendersBonding_Small);

            PossibleResult unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(largeTrustLoss);
            unlikely.TheOutcomes.Add(defendersBonding_Large);

            PossibleResult rare = new PossibleResult(10);
            rare.TheOutcomes.Add(majorTrustLoss);
            rare.TheOutcomes.Add(reverseTrustLoss);
            rare.TheOutcomes.Add(defendersBonding_Major);

            socialAgression.ThePossibleResults.Add(common);
            socialAgression.ThePossibleResults.Add(unlikely);
            socialAgression.ThePossibleResults.Add(rare);

            return socialAgression;
        }

        public static TemplateForIncident Deception()//#TODO - add triggers, make success/fail rate based on duration of lie
        {
            var socialAgression = new TemplateForIncident("Deception"); 

            //Add roles
            var partyAttacking = new Role("Party Deceiving") { MinCount = 1, MaxCount = null };
            var partyDefending = new Role("Party Being Deceived") { MinCount = 1, MaxCount = null };

            socialAgression.TheRoles.Add(partyAttacking);
            socialAgression.TheRoles.Add(partyDefending);

            //Add prereqs
            DirectionalEthics_Max prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Embrace, partyAttacking, partyDefending);
            MutualTrust_Min prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);

            socialAgression.ThePrerequisites.Add(prereqEthicsMax);
            socialAgression.ThePrerequisites.Add(prereq_AttackerMinTrust);

            //Add outcomes
            ChangeInTrust bonding_Small = new ChangeInTrust(1, partyDefending, partyAttacking, "Believed - Small Bonding");
            ChangeInTrust bonding_Large = new ChangeInTrust(2, partyDefending, partyAttacking, "Believed - Large Bonding");
            ChangeInTrust distrust_Large = new ChangeInTrust(-2, partyDefending, partyAttacking, "Caught - Large Trust Loss");

            PossibleResult common_Believed = new PossibleResult(60);
            common_Believed.TheOutcomes.Add(bonding_Small);

            PossibleResult unlikely_Believed = new PossibleResult(20);
            unlikely_Believed.TheOutcomes.Add(bonding_Large);

            PossibleResult caught = new PossibleResult(20);
            caught.TheOutcomes.Add(distrust_Large);

            socialAgression.ThePossibleResults.Add(common_Believed);
            socialAgression.ThePossibleResults.Add(unlikely_Believed);
            socialAgression.ThePossibleResults.Add(caught);

            return socialAgression;
        }
        
        public static TemplateForIncident SacrificeForOther()
        {
            var sacrificeForOther = new TemplateForIncident("Sacrifice for Other(s)");

            //Add roles
            var givers = new Role("Giver(s)") { MinCount = 1, MaxCount = null };
            var getters = new Role("Benefactor(s)") { MinCount = 1, MaxCount = null };

            sacrificeForOther.TheRoles.Add(givers);
            sacrificeForOther.TheRoles.Add(getters);

            //Add prereqs
            var prereq_GiverEthicsMin = new DirectionalEthics_Min(EthicsScale.Cooperate, givers, getters);
            var prereq_GiverMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, givers);

            sacrificeForOther.ThePrerequisites.Add(prereq_GiverEthicsMin);
            sacrificeForOther.ThePrerequisites.Add(prereq_GiverMinTrust);

            //Add outcomes
            ChangeInTrust bonding_Small = new ChangeInTrust(1, getters, givers, "Small Bonding");
            ChangeInTrust bonding_Large = new ChangeInTrust(2, getters, givers, "Large Bonding");
            ChangeInTrust bonding_Major = new ChangeInTrust(3, getters, givers, "Major Bonding");

            PossibleResult common = new PossibleResult(70);
            common.TheOutcomes.Add(bonding_Small);

            PossibleResult unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(bonding_Large);

            PossibleResult rare = new PossibleResult(10);
            rare.TheOutcomes.Add(bonding_Major);

            sacrificeForOther.ThePossibleResults.Add(common);
            sacrificeForOther.ThePossibleResults.Add(unlikely);
            sacrificeForOther.ThePossibleResults.Add(rare);

            return sacrificeForOther;
        }

        public static TemplateForIncident Rejection_Social()
        {
            var socialRejection = new TemplateForIncident("Social Rejection");

            //Add roles
            var partyAttacking = new Role("Party Rejecting") { MinCount = 1, MaxCount = null };
            var partyDefending = new Role("Party Being Rejected") { MinCount = 1, MaxCount = null };

            socialRejection.TheRoles.Add(partyAttacking);
            socialRejection.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Coexist, partyAttacking, partyDefending);
            var prereq_DefenderTrustMin = new DirectionalTrust_Min(EthicsScale.Cooperate, partyAttacking, partyDefending);
            var prereq_AttackerMutualTrust = new MutualTrust_Min(EthicsScale.Exploit, partyAttacking);

            socialRejection.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            socialRejection.ThePrerequisites.Add(prereq_DefenderTrustMin);
            socialRejection.ThePrerequisites.Add(prereq_AttackerMutualTrust);

            //Add outcomes
            var smallTrustLoss = new ChangeInTrust(-1, partyDefending, partyAttacking, "Small Trust Loss");
            var largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            var majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");
            
            var common = new PossibleResult(70);
            common.TheOutcomes.Add(smallTrustLoss);

            var unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(largeTrustLoss);

            var rare = new PossibleResult(10);
            rare.TheOutcomes.Add(majorTrustLoss);

            socialRejection.ThePossibleResults.Add(common);
            socialRejection.ThePossibleResults.Add(unlikely);
            socialRejection.ThePossibleResults.Add(rare);

            return socialRejection;
        }

        public static TemplateForIncident Rejection_Emotional()
        {
            var emotionalRejection = new TemplateForIncident("Emotional Rejection");

            //Add roles
            var partyAttacking = new Role("Party Rejecting") { MinCount = 1, MaxCount = null };
            var partyDefending = new Role("Party Being Rejected") { MinCount = 1, MaxCount = null };

            emotionalRejection.TheRoles.Add(partyAttacking);
            emotionalRejection.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Embrace, partyAttacking, partyDefending);
            var prereq_DefenderTrustMin = new DirectionalTrust_Min(EthicsScale.Embrace, partyAttacking, partyDefending);
            var prereq_AttackerMutualTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);

            emotionalRejection.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            emotionalRejection.ThePrerequisites.Add(prereq_DefenderTrustMin);
            emotionalRejection.ThePrerequisites.Add(prereq_AttackerMutualTrust);

            //Add outcomes
            var smallTrustLoss = new ChangeInTrust(-1, partyDefending, partyAttacking, "Small Trust Loss");
            var largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            var majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");

            var common = new PossibleResult(70);
            common.TheOutcomes.Add(smallTrustLoss);

            var unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(largeTrustLoss);

            var rare = new PossibleResult(10);
            rare.TheOutcomes.Add(majorTrustLoss);

            emotionalRejection.ThePossibleResults.Add(common);
            emotionalRejection.ThePossibleResults.Add(unlikely);
            emotionalRejection.ThePossibleResults.Add(rare);

            return emotionalRejection;
        }

        public static TemplateForIncident Betrayal_Social()//#TODO - make magnitude based on how strong trust was
        {
            var socialBetrayal = new TemplateForIncident("Social Betrayal");

            //Add roles
            var partyAttacking = new Role("Party Betraying") { MinCount = 1, MaxCount = null };
            var partyDefending = new Role("Party Being Betrayed") { MinCount = 1, MaxCount = null };

            socialBetrayal.TheRoles.Add(partyAttacking);
            socialBetrayal.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Exploit, partyAttacking, partyDefending);
            var prereq_DefenderTrustMin = new DirectionalTrust_Min(EthicsScale.Cooperate, partyAttacking, partyDefending);
            var prereq_AttackerMutualTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);

            socialBetrayal.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            socialBetrayal.ThePrerequisites.Add(prereq_DefenderTrustMin);
            socialBetrayal.ThePrerequisites.Add(prereq_AttackerMutualTrust);

            //Add outcomes
            var largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            var majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");

            var common = new PossibleResult(60);
            common.TheOutcomes.Add(largeTrustLoss);

            var unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(majorTrustLoss);
            
            socialBetrayal.ThePossibleResults.Add(common);
            socialBetrayal.ThePossibleResults.Add(unlikely);

            return socialBetrayal;
        }

        public static TemplateForIncident Betrayal_Emotional()//#TODO - make magnitude based on how strong trust was
        {
            var emotionalBetrayal = new TemplateForIncident("Emotional Betrayal");

            //Add roles
            var partyAttacking = new Role("Party Betraying") { MinCount = 1, MaxCount = null };
            var partyDefending = new Role("Party Being Betrayed") { MinCount = 1, MaxCount = null };

            emotionalBetrayal.TheRoles.Add(partyAttacking);
            emotionalBetrayal.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Cooperate, partyAttacking, partyDefending);
            var prereq_DefenderTrustMin = new DirectionalTrust_Min(EthicsScale.Embrace, partyAttacking, partyDefending);
            var prereq_AttackerMutualTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);

            emotionalBetrayal.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            emotionalBetrayal.ThePrerequisites.Add(prereq_DefenderTrustMin);
            emotionalBetrayal.ThePrerequisites.Add(prereq_AttackerMutualTrust);

            //Add outcomes
            var largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            var majorTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Major Trust Loss");
            var massiveTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Massive Trust Loss");

            var common = new PossibleResult(50);
            common.TheOutcomes.Add(largeTrustLoss);

            var unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(massiveTrustLoss);

            var rare = new PossibleResult(10);
            rare.TheOutcomes.Add(massiveTrustLoss);

            emotionalBetrayal.ThePossibleResults.Add(common);
            emotionalBetrayal.ThePossibleResults.Add(unlikely);

            return emotionalBetrayal;
        }
        
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
            ChangeInTrust smallTrustLoss = new ChangeInTrust(-1, partyOffended, partyGivingOffense, "Small Trust Loss");
            ChangeInTrust largeTrustLoss = new ChangeInTrust(-2, partyOffended, partyGivingOffense, "Large Trust Loss");
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, partyOffended, partyGivingOffense, "Major Trust Loss");
            ChangeInTrust reverseTrustLoss = new ChangeInTrust(-1, partyGivingOffense, partyOffended, "Reciprocal Trust Loss");

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


        #endregion

        #region Generic
        /*
       Travel
       Interruption of routine task
       Self improvement - practice / study / research
       Coached improvement - training / school
       Send Message
       Message lost in transit, never received
       Receive message - New rumor / word of remote incident
       Build/acquire tool
       Equipment failure
       Accidental injury
       Rest, recover, heal
       Social gathering
       Windfall, find/win item of value
       Organized Competition
       
         
         */
        #endregion

        #region Action
        /*

        Aggression - violent
        Betrayal - violent
        Chase fleeing agent / Run from pursuit
        Injury / poisoning
        Criminal activity (non-violent)
        Framed
        Hide in secret place
        Seek refuge
        Captured / Trapped / Imprisoned
        EscapeLocation
        Rescue
        Surrender
        Enslaved
        Sabotague
       Public unrest - riot / rebellion / revolution
        */
        #endregion

        #region Mystery
        /*
        
       Search for hidden person/object/information
        Riddle to solve / mystery

         */
        #endregion

        #region ConflictWithNature
        /*
       Weather - delay/discomfort
       Lost
       Survival - search for food, water, shelter
       Natural disaster
       Disease / illness
       Encounter wild animal - in wild or loose in populated area
       */
        #endregion

        /*




       3+ Role Incidents
       Interrupted Aggression - Attacker, Victim, Protector / Rescuer
       {anything normal event with added observers / witnesses}

        */

    }
}
