﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Incidents.DefaultLibrary
{
    public static class CreateTemplateManually
    {
        #region CharacterDevelopment

        public static TemplateForIncident Conversation_Personal()//discussion, request, planning, negotiation
        {
            var conversation = new TemplateForIncident("Personal Conversation");
            conversation.TheFrequency = Frequency.Often;

            //Roles
            var conversants = new Role("Conversants") { MinCount = 2, MaxCount = 3 };

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

        public static TemplateForIncident Argument_Personal()//disagreement, misunderstanding, emotional outburst
        {
            var argument = new TemplateForIncident("Personal Argument");
            argument.TheFrequency = Frequency.Often;
            argument.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var conversants = new Role("Conversants") { MinCount = 2, MaxCount = 3 };

            argument.TheRoles.Add(conversants);

            //Prereqs
            MutualTrust_Min prereq_ConversantMinTrust = new MutualTrust_Min(EthicsScale.Exploit, conversants);

            argument.ThePrerequisites.Add(prereq_ConversantMinTrust);

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

        public static TemplateForIncident Cooperation_Utilitarian()//contract, debt, teaming-up temporarily
        {
            var utilitarianCooperation = new TemplateForIncident("Utilitarian Cooperation");
            utilitarianCooperation.TheFrequency = Frequency.Often;

            //Roles
            var cooperatives = new Role("Cooperatives") { MinCount = 2, MaxCount = null };

            utilitarianCooperation.TheRoles.Add(cooperatives);

            //Prereqs
            var prereq_CooperativesMinTrust = new MutualTrust_Min(EthicsScale.Exploit, cooperatives);

            utilitarianCooperation.ThePrerequisites.Add(prereq_CooperativesMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, cooperatives, cooperatives, "Small Bonding");

            var commonBonding = new PossibleResult(100);
            commonBonding.TheOutcomes.Add(bonding_Small);

            utilitarianCooperation.ThePossibleResults.Add(commonBonding);

            return utilitarianCooperation;
        }

        public static TemplateForIncident Cooperation_Social()//promise, teamwork
        {
            var socialCooperation = new TemplateForIncident("Social Cooperation");
            socialCooperation.TheFrequency = Frequency.Often;
            socialCooperation.IsPleasant = Pleasantness.AlwaysPleasant;

            //Roles
            var cooperatives = new Role("Cooperatives") { MinCount = 2, MaxCount = null };

            socialCooperation.TheRoles.Add(cooperatives);

            //Prereqs
            var prereq_CooperativesMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, cooperatives);

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

        public static TemplateForIncident Aggression_Social()//mockery, public accusation, threats, blackmail
        {
            var socialAggression = new TemplateForIncident("Social Aggression");
            socialAggression.TheFrequency = Frequency.Periodically;
            socialAggression.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var partyAttacking = new Role("Attacker(s)") { MinCount = 0, MaxCount = null };
            var partyDefending = new Role("Defender(s)") { MinCount = 1, MaxCount = null };

            socialAggression.TheRoles.Add(partyAttacking);
            socialAggression.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Exploit, partyAttacking, partyDefending);
            var prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Exploit, partyAttacking);
            var prereq_DefenderMinTrust = new MutualTrust_Min(EthicsScale.Exploit, partyDefending);

            socialAggression.ThePrerequisites.Add(prereqEthicsMax);
            socialAggression.ThePrerequisites.Add(prereq_AttackerMinTrust);
            socialAggression.ThePrerequisites.Add(prereq_DefenderMinTrust);

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

            socialAggression.ThePossibleResults.Add(common);
            socialAggression.ThePossibleResults.Add(unlikely);
            socialAggression.ThePossibleResults.Add(rare);

            return socialAggression;
        }

        //#TODO - add triggers, make success/fail rate based on duration of lie
        public static TemplateForIncident Deception()//lying, framing other, disguise
        {
            var deception = new TemplateForIncident("Deception");
            deception.TheFrequency = Frequency.Periodically;

            //Add roles
            var partyAttacking = new Role("Deceiver(s)") { MinCount = 1, MaxCount = null };
            var partyDefending = new Role("Party Being Deceived") { MinCount = 1, MaxCount = null };

            deception.TheRoles.Add(partyAttacking);
            deception.TheRoles.Add(partyDefending);

            //Add prereqs
            DirectionalEthics_Max prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Befriend, partyAttacking, partyDefending);
            MutualTrust_Min prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);

            deception.ThePrerequisites.Add(prereqEthicsMax);
            deception.ThePrerequisites.Add(prereq_AttackerMinTrust);

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

            deception.ThePossibleResults.Add(common_Believed);
            deception.ThePossibleResults.Add(unlikely_Believed);
            deception.ThePossibleResults.Add(caught);

            return deception;
        }

        public static TemplateForIncident SacrificeForOther()
        {
            var sacrificeForOther = new TemplateForIncident("Sacrifice for Other(s)");
            sacrificeForOther.TheFrequency = Frequency.Rarely;

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
            socialRejection.TheFrequency = Frequency.Rarely;
            socialRejection.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var partyAttacking = new Role("Party Rejecting") { MinCount = 0, MaxCount = null };
            var partyDefending = new Role("Party Being Rejected") { MinCount = 1, MaxCount = null };

            socialRejection.TheRoles.Add(partyAttacking);
            socialRejection.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Coexist, partyAttacking, partyDefending);
            var prereq_DefenderTrustMin = new DirectionalTrust_Min(EthicsScale.Cooperate, partyDefending, partyAttacking);
            var prereq_AttackerMutualTrust = new MutualTrust_Min(EthicsScale.Exploit, partyAttacking);

            socialRejection.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            socialRejection.ThePrerequisites.Add(prereq_DefenderTrustMin);
            socialRejection.ThePrerequisites.Add(prereq_AttackerMutualTrust);

            //Add outcomes
            var smallTrustLoss = new ChangeInTrust(-1, partyDefending, partyAttacking, "Small Trust Loss");
            var largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            var majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");

            var common = new PossibleResult(70);
            common.TheOutcomes.Add(largeTrustLoss);

            var unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(majorTrustLoss);

            var rare = new PossibleResult(10);
            rare.TheOutcomes.Add(smallTrustLoss);

            socialRejection.ThePossibleResults.Add(common);
            socialRejection.ThePossibleResults.Add(unlikely);
            socialRejection.ThePossibleResults.Add(rare);

            return socialRejection;
        }

        public static TemplateForIncident Rejection_Emotional()
        {
            var emotionalRejection = new TemplateForIncident("Emotional Rejection");
            emotionalRejection.TheFrequency = Frequency.Rarely;
            emotionalRejection.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var partyAttacking = new Role("Party Rejecting") { MinCount = 1, MaxCount = null };
            var partyDefending = new Role("Party Being Rejected") { MinCount = 1, MaxCount = null };

            emotionalRejection.TheRoles.Add(partyAttacking);
            emotionalRejection.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Befriend, partyAttacking, partyDefending);
            var prereq_DefenderTrustMin = new DirectionalTrust_Min(EthicsScale.Befriend, partyDefending, partyAttacking);
            var prereq_AttackerMutualTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);

            emotionalRejection.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            emotionalRejection.ThePrerequisites.Add(prereq_DefenderTrustMin);
            emotionalRejection.ThePrerequisites.Add(prereq_AttackerMutualTrust);

            //Add outcomes
            var smallTrustLoss = new ChangeInTrust(-1, partyDefending, partyAttacking, "Small Trust Loss");
            var largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            var majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");

            var common = new PossibleResult(70);
            common.TheOutcomes.Add(largeTrustLoss);

            var unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(majorTrustLoss);

            var rare = new PossibleResult(10);
            rare.TheOutcomes.Add(smallTrustLoss);

            emotionalRejection.ThePossibleResults.Add(common);
            emotionalRejection.ThePossibleResults.Add(unlikely);
            emotionalRejection.ThePossibleResults.Add(rare);

            return emotionalRejection;
        }

        //#TODO - make magnitude based on how strong trust was
        public static TemplateForIncident Betrayal_Social()//broken contract, manipulation
        {
            var socialBetrayal = new TemplateForIncident("Social Betrayal");
            socialBetrayal.TheFrequency = Frequency.ExtremelyRarely;
            socialBetrayal.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var partyAttacking = new Role("Party Betraying") { MinCount = 0, MaxCount = null };
            var partyDefending = new Role("Party Being Betrayed") { MinCount = 1, MaxCount = null };

            socialBetrayal.TheRoles.Add(partyAttacking);
            socialBetrayal.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Exploit, partyAttacking, partyDefending);
            var prereq_DefenderTrustMin = new DirectionalTrust_Min(EthicsScale.Cooperate, partyDefending, partyAttacking);
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

        //#TODO - make magnitude based on how strong trust was
        public static TemplateForIncident Betrayal_Emotional()//broken promise, deception
        {
            var emotionalBetrayal = new TemplateForIncident("Emotional Betrayal");
            emotionalBetrayal.TheFrequency = Frequency.ExtremelyRarely;
            emotionalBetrayal.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var partyAttacking = new Role("Party Betraying") { MinCount = 1, MaxCount = null };
            var partyDefending = new Role("Party Being Betrayed") { MinCount = 1, MaxCount = null };

            emotionalBetrayal.TheRoles.Add(partyAttacking);
            emotionalBetrayal.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Cooperate, partyAttacking, partyDefending);
            var prereq_DefenderTrustMin = new DirectionalTrust_Min(EthicsScale.Befriend, partyDefending, partyAttacking);
            var prereq_AttackerMutualTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);

            emotionalBetrayal.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            emotionalBetrayal.ThePrerequisites.Add(prereq_DefenderTrustMin);
            emotionalBetrayal.ThePrerequisites.Add(prereq_AttackerMutualTrust);

            //Add outcomes
            var largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            var majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");
            var massiveTrustLoss = new ChangeInTrust(-4, partyDefending, partyAttacking, "Massive Trust Loss");

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
            accidentalOffense.TheFrequency = Frequency.Periodically;
            accidentalOffense.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var partyGivingOffense = new Role("Party Giving Offense") { MinCount = 1, MaxCount = null };
            var partyOffended = new Role("Party Offended") { MinCount = 0, MaxCount = null };

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

        public static TemplateForIncident AccidentalEmbarrassment()
        {
            //Assign name
            var accidentalEmbarrassment = new TemplateForIncident("Accidental Embarrassment");
            accidentalEmbarrassment.TheFrequency = Frequency.Periodically;
            accidentalEmbarrassment.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var partyEmbarrassed = new Role("Party Embarrassed") { MinCount = 1, MaxCount = null };

            accidentalEmbarrassment.TheRoles.Add(partyEmbarrassed);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyEmbarrassed);

            accidentalEmbarrassment.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Add outcomes
            ChangeInTrust smallBonding = new ChangeInTrust(1, partyEmbarrassed, partyEmbarrassed, "Small Bonding");
            ChangeInTrust smallTrustLoss = new ChangeInTrust(-1, partyEmbarrassed, partyEmbarrassed, "Small Trust Loss");

            PossibleResult common = new PossibleResult(60);
            common.TheOutcomes.Add(smallBonding);

            PossibleResult unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(smallTrustLoss);

            accidentalEmbarrassment.ThePossibleResults.Add(common);
            accidentalEmbarrassment.ThePossibleResults.Add(unlikely);

            return accidentalEmbarrassment;
        }

        //#TODO, add outcomes that can change character traits/personality
        public static TemplateForIncident Internal_Struggle()//crisis of faith, crisis of loyalty, guilty conscience
        {
            var internalStruggle = new TemplateForIncident("Internal Struggle");
            internalStruggle.TheFrequency = Frequency.Rarely;
            internalStruggle.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var individual = new Role("Individual") { MinCount = 1, MaxCount = 1 };

            internalStruggle.TheRoles.Add(individual);

            //Prereqs - none

            //Outcomes - #TODO, add outcomes that can change character traits/personality

            var common = new PossibleResult(80);

            var unlikely = new PossibleResult(20);

            internalStruggle.ThePossibleResults.Add(common);
            internalStruggle.ThePossibleResults.Add(unlikely);

            return internalStruggle;
        }

        //#TODO, add outcomes that can change character traits/personality
        public static TemplateForIncident Internal_Realization()//epiphany, self-discovery, change in world view, new philosophy
        {
            var internalRealization = new TemplateForIncident("Internal Realization");
            internalRealization.TheFrequency = Frequency.ExtremelyRarely;

            //Roles
            var individual = new Role("Individual") { MinCount = 1, MaxCount = 1 };

            internalRealization.TheRoles.Add(individual);

            //Prereqs - none

            //Outcomes - #TODO, add outcomes that can change character traits/personality

            var common = new PossibleResult(80);

            var unlikely = new PossibleResult(20);

            internalRealization.ThePossibleResults.Add(common);
            internalRealization.ThePossibleResults.Add(unlikely);

            return internalRealization;
        }

        public static TemplateForIncident ImpulsiveDecision()//emotional choice, rushed choice, poorly thought out choice
        {
            var impulsiveDecision = new TemplateForIncident("Impulsive Decision");
            impulsiveDecision.TheFrequency = Frequency.Periodically;

            //Roles
            var participants = new Role("Group") { MinCount = 1, MaxCount = null };

            impulsiveDecision.TheRoles.Add(participants);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            impulsiveDecision.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, participants, participants, "Large Bonding");

            var commonBonding = new PossibleResult(80);
            commonBonding.TheOutcomes.Add(bonding_Small);

            var unlikelyBonding = new PossibleResult(20);
            unlikelyBonding.TheOutcomes.Add(bonding_Large);

            impulsiveDecision.ThePossibleResults.Add(commonBonding);
            impulsiveDecision.ThePossibleResults.Add(unlikelyBonding);

            return impulsiveDecision;
        }

        public static TemplateForIncident Rescue_Social()
        {
            var rescueSocial = new TemplateForIncident("Rescue Socially");
            rescueSocial.TheFrequency = Frequency.Rarely;
            rescueSocial.IsPleasant = Pleasantness.EitherPleasantOrNot;
            rescueSocial.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Add roles
            var attackers = new Role("Attacker(s)") { MinCount = 0, MaxCount = null };
            var victims = new Role("Victim(s)") { MinCount = 1, MaxCount = null };
            var rescuers = new Role("Rescuer(s)") { MinCount = 1, MaxCount = null };

            rescueSocial.TheRoles.Add(attackers);
            rescueSocial.TheRoles.Add(victims);
            rescueSocial.TheRoles.Add(rescuers);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Exploit, attackers, victims);
            var prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, attackers);

            var prereq_RescuerEthicsMin = new DirectionalEthics_Min(EthicsScale.Coexist, rescuers, victims);
            var prereq_RescuerEthicsMax = new DirectionalEthics_Max(EthicsScale.Coexist, rescuers, attackers);
            var prereq_RescuerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, rescuers);

            rescueSocial.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            rescueSocial.ThePrerequisites.Add(prereq_AttackerMinTrust);
            rescueSocial.ThePrerequisites.Add(prereq_RescuerEthicsMin);
            rescueSocial.ThePrerequisites.Add(prereq_RescuerEthicsMax);
            rescueSocial.ThePrerequisites.Add(prereq_RescuerMinTrust);

            //Add outcomes
            ChangeInTrust largeTrustLoss = new ChangeInTrust(-2, victims, attackers, "Large Trust Loss");
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, victims, attackers, "Major Trust Loss");
            ChangeInTrust defendersBonding_Small = new ChangeInTrust(1, victims, victims, "Small Defender Bonding");
            ChangeInTrust defendersBonding_Large = new ChangeInTrust(2, victims, victims, "Large Defender Bonding");
            ChangeInTrust rescuerBonding_Small = new ChangeInTrust(1, victims, rescuers, "Small Bonding with Rescuer");
            ChangeInTrust rescuerBonding_Large = new ChangeInTrust(2, victims, rescuers, "Large Bonding with Rescuer");

            PossibleResult common = new PossibleResult(60);
            common.TheOutcomes.Add(largeTrustLoss);
            common.TheOutcomes.Add(defendersBonding_Small);
            common.TheOutcomes.Add(rescuerBonding_Small);

            PossibleResult unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(majorTrustLoss);
            unlikely.TheOutcomes.Add(defendersBonding_Large);
            common.TheOutcomes.Add(rescuerBonding_Large);

            rescueSocial.ThePossibleResults.Add(common);
            rescueSocial.ThePossibleResults.Add(unlikely);

            return rescueSocial;
        }

        /*
        Future - NewCharacter? (if not, need a way to delay using a character that already was created until later)
        Future - RemoveCharacter (death, permanent relocation, etc)
        Future - Compete for favor - competitors, person they are trying to impress

            3+ roles            
        Conversation_About3rdParty - Targeted Deception/Revelation (lying, unmasking, gossip) - PartyWhoIsTelling, PartyWhoListens, PartyBeingLiedAbout_OrRevealed

         */
        #endregion

        #region Generic

        public static TemplateForIncident RoutineTask()//eat, sleep, work, grooming
        {
            var routineTask = new TemplateForIncident("Routine Task");
            routineTask.TheFrequency = Frequency.Often;

            //Roles
            var participants = new Role("Participants") { MinCount = 1, MaxCount = null };

            routineTask.TheRoles.Add(participants);

            //Prereqs
            MutualTrust_Min prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            routineTask.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");


            var common = new PossibleResult(70);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(distrust_Small);

            routineTask.ThePossibleResults.Add(common);
            routineTask.ThePossibleResults.Add(unlikely);

            return routineTask;
        }

        public static TemplateForIncident Travel()
        {
            var travel = new TemplateForIncident("Travel");
            travel.TheFrequency = Frequency.Often;

            //Roles
            var travelers = new Role("Travelers") { MinCount = 1, MaxCount = null };

            travel.TheRoles.Add(travelers);

            //Prereqs
            MutualTrust_Min prereq_TravelerMinTrust = new MutualTrust_Min(EthicsScale.Coexist, travelers);

            travel.ThePrerequisites.Add(prereq_TravelerMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, travelers, travelers, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, travelers, travelers, "Small Distrust");


            var common = new PossibleResult(70);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(distrust_Small);

            travel.ThePossibleResults.Add(common);
            travel.ThePossibleResults.Add(unlikely);

            return travel;
        }

        public static TemplateForIncident Training()
        {
            var training = new TemplateForIncident("Training");
            training.TheFrequency = Frequency.Often;

            //Roles
            var trainer = new Role("Trainer") { MinCount = 0, MaxCount = 1 };
            var students = new Role("Student(s)") { MinCount = 0, MaxCount = null };

            training.TheRoles.Add(trainer);
            training.TheRoles.Add(students);

            //Prereqs
            var prereq_TrainerEthicsMin = new DirectionalEthics_Min(EthicsScale.Cooperate, trainer, students);
            var prereq_StudentTrustMin = new DirectionalTrust_Min(EthicsScale.Cooperate, students, trainer);

            training.ThePrerequisites.Add(prereq_TrainerEthicsMin);
            training.ThePrerequisites.Add(prereq_StudentTrustMin);

            //Outcomes
            var bonding_Small_Peers = new ChangeInTrust(1, students, students, "Small Bonding - Peers");
            var bonding_Small_Trainer = new ChangeInTrust(1, students, trainer, "Small Bonding - Trainer");
            var distrust_Small_Trainer = new ChangeInTrust(-1, students, trainer, "Small Distrust - Trainer");

            var common = new PossibleResult(50);
            common.TheOutcomes.Add(bonding_Small_Peers);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(bonding_Small_Trainer);
            unlikely.TheOutcomes.Add(bonding_Small_Peers);

            var rare = new PossibleResult(20);
            rare.TheOutcomes.Add(distrust_Small_Trainer);

            training.ThePossibleResults.Add(common);
            training.ThePossibleResults.Add(unlikely);

            return training;
        }

        public static TemplateForIncident SelfImprovement() //Practice, study, pondering, research
        {
            var selfImprovement = new TemplateForIncident("Self Improvement");
            selfImprovement.TheFrequency = Frequency.Often;

            //Roles
            var participants = new Role("Participants") { MinCount = 1, MaxCount = null };

            selfImprovement.TheRoles.Add(participants);

            //Prereqs
            var prereq_ParticipantMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            selfImprovement.ThePrerequisites.Add(prereq_ParticipantMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");

            var common = new PossibleResult(70);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(distrust_Small);

            selfImprovement.ThePossibleResults.Add(common);
            selfImprovement.ThePossibleResults.Add(unlikely);

            return selfImprovement;
        }

        public static TemplateForIncident SocialGathering()//ceremony, celebration, funeral, entertainment, games, performance
        {
            var socialGathering = new TemplateForIncident("Social Gathering");
            socialGathering.TheFrequency = Frequency.Often;

            //Roles
            var participants = new Role("Attendee(s)") { MinCount = 1, MaxCount = null };

            socialGathering.TheRoles.Add(participants);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Coexist, participants);

            socialGathering.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");


            var common = new PossibleResult(70);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(distrust_Small);

            socialGathering.ThePossibleResults.Add(common);
            socialGathering.ThePossibleResults.Add(unlikely);

            return socialGathering;
        }

        public static TemplateForIncident Message_Received() //new rumor, word of remote event
        {
            var receiveMessage = new TemplateForIncident("Receive Message");
            receiveMessage.TheFrequency = Frequency.Periodically;

            //Roles
            var participants = new Role("Party Receiving") { MinCount = 1, MaxCount = null };

            receiveMessage.TheRoles.Add(participants);

            //Prereqs
            MutualTrust_Min prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Coexist, participants);

            receiveMessage.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");


            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(distrust_Small);

            receiveMessage.ThePossibleResults.Add(common);
            receiveMessage.ThePossibleResults.Add(unlikely);

            return receiveMessage;
        }

        public static TemplateForIncident Message_Sent() //request help, leave instructions
        {
            var sendMessage = new TemplateForIncident("Send Message");
            sendMessage.TheFrequency = Frequency.Periodically;

            //Roles
            var participants = new Role("Sender(s)") { MinCount = 1, MaxCount = null };

            sendMessage.TheRoles.Add(participants);

            //Prereqs
            MutualTrust_Min prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Coexist, participants);

            sendMessage.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, participants, participants, "Large Bonding");


            var common = new PossibleResult(80);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(bonding_Large);

            sendMessage.ThePossibleResults.Add(common);
            sendMessage.ThePossibleResults.Add(unlikely);

            return sendMessage;
        }

        public static TemplateForIncident Message_Lost()
        {
            var lostMessage = new TemplateForIncident("Message Lost & Never Delivered");
            lostMessage.TheFrequency = Frequency.ExtremelyRarely;
            lostMessage.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var senders = new Role("Sender(s)") { MinCount = 0, MaxCount = null };
            var getters = new Role("Receiver(s)") { MinCount = 0, MaxCount = null };

            lostMessage.TheRoles.Add(senders);
            lostMessage.TheRoles.Add(getters);

            //Prereqs
            var prereq_MutualMinTrust_Senders = new MutualTrust_Min(EthicsScale.Cooperate, senders);
            var prereq_MinEthicsTowardsOthers = new DirectionalEthics_Min(EthicsScale.Cooperate, senders, getters);
            var prereq_MutualMinTrust_Getters = new MutualTrust_Min(EthicsScale.Coexist, getters);

            lostMessage.ThePrerequisites.Add(prereq_MutualMinTrust_Senders);
            lostMessage.ThePrerequisites.Add(prereq_MinEthicsTowardsOthers);
            lostMessage.ThePrerequisites.Add(prereq_MutualMinTrust_Getters);

            //Outcomes
            var distrust_Small = new ChangeInTrust(-1, getters, senders, "Small Distrust");
            var distrust_Large = new ChangeInTrust(-2, getters, senders, "Large Distrust");


            var common = new PossibleResult(30);
            common.TheOutcomes.Add(distrust_Small);

            var unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(distrust_Large);

            lostMessage.ThePossibleResults.Add(common);
            lostMessage.ThePossibleResults.Add(unlikely);

            return lostMessage;
        }

        public static TemplateForIncident AcquireTool() //build or craft, find abandoned, purchase
        {
            var acquireTool = new TemplateForIncident("Acquire Tool or Equipment");
            acquireTool.TheFrequency = Frequency.Periodically;

            //Roles
            var participants = new Role("Involved") { MinCount = 1, MaxCount = null };

            acquireTool.TheRoles.Add(participants);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            acquireTool.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");


            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(distrust_Small);

            acquireTool.ThePossibleResults.Add(common);
            acquireTool.ThePossibleResults.Add(unlikely);

            return acquireTool;
        }

        public static TemplateForIncident EquipmentFailure()
        {
            var equipmentFailure = new TemplateForIncident("Equipment Failure");
            equipmentFailure.TheFrequency = Frequency.Rarely;
            equipmentFailure.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var participants = new Role("Involved") { MinCount = 1, MaxCount = null };

            equipmentFailure.TheRoles.Add(participants);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            equipmentFailure.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");


            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(distrust_Small);

            equipmentFailure.ThePossibleResults.Add(common);
            equipmentFailure.ThePossibleResults.Add(unlikely);

            return equipmentFailure;
        }

        public static TemplateForIncident Injury_Accidental()
        {
            var accidentalInjury = new TemplateForIncident("Accidental Injury");
            accidentalInjury.TheFrequency = Frequency.Rarely;
            accidentalInjury.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var participants = new Role("Involved") { MinCount = 1, MaxCount = 2 };

            accidentalInjury.TheRoles.Add(participants);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            accidentalInjury.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");


            var common = new PossibleResult(70);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(distrust_Small);

            accidentalInjury.ThePossibleResults.Add(common);
            accidentalInjury.ThePossibleResults.Add(unlikely);

            return accidentalInjury;
        }

        public static TemplateForIncident RestAndRecover()
        {
            var restAndRecover = new TemplateForIncident("Rest And Recover");
            restAndRecover.TheFrequency = Frequency.Periodically;

            //Roles
            var participants = new Role("Involved") { MinCount = 1, MaxCount = null };

            restAndRecover.TheRoles.Add(participants);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            restAndRecover.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");


            var common = new PossibleResult(70);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(distrust_Small);

            restAndRecover.ThePossibleResults.Add(common);
            restAndRecover.ThePossibleResults.Add(unlikely);

            return restAndRecover;
        }

        public static TemplateForIncident IndustrialDisaster()
        {
            var industrialDisaster = new TemplateForIncident("Industrial Disaster");
            industrialDisaster.TheFrequency = Frequency.ExtremelyRarely;
            industrialDisaster.IsPleasant = Pleasantness.NeverPleasant;
            industrialDisaster.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Roles
            var participants = new Role("Involved") { MinCount = 1, MaxCount = 5 };

            industrialDisaster.TheRoles.Add(participants);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            industrialDisaster.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, participants, participants, "Large Bonding");
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");

            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Large);

            var unlikely = new PossibleResult(25);
            unlikely.TheOutcomes.Add(bonding_Small);

            var rare = new PossibleResult(15);
            rare.TheOutcomes.Add(distrust_Small);

            industrialDisaster.ThePossibleResults.Add(common);
            industrialDisaster.ThePossibleResults.Add(unlikely);
            industrialDisaster.ThePossibleResults.Add(rare);

            return industrialDisaster;
        }

        public static TemplateForIncident Luck_Good() //find valuable item, near-miss with danger, inherit fortune
        {
            var goodLuck = new TemplateForIncident("Chance Happening - Good Luck");
            goodLuck.TheFrequency = Frequency.Rarely;
            goodLuck.IsPleasant = Pleasantness.AlwaysPleasant;

            //Roles
            var participants = new Role("Involved") { MinCount = 1, MaxCount = null };

            goodLuck.TheRoles.Add(participants);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            goodLuck.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, participants, participants, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, participants, participants, "Large Bonding");

            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(bonding_Large);

            goodLuck.ThePossibleResults.Add(common);
            goodLuck.ThePossibleResults.Add(unlikely);

            return goodLuck;
        }

        public static TemplateForIncident Luck_Bad() //lose valued item, obstacle when already late
        {
            var badLuck = new TemplateForIncident("Chance Happening - Bad Luck");
            badLuck.TheFrequency = Frequency.Periodically;
            badLuck.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var participants = new Role("Involved") { MinCount = 1, MaxCount = null };

            badLuck.TheRoles.Add(participants);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, participants);

            badLuck.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var distrust_Small = new ChangeInTrust(-1, participants, participants, "Small Distrust");
            var distrust_Large = new ChangeInTrust(-2, participants, participants, "Large Distrust");

            var common = new PossibleResult(70);
            common.TheOutcomes.Add(distrust_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(distrust_Large);

            badLuck.ThePossibleResults.Add(common);
            badLuck.ThePossibleResults.Add(unlikely);

            return badLuck;
        }

        public static TemplateForIncident OrganizedCompetition() //#TODO - add trigger for multi-stage competitions
        {
            var organizedCompetition = new TemplateForIncident("Organized Competition");
            organizedCompetition.TheFrequency = Frequency.Rarely;
            organizedCompetition.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Roles
            var competitors = new Role("Competitor(s)") { MinCount = 1, MaxCount = null };

            organizedCompetition.TheRoles.Add(competitors);

            //Prereqs
            var prereq_MutualMinTrust = new MutualTrust_Min(EthicsScale.Exploit, competitors);

            organizedCompetition.ThePrerequisites.Add(prereq_MutualMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, competitors, competitors, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, competitors, competitors, "Small Distrust");

            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(distrust_Small);

            organizedCompetition.ThePossibleResults.Add(common);
            organizedCompetition.ThePossibleResults.Add(unlikely);

            return organizedCompetition;
        }


        #endregion

        #region Action
        
        public static TemplateForIncident Aggression_Violent()//ambush, fist fight, battle, duel/challenge, outburst
        {
            var violentAggression = new TemplateForIncident("Violent Aggression");
            violentAggression.TheFrequency = Frequency.Periodically;
            violentAggression.IsPleasant = Pleasantness.NeverPleasant;
            violentAggression.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Add roles
            var partyAttacking = new Role("Attacker(s)") { MinCount = 0, MaxCount = null };
            var partyDefending = new Role("Defender(s)") { MinCount = 0, MaxCount = null };

            violentAggression.TheRoles.Add(partyAttacking);
            violentAggression.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Beat, partyAttacking, partyDefending);
            var prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);
            var prereq_DefenderMinTrust = new MutualTrust_Min(EthicsScale.Exploit, partyDefending);

            violentAggression.ThePrerequisites.Add(prereqEthicsMax);
            violentAggression.ThePrerequisites.Add(prereq_AttackerMinTrust);
            violentAggression.ThePrerequisites.Add(prereq_DefenderMinTrust);

            //Add outcomes
            ChangeInTrust largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");
            ChangeInTrust reverseTrustLoss = new ChangeInTrust(-1, partyAttacking, partyDefending, "Reciprocal Trust Loss");
            ChangeInTrust defendersBonding_Small = new ChangeInTrust(1, partyDefending, partyDefending, "Small Defender Bonding");
            ChangeInTrust defendersBonding_Large = new ChangeInTrust(2, partyDefending, partyDefending, "Large Defender Bonding");
            ChangeInTrust defendersBonding_Major = new ChangeInTrust(3, partyDefending, partyDefending, "Major Defender Bonding");

            PossibleResult common = new PossibleResult(40);
            common.TheOutcomes.Add(largeTrustLoss);
            common.TheOutcomes.Add(defendersBonding_Small);

            PossibleResult unlikely = new PossibleResult(35);
            unlikely.TheOutcomes.Add(largeTrustLoss);
            unlikely.TheOutcomes.Add(defendersBonding_Large);

            PossibleResult rare = new PossibleResult(25);
            rare.TheOutcomes.Add(majorTrustLoss);
            rare.TheOutcomes.Add(reverseTrustLoss);
            rare.TheOutcomes.Add(defendersBonding_Major);

            violentAggression.ThePossibleResults.Add(common);
            violentAggression.ThePossibleResults.Add(unlikely);
            violentAggression.ThePossibleResults.Add(rare);

            return violentAggression;
        }

        public static TemplateForIncident Aggression_Murderous()//enslave, murder, torture, abuse
        {
            var murderousAggression = new TemplateForIncident("Murderous Aggression");
            murderousAggression.TheFrequency = Frequency.Rarely;
            murderousAggression.IsPleasant = Pleasantness.NeverPleasant;
            murderousAggression.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Add roles
            var partyAttacking = new Role("Attacker(s)") { MinCount = 0, MaxCount = null };
            var partyDefending = new Role("Defender(s)") { MinCount = 1, MaxCount = null };

            murderousAggression.TheRoles.Add(partyAttacking);
            murderousAggression.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Murder, partyAttacking, partyDefending);
            var prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);

            murderousAggression.ThePrerequisites.Add(prereqEthicsMax);
            murderousAggression.ThePrerequisites.Add(prereq_AttackerMinTrust);

            //Add outcomes
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");
            ChangeInTrust massiveTrustLoss = new ChangeInTrust(-4, partyDefending, partyAttacking, "Massive Trust Loss");
            ChangeInTrust defendersBonding_Small = new ChangeInTrust(1, partyDefending, partyDefending, "Small Defender Bonding");
            ChangeInTrust defendersBonding_Large = new ChangeInTrust(2, partyDefending, partyDefending, "Large Defender Bonding");
            ChangeInTrust defendersBonding_Major = new ChangeInTrust(3, partyDefending, partyDefending, "Major Defender Bonding");

            PossibleResult common = new PossibleResult(40);
            common.TheOutcomes.Add(majorTrustLoss);
            common.TheOutcomes.Add(defendersBonding_Small);

            PossibleResult unlikely = new PossibleResult(35);
            unlikely.TheOutcomes.Add(majorTrustLoss);
            unlikely.TheOutcomes.Add(defendersBonding_Large);

            PossibleResult rare = new PossibleResult(25);
            rare.TheOutcomes.Add(massiveTrustLoss);
            rare.TheOutcomes.Add(defendersBonding_Major);

            murderousAggression.ThePossibleResults.Add(common);
            murderousAggression.ThePossibleResults.Add(unlikely);
            murderousAggression.ThePossibleResults.Add(rare);

            return murderousAggression;
        }

        public static TemplateForIncident Persuit_NonViolent()
        {
            var persuit = new TemplateForIncident("Persuit");
            persuit.TheFrequency = Frequency.Often;
            persuit.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Add roles
            var chasing = new Role("Persuer(s)") { MinCount = 0, MaxCount = null };
            var fleeing = new Role("Fleeing") { MinCount = 0, MaxCount = null };

            persuit.TheRoles.Add(chasing);
            persuit.TheRoles.Add(fleeing);

            //Add prereqs
            var prereqEthicsMin = new DirectionalEthics_Min(EthicsScale.Exploit, chasing, fleeing);
            var prereq_Chaser_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, chasing);
            var prereq_Fleeing_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, fleeing);

            persuit.ThePrerequisites.Add(prereq_Chaser_MutualMinTrust);
            persuit.ThePrerequisites.Add(prereq_Fleeing_MutualMinTrust);
            persuit.ThePrerequisites.Add(prereqEthicsMin);

            //Add outcomes
            ChangeInTrust fleeing_Bonding_Small = new ChangeInTrust(1, fleeing, fleeing, "Small Bonding - Those Fleeing");
            ChangeInTrust fleeing_Bonding_Large = new ChangeInTrust(1, fleeing, fleeing, "Large Bonding - Those Fleeing");
            ChangeInTrust chasing_Bonding_Small = new ChangeInTrust(1, chasing, chasing, "Small Bonding - Persuers");
            ChangeInTrust chasing_Bonding_Large = new ChangeInTrust(1, chasing, chasing, "Large Bonding - Persuers");

            PossibleResult common = new PossibleResult(60);
            common.TheOutcomes.Add(chasing_Bonding_Small);
            common.TheOutcomes.Add(fleeing_Bonding_Small);

            PossibleResult unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(chasing_Bonding_Large);
            unlikely.TheOutcomes.Add(fleeing_Bonding_Large);

            persuit.ThePossibleResults.Add(common);
            persuit.ThePossibleResults.Add(unlikely);

            return persuit;
        }

        public static TemplateForIncident Persuit_Violent()//escape, retreat, seek refuge 
        {
            var violentPersuit = new TemplateForIncident("Violent Persuit");
            violentPersuit.TheFrequency = Frequency.Periodically;
            violentPersuit.IsPleasant = Pleasantness.NeverPleasant;
            violentPersuit.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Add roles
            var chasing = new Role("Persuer(s)") { MinCount = 0, MaxCount = null };
            var fleeing = new Role("Fleeing") { MinCount = 0, MaxCount = null };

            violentPersuit.TheRoles.Add(chasing);
            violentPersuit.TheRoles.Add(fleeing);

            //Add prereqs
            var prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Beat, chasing, fleeing);
            var prereq_Chaser_MutualMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, chasing);
            var prereq_Fleeing_MutualMinTrust = new MutualTrust_Min(EthicsScale.Exploit, fleeing);

            violentPersuit.ThePrerequisites.Add(prereqEthicsMax);
            violentPersuit.ThePrerequisites.Add(prereq_Chaser_MutualMinTrust);
            violentPersuit.ThePrerequisites.Add(prereq_Fleeing_MutualMinTrust);

            //Add outcomes
            ChangeInTrust smallTrustLoss = new ChangeInTrust(-1, fleeing, chasing, "Small Trust Loss");
            ChangeInTrust largeTrustLoss = new ChangeInTrust(-2, fleeing, chasing, "Large Trust Loss");
            ChangeInTrust fleeing_Bonding_Small = new ChangeInTrust(1, fleeing, fleeing, "Small Bonding - Those Fleeing");
            ChangeInTrust fleeing_Bonding_Large = new ChangeInTrust(1, fleeing, fleeing, "Large Bonding - Those Fleeing");
            ChangeInTrust chasing_Bonding_Small = new ChangeInTrust(1, chasing, chasing, "Small Bonding - Persuers");
            ChangeInTrust chasing_Bonding_Large = new ChangeInTrust(1, chasing, chasing, "Large Bonding - Persuers");

            PossibleResult common = new PossibleResult(60);
            common.TheOutcomes.Add(smallTrustLoss);
            common.TheOutcomes.Add(fleeing_Bonding_Small);
            common.TheOutcomes.Add(chasing_Bonding_Small);

            PossibleResult unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(largeTrustLoss);
            unlikely.TheOutcomes.Add(fleeing_Bonding_Large);
            unlikely.TheOutcomes.Add(chasing_Bonding_Large);

            violentPersuit.ThePossibleResults.Add(common);
            violentPersuit.ThePossibleResults.Add(unlikely);

            return violentPersuit;
        }

        public static TemplateForIncident Hide()
        {
            var hide = new TemplateForIncident("Hide");
            hide.TheFrequency = Frequency.Periodically;

            //Roles
            var hiders = new Role("Hiding") { MinCount = 1, MaxCount = null };

            hide.TheRoles.Add(hiders);

            //Prereqs
            MutualTrust_Min prereq_TravelerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, hiders);

            hide.ThePrerequisites.Add(prereq_TravelerMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, hiders, hiders, "Small Bonding");
            var distrust_Small = new ChangeInTrust(-1, hiders, hiders, "Small Distrust");

            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(distrust_Small);

            hide.ThePossibleResults.Add(common);
            hide.ThePossibleResults.Add(unlikely);

            return hide;
        }

        //#TODO - add prerequisiste so that "good" characters do not end up robbing minor/unnamed characters
        public static TemplateForIncident CriminalAction_NonViolent()//theft, fraud, smuggling
        {
            var crime_Nonviolent = new TemplateForIncident("Criminal Action");
            crime_Nonviolent.TheFrequency = Frequency.Periodically;
            crime_Nonviolent.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var criminals = new Role("Criminal(s)") { MinCount = 0, MaxCount = null };
            var targets = new Role("Target(s)") { MinCount = 0, MaxCount = null };

            crime_Nonviolent.TheRoles.Add(criminals);
            crime_Nonviolent.TheRoles.Add(targets);

            //Add prereqs
            var prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Exploit, criminals, targets);
            var prereq_MutualMinTrust_Criminals = new MutualTrust_Min(EthicsScale.Cooperate, criminals);
            var prereq_MutualMinTrust_Targets = new MutualTrust_Min(EthicsScale.Coexist, targets);

            crime_Nonviolent.ThePrerequisites.Add(prereqEthicsMax);
            crime_Nonviolent.ThePrerequisites.Add(prereq_MutualMinTrust_Criminals);
            crime_Nonviolent.ThePrerequisites.Add(prereq_MutualMinTrust_Targets);

            //Add outcomes
            ChangeInTrust largeTrustLoss = new ChangeInTrust(-2, targets, criminals, "Large Trust Loss");
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, targets, criminals, "Major Trust Loss");
            ChangeInTrust defendersBonding_Small = new ChangeInTrust(1, targets, targets, "Small Defender Bonding");
            ChangeInTrust defendersBonding_Large = new ChangeInTrust(2, targets, targets, "Large Defender Bonding");

            PossibleResult common = new PossibleResult(70);
            common.TheOutcomes.Add(largeTrustLoss);
            common.TheOutcomes.Add(defendersBonding_Small);

            PossibleResult unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(majorTrustLoss);
            unlikely.TheOutcomes.Add(defendersBonding_Large);
            
            crime_Nonviolent.ThePossibleResults.Add(common);
            crime_Nonviolent.ThePossibleResults.Add(unlikely);

            return crime_Nonviolent;
        }
        
        //#TODO - make magnitude based on how strong trust was
        public static TemplateForIncident Betrayal_Violent()
        {
            var violentBetrayal = new TemplateForIncident("Violent Betrayal");
            violentBetrayal.TheFrequency = Frequency.ExtremelyRarely;
            violentBetrayal.IsPleasant = Pleasantness.NeverPleasant;
            violentBetrayal.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Add roles
            var partyAttacking = new Role("Party Betraying") { MinCount = 0, MaxCount = null };
            var partyDefending = new Role("Party Being Betrayed") { MinCount = 1, MaxCount = null };

            violentBetrayal.TheRoles.Add(partyAttacking);
            violentBetrayal.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Beat, partyAttacking, partyDefending);
            var prereq_DefenderTrustMin = new DirectionalTrust_Min(EthicsScale.Cooperate, partyDefending, partyAttacking);
            var prereq_AttackerMutualTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);
            var prereq_DefenderMutualTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyDefending);

            violentBetrayal.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            violentBetrayal.ThePrerequisites.Add(prereq_DefenderTrustMin);
            violentBetrayal.ThePrerequisites.Add(prereq_AttackerMutualTrust);
            violentBetrayal.ThePrerequisites.Add(prereq_DefenderMutualTrust);

            //Add outcomes
            var majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");
            var massiveTrustLoss = new ChangeInTrust(-4, partyDefending, partyAttacking, "Massive Trust Loss");

            var common = new PossibleResult(60);
            common.TheOutcomes.Add(majorTrustLoss);

            var unlikely = new PossibleResult(40);
            unlikely.TheOutcomes.Add(massiveTrustLoss);

            violentBetrayal.ThePossibleResults.Add(common);
            violentBetrayal.ThePossibleResults.Add(unlikely);

            return violentBetrayal;
        }

        public static TemplateForIncident Trapped()//captured, surrounded
        {
            var trapped = new TemplateForIncident("Trapped");
            trapped.TheFrequency = Frequency.Rarely;
            trapped.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var partyAttacking = new Role("Attacker(s)") { MinCount = 0, MaxCount = null };
            var partyDefending = new Role("Defender(s)") { MinCount = 1, MaxCount = null };

            trapped.TheRoles.Add(partyAttacking);
            trapped.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Beat, partyAttacking, partyDefending);
            var prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);
            var prereq_DefenderMinTrust = new MutualTrust_Min(EthicsScale.Coexist, partyDefending);

            trapped.ThePrerequisites.Add(prereqEthicsMax);
            trapped.ThePrerequisites.Add(prereq_AttackerMinTrust);
            trapped.ThePrerequisites.Add(prereq_DefenderMinTrust);

            //Add outcomes
            ChangeInTrust largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");
            ChangeInTrust defendersBonding_Small = new ChangeInTrust(1, partyDefending, partyDefending, "Small Defender Bonding");
            ChangeInTrust defendersBonding_Large = new ChangeInTrust(2, partyDefending, partyDefending, "Large Defender Bonding");

            PossibleResult common = new PossibleResult(40);
            common.TheOutcomes.Add(largeTrustLoss);
            common.TheOutcomes.Add(defendersBonding_Small);

            PossibleResult unlikely = new PossibleResult(35);
            unlikely.TheOutcomes.Add(majorTrustLoss);
            unlikely.TheOutcomes.Add(defendersBonding_Large);
            
            trapped.ThePossibleResults.Add(common);
            trapped.ThePossibleResults.Add(unlikely);

            return trapped;
        }

        //#TODO - make trigger only?
        public static TemplateForIncident Surrender()
        {
            var surrender = new TemplateForIncident("Surrender");
            surrender.TheFrequency = Frequency.ExtremelyRarely;
            surrender.IsPleasant = Pleasantness.NeverPleasant;

            //Add roles
            var partyAttacking = new Role("Attacker(s)") { MinCount = 0, MaxCount = null };
            var partyDefending = new Role("Defender(s)") { MinCount = 1, MaxCount = null };

            surrender.TheRoles.Add(partyAttacking);
            surrender.TheRoles.Add(partyDefending);

            //Add prereqs
            var prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Beat, partyAttacking, partyDefending);
            var prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyAttacking);
            var prereq_DefenderMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, partyDefending);

            surrender.ThePrerequisites.Add(prereqEthicsMax);
            surrender.ThePrerequisites.Add(prereq_AttackerMinTrust);
            surrender.ThePrerequisites.Add(prereq_DefenderMinTrust);

            //Add outcomes
            ChangeInTrust largeTrustLoss = new ChangeInTrust(-2, partyDefending, partyAttacking, "Large Trust Loss");
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, partyDefending, partyAttacking, "Major Trust Loss");
            ChangeInTrust defendersBonding_Small = new ChangeInTrust(1, partyDefending, partyDefending, "Small Defender Bonding");
            ChangeInTrust defendersBonding_Large = new ChangeInTrust(2, partyDefending, partyDefending, "Large Defender Bonding");

            PossibleResult common = new PossibleResult(40);
            common.TheOutcomes.Add(largeTrustLoss);
            common.TheOutcomes.Add(defendersBonding_Small);

            PossibleResult unlikely = new PossibleResult(35);
            unlikely.TheOutcomes.Add(majorTrustLoss);
            unlikely.TheOutcomes.Add(defendersBonding_Large);

            surrender.ThePossibleResults.Add(common);
            surrender.ThePossibleResults.Add(unlikely);

            return surrender;
        }

        public static TemplateForIncident Sabotage()
        {
            var sabotage = new TemplateForIncident("Sabotage");
            sabotage.TheFrequency = Frequency.Periodically;
            sabotage.IsPleasant = Pleasantness.NeverPleasant;
            sabotage.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Add roles
            var attackers = new Role("Attacker(s)") { MinCount = 0, MaxCount = null };
            var targets = new Role("Target(s)") { MinCount = 0, MaxCount = null };

            sabotage.TheRoles.Add(attackers);
            sabotage.TheRoles.Add(targets);

            //Add prereqs
            var prereqEthicsMax = new DirectionalEthics_Max(EthicsScale.Exploit, attackers, targets);
            var prereq_MutualMinTrust_Attackers = new MutualTrust_Min(EthicsScale.Cooperate, attackers);
            var prereq_MutualMinTrust_Targets = new MutualTrust_Min(EthicsScale.Coexist, targets);

            sabotage.ThePrerequisites.Add(prereqEthicsMax);
            sabotage.ThePrerequisites.Add(prereq_MutualMinTrust_Attackers);
            sabotage.ThePrerequisites.Add(prereq_MutualMinTrust_Targets);

            //Add outcomes
            ChangeInTrust largeTrustLoss = new ChangeInTrust(-2, targets, attackers, "Large Trust Loss");
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, targets, attackers, "Major Trust Loss");
            ChangeInTrust defendersBonding_Small = new ChangeInTrust(1, targets, targets, "Small Defender Bonding");
            ChangeInTrust defendersBonding_Large = new ChangeInTrust(2, targets, targets, "Large Defender Bonding");

            PossibleResult common = new PossibleResult(70);
            common.TheOutcomes.Add(largeTrustLoss);
            common.TheOutcomes.Add(defendersBonding_Small);

            PossibleResult unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(majorTrustLoss);
            unlikely.TheOutcomes.Add(defendersBonding_Large);

            sabotage.ThePossibleResults.Add(common);
            sabotage.ThePossibleResults.Add(unlikely);

            return sabotage;
        }
        
        public static TemplateForIncident Rescue_Violent()
        {
            var rescueViolent = new TemplateForIncident("Rescue from Violence");
            rescueViolent.TheFrequency = Frequency.Rarely;
            rescueViolent.IsPleasant = Pleasantness.EitherPleasantOrNot;
            rescueViolent.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Add roles
            var attackers = new Role("Attacker(s)") { MinCount = 0, MaxCount = null };
            var victims = new Role("Victim(s)") { MinCount = 1, MaxCount = null };
            var rescuers = new Role("Rescuer(s)") { MinCount = 1, MaxCount = null };

            rescueViolent.TheRoles.Add(attackers);
            rescueViolent.TheRoles.Add(victims);
            rescueViolent.TheRoles.Add(rescuers);

            //Add prereqs
            var prereq_AttackerEthicsMax = new DirectionalEthics_Max(EthicsScale.Beat, attackers, victims);
            var prereq_AttackerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, attackers);

            var prereq_RescuerEthicsMin = new DirectionalEthics_Min(EthicsScale.Cooperate, rescuers, victims);
            var prereq_RescuerEthicsMax = new DirectionalEthics_Max(EthicsScale.Coexist, rescuers, attackers);
            var prereq_RescuerMinTrust = new MutualTrust_Min(EthicsScale.Cooperate, rescuers);

            rescueViolent.ThePrerequisites.Add(prereq_AttackerEthicsMax);
            rescueViolent.ThePrerequisites.Add(prereq_AttackerMinTrust);
            rescueViolent.ThePrerequisites.Add(prereq_RescuerEthicsMin);
            rescueViolent.ThePrerequisites.Add(prereq_RescuerEthicsMax);
            rescueViolent.ThePrerequisites.Add(prereq_RescuerMinTrust);

            //Add outcomes
            ChangeInTrust majorTrustLoss = new ChangeInTrust(-3, victims, attackers, "Major Trust Loss");
            ChangeInTrust massiveTrustLoss = new ChangeInTrust(-4, victims, attackers, "Massive Trust Loss");
            ChangeInTrust defendersBonding_Small = new ChangeInTrust(1, victims, victims, "Small Defender Bonding");
            ChangeInTrust defendersBonding_Large = new ChangeInTrust(2, victims, victims, "Large Defender Bonding");
            ChangeInTrust defendersBonding_Major = new ChangeInTrust(3, victims, victims, "Major Defender Bonding");
            ChangeInTrust rescuerBonding_Large = new ChangeInTrust(2, victims, rescuers, "Large Bonding with Rescuer");
            ChangeInTrust rescuerBonding_Major = new ChangeInTrust(3, victims, rescuers, "Major Bonding with Rescuer");

            PossibleResult common = new PossibleResult(40);
            common.TheOutcomes.Add(majorTrustLoss);
            common.TheOutcomes.Add(defendersBonding_Small);
            common.TheOutcomes.Add(rescuerBonding_Large);

            PossibleResult unlikely = new PossibleResult(35);
            unlikely.TheOutcomes.Add(majorTrustLoss);
            unlikely.TheOutcomes.Add(defendersBonding_Large);
            common.TheOutcomes.Add(rescuerBonding_Large);

            PossibleResult rare = new PossibleResult(25);
            rare.TheOutcomes.Add(massiveTrustLoss);
            rare.TheOutcomes.Add(defendersBonding_Major);
            common.TheOutcomes.Add(rescuerBonding_Major);

            rescueViolent.ThePossibleResults.Add(common);
            rescueViolent.ThePossibleResults.Add(unlikely);
            rescueViolent.ThePossibleResults.Add(rare);

            return rescueViolent;
        }


        /*
        Add more common action incidents? Right now only Persuit_Nonviolent
        
        Injury / poisoning **trigger only?**      
        Public unrest - riot / rebellion / revolution
        
        */

        #endregion

        #region Mystery
        /*
        
        Find riddle to solve / mystery (rare)
        Search for hidden person/object/information (often)
        Find clue / useful information (periodically)
        Uncover explanation for mystery (extremely rare)

         */
        #endregion

        #region ConflictWithNature

        public static TemplateForIncident Lost()
        {
            var lost = new TemplateForIncident("Lost");
            lost.TheFrequency = Frequency.Periodically;
            lost.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var travelers = new Role("Travelers") { MinCount = 1, MaxCount = null };

            lost.TheRoles.Add(travelers);

            //Prereqs
            MutualTrust_Min prereq_TravelerMinTrust = new MutualTrust_Min(EthicsScale.Exploit, travelers);

            lost.ThePrerequisites.Add(prereq_TravelerMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, travelers, travelers, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, travelers, travelers, "Large Bonding");
            var distrust_Small = new ChangeInTrust(-1, travelers, travelers, "Small Distrust");

            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(25);
            unlikely.TheOutcomes.Add(distrust_Small);

            var rare = new PossibleResult(15);
            rare.TheOutcomes.Add(bonding_Large);


            lost.ThePossibleResults.Add(common);
            lost.ThePossibleResults.Add(unlikely);
            lost.ThePossibleResults.Add(rare);

            return lost;
        }

        public static TemplateForIncident Disease()
        {
            var disease = new TemplateForIncident("Disease");
            disease.TheFrequency = Frequency.Rarely;
            disease.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var patients = new Role("Patients") { MinCount = 1, MaxCount = 4 };

            disease.TheRoles.Add(patients);

            //No Prereqs            

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, patients, patients, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, patients, patients, "Large Bonding");
            var distrust_Small = new ChangeInTrust(-1, patients, patients, "Small Distrust");


            var common = new PossibleResult(50);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(bonding_Large);

            var rare = new PossibleResult(20);
            rare.TheOutcomes.Add(distrust_Small);

            disease.ThePossibleResults.Add(common);
            disease.ThePossibleResults.Add(unlikely);
            disease.ThePossibleResults.Add(rare);

            return disease;
        }

        public static TemplateForIncident NaturalDisaster()//wildfire, flood, earthquake, hurricane, tornado
        {
            var naturalDisaster = new TemplateForIncident("Natural Disaster");
            naturalDisaster.TheFrequency = Frequency.ExtremelyRarely;
            naturalDisaster.IsPleasant = Pleasantness.NeverPleasant;
            naturalDisaster.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;

            //Roles
            var survivors = new Role("Survivors") { MinCount = 1, MaxCount = 5 };

            naturalDisaster.TheRoles.Add(survivors);

            //No Prereqs            

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, survivors, survivors, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, survivors, survivors, "Large Bonding");
            var distrust_Small = new ChangeInTrust(-1, survivors, survivors, "Small Distrust");

            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Large);

            var unlikely = new PossibleResult(25);
            unlikely.TheOutcomes.Add(bonding_Small);

            var rare = new PossibleResult(15);
            rare.TheOutcomes.Add(distrust_Small);

            naturalDisaster.ThePossibleResults.Add(common);
            naturalDisaster.ThePossibleResults.Add(unlikely);
            naturalDisaster.ThePossibleResults.Add(rare);

            return naturalDisaster;
        }

        public static TemplateForIncident Weather_Challenging()//**Specifics have weather details, need this to be generic
        {
            var badWeather = new TemplateForIncident("Challenge Due To Weather");
            badWeather.TheFrequency = Frequency.Often;
            badWeather.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var travelers = new Role("Travelers") { MinCount = 1, MaxCount = null };

            badWeather.TheRoles.Add(travelers);

            //Prereqs
            MutualTrust_Min prereq_TravelerMinTrust = new MutualTrust_Min(EthicsScale.Exploit, travelers);

            badWeather.ThePrerequisites.Add(prereq_TravelerMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, travelers, travelers, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, travelers, travelers, "Large Bonding");
            var distrust_Small = new ChangeInTrust(-1, travelers, travelers, "Small Distrust");


            var common = new PossibleResult(60);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(20);
            unlikely.TheOutcomes.Add(distrust_Small);

            var rare = new PossibleResult(15);
            rare.TheOutcomes.Add(bonding_Large);

            badWeather.ThePossibleResults.Add(common);
            badWeather.ThePossibleResults.Add(unlikely);
            badWeather.ThePossibleResults.Add(rare);

            return badWeather;
        }

        public static TemplateForIncident DangerousAnimal()//in-the-wild, loose in populated area
        {
            var dangerousAnimal = new TemplateForIncident("Dangerous Animal");
            dangerousAnimal.TheFrequency = Frequency.Periodically;
            dangerousAnimal.IsHighEnergy = EnergyLevel.AlwaysHighEnergy;
            dangerousAnimal.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var group = new Role("Group") { MinCount = 1, MaxCount = null };

            dangerousAnimal.TheRoles.Add(group);

            //Prereqs
            MutualTrust_Min prereq_GroupMinTrust = new MutualTrust_Min(EthicsScale.Beat, group);

            dangerousAnimal.ThePrerequisites.Add(prereq_GroupMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, group, group, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, group, group, "Large Bonding");
            var distrust_Small = new ChangeInTrust(-1, group, group, "Small Distrust");


            var common = new PossibleResult(50);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(bonding_Large);

            var rare = new PossibleResult(20);
            rare.TheOutcomes.Add(distrust_Small);

            dangerousAnimal.ThePossibleResults.Add(common);
            dangerousAnimal.ThePossibleResults.Add(unlikely);
            dangerousAnimal.ThePossibleResults.Add(rare);

            return dangerousAnimal;
        }

        public static TemplateForIncident Survival()//food, water, shelter
        {
            var survival = new TemplateForIncident("Survival - Seek Basic Needs");
            survival.TheFrequency = Frequency.Often;
            survival.IsPleasant = Pleasantness.NeverPleasant;

            //Roles
            var survivors = new Role("Survivors") { MinCount = 1, MaxCount = null };

            survival.TheRoles.Add(survivors);

            //Prereqs
            MutualTrust_Min prereq_TravelerMinTrust = new MutualTrust_Min(EthicsScale.Exploit, survivors);

            survival.ThePrerequisites.Add(prereq_TravelerMinTrust);

            //Outcomes
            var bonding_Small = new ChangeInTrust(1, survivors, survivors, "Small Bonding");
            var bonding_Large = new ChangeInTrust(2, survivors, survivors, "Large Bonding");
            var distrust_Small = new ChangeInTrust(-1, survivors, survivors, "Small Distrust");

            var common = new PossibleResult(50);
            common.TheOutcomes.Add(bonding_Small);

            var unlikely = new PossibleResult(30);
            unlikely.TheOutcomes.Add(distrust_Small);

            var rare = new PossibleResult(20);
            rare.TheOutcomes.Add(bonding_Large);


            survival.ThePossibleResults.Add(common);
            survival.ThePossibleResults.Add(unlikely);
            survival.ThePossibleResults.Add(rare);

            return survival;
        }

        #endregion

        /*

       3+ Role Incidents
       {anything normal event with added observers / witnesses}

        */

    }
}
