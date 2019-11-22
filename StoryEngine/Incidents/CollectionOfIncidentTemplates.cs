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
            //#TODO - add step to sort/exclude by rarity

            var diceRoll = rng.Next(0, this.theTemplates.Count);
            return this.theTemplates[diceRoll].CreateIncident(rng);
        }
    }
}
