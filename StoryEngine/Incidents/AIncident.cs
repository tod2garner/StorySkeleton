using System;
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

        public AIncident(string givenName)
        {
            this.name = givenName;
            prerequisites = new List<IPrerequisite>();
            allPossibleOutcomes = new List<PossibleResult>();
        }

        private string name;
        public string Name { get { return name; } }

        protected List<string> textSummary;
        public List<string> GetTextSummary() { return textSummary; }

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

        public static List<Character> TestCandidatesAgainstOtherPrereqs(List<Character> myCandidates, string suggestedRole, List<IPrerequisite> otherPrereqs)
        {
            var passedAllTests = new List<Character>(myCandidates.Count);
            passedAllTests = myCandidates.Where(c => false == otherPrereqs.Any(p => false == p.WouldBeMetBySuggestedParticipant(c, suggestedRole))).ToList();
            return passedAllTests;
        }

        public static void AddParticipantsRandomly_RetestingAfterEach(Role theRole, List<Character> theCandidates, List<IPrerequisite> otherPrereqs, Random rng = null)
        {
            throw new NotImplementedException();             
        }

        public bool TryToFulfillAllPrerequisites(SocietySnapshot currentCast, Random rng = null)
        {
            if(MyPrerequisites.Any() == false)
            {
                PopulateAllRolesRandomly(currentCast, rng);
                return true;
            }

            //#TODO - fix for multiple prerequisites, simultaneous
            var primaryPrereq = MyPrerequisites.First(); //first in list always given priority - #TODO, change to most roles involved
            var otherPrereqs = MyPrerequisites.Where(p => p != primaryPrereq).ToList();
            var ableToFill = primaryPrereq.TryToFulfillFromScratch(currentCast, otherPrereqs, rng);

            var allAreFulfilled = !prerequisites.Any(p => p.IsMetByCurrentParticipants() == false);
            return allAreFulfilled;
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
            this.textSummary = new List<string>();
            this.textSummary.Add(string.Format("INCIDENT: {0}", this.name));

            if (rng == null)
                rng = new Random();

            var diceRoll = rng.Next(0, GetTotalOutcomePercentChance());

            foreach (PossibleResult p in allPossibleOutcomes)
            {
                if (diceRoll < p.PercentChance)
                {
                    this.textSummary.AddRange(p.Execute());
                    return;
                }

                diceRoll -= p.PercentChance;
            }
        }
    }
}
