using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Incident : AIncident
    {
        public Incident() : base()
        {
            allParticipants = new List<Role>();
        }

        private List<Role> allParticipants;
        public List<Role> AllParticipants { get { return allParticipants; } }
    }
}
