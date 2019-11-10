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
    public class Role
    {
        public const int DEFAULT_ROLE_MAX_COUNT = 4; //#TODO - move to config file?

        public Role(string roleName)
        {
            this.name = roleName;
            myParticipants = new List<Character>();
        }

        private List<Character> myParticipants;
        public List<Character> Participants { get { return myParticipants; } }

        private string name;
        /// <summary>
        /// Used to coordinate between multiple prerequisites simultaneously when trying to populate an event
        /// </summary>
        public string RoleName { get { return name; } }

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

        private int? maxCount;
        public int? MaxCount
        {
            get { return maxCount; }
            set { maxCount = value; }
        }

        public bool AreMinAndMaxMet()
        {
            if (this.MinCount.HasValue && this.Participants.Count < this.MinCount)
                return false;

            if (this.MaxCount.HasValue && this.Participants.Count > this.MaxCount)
                return false;

            return true;
        }

    }
}
