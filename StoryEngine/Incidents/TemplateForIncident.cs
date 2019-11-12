using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    [Serializable]//#TODO - tag properties
    public class TemplateForIncident
    {
        public string Name;

        public List<Role> TheRoles;

        public List<IPrerequisite> ThePrerequisites;

        public List<PossibleResult> ThePossibleResults;

        public TemplateForIncident()
        {
            TheRoles = new List<Role>();
            ThePrerequisites = new List<IPrerequisite>();
            ThePossibleResults = new List<PossibleResult>();
        }

        public IIncident CreateIncident()
        {
            var theIncident = new Incident(Name);

            foreach(Role r in TheRoles)
                theIncident.AllParticipantRoles.Add(r.Copy());

            //Add prerequisite --- Must re-wire all roles to the new copies
            foreach (IPrerequisite p in ThePrerequisites)
                theIncident.MyPrerequisites.Add(p.Copy(theIncident.AllParticipantRoles));

            //Add outcomes --- Must re-wire all roles to the new copies
            foreach (PossibleResult pr in ThePossibleResults)
                theIncident.AllPossibleOutcomes.Add(pr.Copy(theIncident.AllParticipantRoles));

            return theIncident;
        }
    }
}
