using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    [DataContract]
    public class CollectionOfIncidentTemplates : AObjectWithProbability
    {
        public CollectionOfIncidentTemplates(int theProbabilityScore) : base(theProbabilityScore)
        {
            theTemplates = new List<TemplateForIncident>();
        }

        [DataMember]
        private List<TemplateForIncident> theTemplates;
        public List<TemplateForIncident> TheTemplates
        {
            get { return theTemplates; }
            set { theTemplates = value; }
        }                       

        public void LoadFromFile()
        {
            throw new NotImplementedException(); //#TODO
        }

        public void SaveToFile()
        {
            throw new NotImplementedException();//#TODO
        }

        public IIncident GetRandomIncident(Random rng)
        {
            //First, exclude by rarity
            var maxRarity = IncidentEnumExtensions.GetRandomFrequency_Weighted(rng);
            var possibleTemplates = this.TheTemplates.Where(t => t.TheFrequency <= maxRarity).ToList();

            var diceRoll = rng.Next(0, possibleTemplates.Count);
            return possibleTemplates[diceRoll].CreateIncident(rng);
        }
    }
}
