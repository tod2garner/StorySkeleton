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
    public class ParticipantRole
    {
        ParticipantRole()
        {
            theParticipants = new List<Character>();
            allPossibleOutcomes = new List<PossibleOutcome>();
        }

        private List<Character> theParticipants;
        public List<Character> Participants { get { return theParticipants; } }
        
        private List<PossibleOutcome> allPossibleOutcomes;
        public List<PossibleOutcome> AllPossibleOutcomes { get { return allPossibleOutcomes; } }        

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
