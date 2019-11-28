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
            allRoles = new List<Role>();
            allPossibleOutcomes = new List<PossibleResult>();

            textSummary = new List<string>();
            outcomeTextSummary = new List<string>();
        }

        private string name;
        public string Name { get { return name; } }

        private Frequency theFrequency;
        public Frequency TheFrequency { get { return theFrequency; } }

        protected List<string> textSummary;
        public List<string> GetTextSummary() { return textSummary; }

        private List<string> outcomeTextSummary;
        public List<string> OutcomeTextSummary { get { return outcomeTextSummary; } }


        protected List<IPrerequisite> prerequisites;
        public List<IPrerequisite> MyPrerequisites { get { return prerequisites; } }

        private List<PossibleResult> allPossibleOutcomes;
        public List<PossibleResult> AllPossibleOutcomes { get { return allPossibleOutcomes; } }

        private List<Role> allRoles;
        public List<Role> AllParticipantRoles { get { return allRoles; } }

        public void PopulateAllRoles_Randomly(SocietySnapshot currentCast, Random rng)
        {
            if (rng == null)
                rng = new Random();

            var nonParticipants = currentCast.AllCharacters.ToList();//must copy list to avoid changes to original

            foreach (Role r in this.allRoles)
            {
                r.AddParticipantsRandomly(nonParticipants, rng);
                nonParticipants = nonParticipants.Where(n => false == r.Participants.Any(p => n.Id == p.Id)).ToList();
            }
        }

        public void PopulateAllRoles_FollowingPrereqs(SocietySnapshot currentCast, Random rng)
        {
            if (rng == null)
                rng = new Random();

            var nonParticipants = currentCast.AllCharacters.ToList();//must copy list to avoid changes to original

            //Start with whichever role is hardest to fill
            var rolesOrderedByCountOfFirstCandidates = allRoles.OrderBy(r =>
                            r.FirstCandidateOptions(nonParticipants, MyPrerequisites, currentCast).Count
                            + (r.MinCount == 0 ? 1 : 0));//add to count if min particpants == 0

            var roleToStartWith = rolesOrderedByCountOfFirstCandidates.First();
            var firstCandidates = roleToStartWith.FirstCandidateOptions(nonParticipants, MyPrerequisites, currentCast);
            roleToStartWith.AddOneParticipantRandomly(firstCandidates, rng);
            nonParticipants = nonParticipants.Where(n => false == roleToStartWith.Participants.Any(p => n.Id == p.Id)).ToList();

            //One cycle through roles, adding one participant to each
            foreach (Role r in this.allRoles)
            {
                if (r.RoleName == roleToStartWith.RoleName)
                    continue;

                if (r.MinCount == 0 && rng.Next(0, Role.DEFAULT_ROLE_MAX_COUNT) == 0)//chance to leave empty roles that are allow to have 0 participants
                    continue;

                var theCandidates = r.CandidatesThatPassPrereqs(nonParticipants, MyPrerequisites);
                r.AddOneParticipantRandomly(theCandidates, rng);
                nonParticipants = nonParticipants.Where(n => false == r.Participants.Any(p => n.Id == p.Id)).ToList();
            }

            //Cycle again, adding further participants
            foreach (Role r in this.allRoles)
            {
                r.AddParticipantsRandomly_RetestingAfterEach(nonParticipants, MyPrerequisites, rng);
                nonParticipants = nonParticipants.Where(n => false == r.Participants.Any(p => n.Id == p.Id)).ToList();
            }
        }

        public bool TryToPopulateIncident(SocietySnapshot currentCast, Random rng)
        {
            if (MyPrerequisites.Any() == false)
            {
                PopulateAllRoles_Randomly(currentCast, rng);
                return true;
            }
            else
            {
                PopulateAllRoles_FollowingPrereqs(currentCast, rng);
            }

            var allAreFulfilled = !prerequisites.Any(p => p.IsMetByCurrentParticipants() == false);
            return allAreFulfilled;
        }

        public void RollDiceAndExecuteOneOutcome(SocietySnapshot currentCast, Random rng)
        {
            InitializeTextSummary();

            var chosen = AObjectWithProbability.PickOne(AllPossibleOutcomes, rng);

            this.outcomeTextSummary.AddRange(chosen.Execute());
            this.textSummary.AddRange(outcomeTextSummary);
        }

        public virtual void InitializeTextSummary()
        {
            this.textSummary = new List<string>();
            this.textSummary.Add(string.Format("INCIDENT: {0}", this.name));

            foreach (Role r in AllParticipantRoles)
            {
                var roleParticipantSummary = string.Format("    {0}: ", r.RoleName);

                if (r.Participants.Any() == false)
                    roleParticipantSummary += "Unnamed minor character(s)";
                else
                {
                    foreach (Character c in r.Participants)
                        roleParticipantSummary += c.Name + ", ";
                }

                textSummary.Add(roleParticipantSummary);
            }
        }
    }
}
