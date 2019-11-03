using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    /// <summary>
    /// Group of characters playing the same role within a specific incident.
    /// </summary>
    public class ParticipantRole
    {
        public ParticipantRole()
        {
            theParticipants = new List<Character>();
            allPossibleOutcomes = new List<PossibleResult>();
        }

        private List<Character> theParticipants;
        public List<Character> Participants { get { return theParticipants; } }
        
        private List<PossibleResult> allPossibleOutcomes;
        public List<PossibleResult> AllPossibleOutcomes { get { return allPossibleOutcomes; } }        

        public bool IsOutcome100Percent()
        {
            int totalOutcomePercent = 0;

            foreach(PossibleResult p in allPossibleOutcomes)
            {
                totalOutcomePercent += p.PercentChance;
            }

            return totalOutcomePercent == 100;
        }
    }
}
