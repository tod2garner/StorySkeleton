﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class AIncident : IIncident
    {
        //#TODO Future: triggers, chance to trigger other incident(s)
        
        //Future:
        //      Multi-stage events - recursive triggers, and option to force a final trigger (e.g. deception)

        public AIncident()
        {
            prerequisites = new List<IPrerequisite>();
            allPossibleOutcomes = new List<PossibleResult>();
        }

        protected List<IPrerequisite> prerequisites;
        public List<IPrerequisite> MyPrerequisites { get { return prerequisites; } }

        private List<PossibleResult> allPossibleOutcomes;
        public List<PossibleResult> AllPossibleOutcomes { get { return allPossibleOutcomes; } }
        
        public abstract void PopulateAllRolesRandomly(SocietySnapshot currentCast, Random rng = null);

        public static void AddParticipantsRandomly(Role theRole, List<Character> theCandidates, Random rng = null)
        {
            if (rng == null)
                rng = new Random();

            int min = theRole.MinCount.HasValue ? theRole.MinCount.Value : 0; //role can be left empty, assumed filled by unnamed minor characters
            int max = theRole.MaxCount.HasValue ? theRole.MaxCount.Value : Role.DEFAULT_ROLE_MAX_COUNT;
            max = System.Math.Min(max, theCandidates.Count);

            if (min > max)
                return;

            int targetCount = rng.Next(min, max + 1);

            for (int i = 0; i < targetCount; i++)
            {
                if (theCandidates.Count == 0)
                    break;

                var nextChosen = theCandidates[rng.Next(0, theCandidates.Count)];
                theRole.Participants.Add(nextChosen);
                theCandidates.Remove(nextChosen);
            }
        }
        
        public bool TryToFulfillAllPrerequisites(SocietySnapshot currentCast)
        {
            if(MyPrerequisites.Any() == false)
            {
                PopulateAllRolesRandomly(currentCast);
                return true;
            }

            //#TODO - fix for multiple prerequisites, simultaneous
            var primaryPrereq = MyPrerequisites.First(); //first in list always given priority
            var ableToFill = primaryPrereq.TryToFulfillFromScratch(currentCast);

            return !prerequisites.Any(p => p.IsMetByCurrentParticipants() == false);
        }

        public int GetTotalOutcomePercentChance()
        {
            int totalOutcomePercent = 0;

            foreach (PossibleResult p in allPossibleOutcomes)
            {
                totalOutcomePercent += p.PercentChance;
            }

            return totalOutcomePercent;
        }

        public void RollDiceAndExecuteOneOutcome(SocietySnapshot currentCast, Random rng = null)
        {

            if (rng == null)
                rng = new Random();

            var diceRoll = rng.Next(0, GetTotalOutcomePercentChance());

            foreach (PossibleResult p in allPossibleOutcomes)
            {
                if (diceRoll < p.PercentChance)
                {
                    p.Execute();
                    return;
                }

                diceRoll -= p.PercentChance;
            }
        }
    }
}
