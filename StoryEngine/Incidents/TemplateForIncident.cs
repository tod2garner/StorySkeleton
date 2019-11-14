using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    [Serializable]
    public class TemplateForIncident
    {
        public string Name;

        public List<Role> TheRoles;

        public List<APrerequisite> ThePrerequisites;

        public List<PossibleResult> ThePossibleResults;

        public TemplateForIncident(string theName)
        {
            Name = theName;
            TheRoles = new List<Role>();
            ThePrerequisites = new List<APrerequisite>();
            ThePossibleResults = new List<PossibleResult>();
        }

        public TemplateForIncident()
        {
            TheRoles = new List<Role>();
            ThePrerequisites = new List<APrerequisite>();
            ThePossibleResults = new List<PossibleResult>();
        }

        public IIncident CreateIncident()
        {
            var theIncident = new Incident(Name);

            foreach(Role r in TheRoles)
                theIncident.AllParticipantRoles.Add(r.Copy());
                        
            foreach (IPrerequisite p in ThePrerequisites)//Add prerequisite --- Must re-wire all roles to the new copies
                theIncident.MyPrerequisites.Add(p.Copy(theIncident.AllParticipantRoles));
                        
            foreach (PossibleResult pr in ThePossibleResults)//Add outcomes --- Must re-wire all roles to the new copies
                theIncident.AllPossibleOutcomes.Add(pr.Copy(theIncident.AllParticipantRoles));

            return theIncident;
        }
    }
}
