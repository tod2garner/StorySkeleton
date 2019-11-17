using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    [DataContract]
    /// <summary>
    /// Group of characters playing the same role within a specific incident.
    /// </summary>
    public class Role
    {
        public const int DEFAULT_ROLE_MAX_COUNT = 3; //#TODO - move to config file?

        public Role(string roleName)
        {
            this.name = roleName;
            myParticipants = new List<Character>();
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public Role()
        {
            myParticipants = new List<Character>();
        }

        [DataMember]
        private List<Character> myParticipants;
        public List<Character> Participants { get { return myParticipants; } }

        [DataMember]
        private string name;
        /// <summary>
        /// Used to coordinate between multiple prerequisites simultaneously when trying to populate an event
        /// </summary>
        public string RoleName { get { return name; } }

        [DataMember]
        private int? minCount;
        /// <summary>
        /// Minimum character count required.
        /// If min is null the role can be left empty and is assumed filled by unnamed minor character(s).
        /// </summary>
        public int? MinCount
        {
            get { return minCount; }
            set { minCount = value; }
        }

        [DataMember]
        private int? maxCount;
        public int? MaxCount
        {
            get { return maxCount; }
            set { maxCount = value; }
        }

        public Role Copy(bool CopyParticipants = false)
        {
            var theCopy = new Role(this.name);
            theCopy.MinCount = this.minCount;
            theCopy.maxCount = this.maxCount;

            if (CopyParticipants && this.Participants.Any())
            {
                foreach (Character c in this.Participants)
                    theCopy.Participants.Add(c.Copy());
            }
                

            return theCopy;
        }

        public bool AreMinAndMaxMet()
        {
            if (this.MinCount.HasValue && this.Participants.Count < this.MinCount)
                return false;

            if (this.MaxCount.HasValue && this.Participants.Count > this.MaxCount)
                return false;

            return true;
        }

        public bool AddOneParticipantRandomly(List<Character> theCandidates, Random rng = null)
        {
            if (rng == null)
                rng = new Random();

            if (theCandidates.Count == 0)
                return false;

            var nextChosen = theCandidates[rng.Next(0, theCandidates.Count)];
            this.Participants.Add(nextChosen);
            theCandidates.Remove(nextChosen);

            return true;
        }

        public bool AddParticipantsRandomly(List<Character> theCandidates, Random rng = null)
        {
            return AddParticipants_OptionalRetest(theCandidates, null, rng);
        }

        public bool AddParticipantsRandomly_RetestingAfterEach(List<Character> theCandidates, List<IPrerequisite> thePrereqs, Random rng = null)
        {
            return AddParticipants_OptionalRetest(theCandidates, thePrereqs, rng);
        }

        private bool AddParticipants_OptionalRetest(List<Character> theCandidates, List<IPrerequisite> thePrereqs = null, Random rng = null)
        {
            if (rng == null)
                rng = new Random();

            int min = this.MinCount.HasValue ? this.MinCount.Value : 0; //role can be left empty, assumed filled by unnamed minor characters
            int max = this.MaxCount.HasValue ? this.MaxCount.Value : DEFAULT_ROLE_MAX_COUNT;
            max = System.Math.Min(max, theCandidates.Count);

            if (min > max)
                return false;

            int targetCount = rng.Next(min, max + 1);
            int stillNeeded = targetCount - this.Participants.Count;

            for (int i = 0; i < stillNeeded; i++)
            {
                if (thePrereqs != null)
                    theCandidates = CandidatesThatPassPrereqs(theCandidates, thePrereqs);

                var result = AddOneParticipantRandomly(theCandidates, rng);
                if (result == false)
                    break;
            }

            return true;
        }

        public List<Character> CandidatesThatPassPrereqs(List<Character> theCandidates, List<IPrerequisite> thePrereqs)
        {
            var passedAllTests = new List<Character>();
            passedAllTests = theCandidates.Where(c => false == thePrereqs.Any(p => false == p.WouldBeMetBySuggestedParticipant(c, this.RoleName))).ToList();
            return passedAllTests;
        }

        public List<Character> FirstCandidateOptions(List<Character> theCandidates, List<IPrerequisite> thePrereqs, SocietySnapshot currentCast)
        {
            var viableFirstCandidates = new List<Character>();
            viableFirstCandidates = theCandidates.Where(c => false == thePrereqs.Any(p => false == p.IsCharacterViableFirstCandidateForRole(c, this.RoleName, currentCast))).ToList();
            return viableFirstCandidates;
        }
    }
}
