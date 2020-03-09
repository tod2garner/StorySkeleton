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
        public Frequency TheFrequency;

        [DataMember]
        public EnergyLevel IsHighEnergy;

        [DataMember]
        public Pleasantness IsPleasant;

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

        public Incident CreateIncident(Random rng)
        {
            var theIncident = new Incident(Name);

            theIncident.TheEnergyVariation = this.IsHighEnergy;
            theIncident.TheStressVariation = this.IsPleasant;
            theIncident.TheFrequency = this.TheFrequency;
            theIncident.SetToneRandomly(rng);

            theIncident.TheSetting.Randomize(rng);

            foreach(Role r in TheRoles)
                theIncident.AllParticipantRoles.Add(r.Copy());
                        
            foreach (IPrerequisite p in ThePrerequisites)//Add prerequisite --- Must re-wire all roles to the new copies
                theIncident.MyPrerequisites.Add(p.Copy(theIncident.AllParticipantRoles));
                        
            foreach (PossibleResult pr in ThePossibleResults)//Add outcomes --- Must re-wire all roles to the new copies
                theIncident.AllPossibleOutcomes.Add(pr.Copy(theIncident.AllParticipantRoles));

            return theIncident;
        }


        public Incident CreateIncident(Random rng, Pleasantness forced_P, EnergyLevel forced_E)
        {
            var theI = CreateIncident(rng);
            theI.SetToneRandomly_WithLimits(rng, forced_P, forced_E);
            return theI;
        }
    }
}
