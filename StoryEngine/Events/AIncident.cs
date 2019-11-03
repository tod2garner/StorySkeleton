using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class AIncident : IIncident
    {
        //Future: triggers, chance to trigger other incident(s)
        
        public bool AreAllPrerequisitesMet(SocietySnapshot currentCast)
        {
            return !prerequisites.Any(p => p.CanBeFulfilled(currentCast) == false);
        }

        private List<ParticipantRole> allParticipants;
        public List<ParticipantRole> AllParticipants { get { return allParticipants; } }

        private List<IPrerequisite> prerequisites;
        public List<IPrerequisite> MyPrerequisites { get { return prerequisites; } }

        //Concrete classes
        //          One role - participants
        //          Two role - acting, acted upon
        //          3 roles  - acting, acted upon, interrupting (e.g. hero rescues victim from attackers)
        //Future:
        //      Add classes with observer roles
        //      Multi-stage events - recursive triggers, and option to force a final trigger (e.g. deception)
    }
}
