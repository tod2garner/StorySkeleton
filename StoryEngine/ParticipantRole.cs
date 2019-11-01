using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    /// <summary>
    /// Group of characters playing the same role within a specific event.
    /// </summary>
    class ParticipantRole
    {
        ParticipantRole(int? givenMin, int? givenMax)
        {
            this.min = givenMin;
            this.max = givenMax;

            prerequisites = new List<IPrerequisite>();
            theParticipants = new List<Character>();
            allPossibleOutcomes = new List<PossibleOutcome>();
        }


        private List<IPrerequisite> prerequisites;
        public List<IPrerequisite> MyPrerequisites { get { return prerequisites; } }

        private int? min;
        public int? MinCount { get { return min; } }

        private int? max;
        public int? MaxCount { get { return max; } }

        private List<Character> theParticipants;
        public List<Character> Participants { get { return theParticipants; } }
        
        private List<PossibleOutcome> allPossibleOutcomes;
        public List<PossibleOutcome> AllPossibleOutcomes { get { return allPossibleOutcomes; } }

        public bool AreAllPrerequisitesMet()
        {
            return !prerequisites.Any(p => p.IsFulfilled() == false);
        }

        public bool IsValidNumberParticipating()
        {
            if(min.HasValue)
            {
                if (theParticipants.Count < min.Value)
                    return false;
            }

            if (max.HasValue)
            {
                if (theParticipants.Count > max.Value)
                    return false;
            }

            return true;
        }

        public bool IsOutcome100Percent()
        {
            int totalOutcomePercent = 0;

            foreach(PossibleOutcome p in allPossibleOutcomes)
            {
                totalOutcomePercent += p.PercentChance;
            }

            return totalOutcomePercent == 100;
        }
    }
}
