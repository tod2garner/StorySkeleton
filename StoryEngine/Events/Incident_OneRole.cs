using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Incident_OneRole : AIncident
    {
        public Incident_OneRole(IncidentRole givenRole, List<IPrerequisite> givenPrereqs) : base(givenPrereqs)
        {
            participants = givenRole;
        }

        private IncidentRole participants;
        public IncidentRole Participants { get { return participants; } }
    }
}
