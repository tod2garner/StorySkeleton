using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    [DataContract]
    public class TemplateForIncident
    {
        [DataMember]
        public string Name;
        
        [DataMember]
        public EnergyVariation TheEnergyVariation;

        [DataMember]
        public StressVariation TheStressVariation;

        [DataMember]
        public List<Role> TheRoles;

        [DataMember]
        public List<APrerequisite> ThePrerequisites;

        [DataMember]
        public List<PossibleResult> ThePossibleResults;

        public TemplateForIncident(string theName)
        {
            Name = theName;
            TheRoles = new List<Role>();
            ThePrerequisites = new List<APrerequisite>();
            ThePossibleResults = new List<PossibleResult>();
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public TemplateForIncident()
        {
            TheRoles = new List<Role>();
            ThePrerequisites = new List<APrerequisite>();
            ThePossibleResults = new List<PossibleResult>();
        }

        public IIncident CreateIncident(Random rng)
        {
            var theIncident = new Incident(Name);

            theIncident.TheEnergyVariation = this.TheEnergyVariation;
            theIncident.TheStressVariation = this.TheStressVariation;
            theIncident.SetToneRandomly(rng);

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
