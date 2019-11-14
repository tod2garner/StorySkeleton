using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class CollectionOfIncidentTemplates
    {
        public CollectionOfIncidentTemplates()
        {
            theTemplates = new List<TemplateForIncident>();
        }

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
            return this.theTemplates[diceRoll].CreateIncident();
        }
    }
}
