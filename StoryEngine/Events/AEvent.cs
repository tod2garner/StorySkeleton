using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    abstract class AEvent : IEvent
    {
        //Future: triggers, chance to trigger other event(s)
        
        public bool AreAllPrerequisitesMet()
        {
            return !allParticipants.Any(p => p.AreAllPrerequisitesMet() == false);
        }
        
        private List<ParticipantRole> allParticipants;
        public List<ParticipantRole> AllParticipants { get { return allParticipants; } }


        //Concrete classes
        //          One role - participants
        //          Two role - acting, acted upon
        //Future:
        //      Add classes with observer roles
        //      Multi-stage events - recursive triggers, and option to force a final trigger (e.g. deception)
    }
}
