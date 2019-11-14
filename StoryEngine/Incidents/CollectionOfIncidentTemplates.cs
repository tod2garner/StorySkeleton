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
            LoadFromFile();
        }

        private List<TemplateForIncident> allTemplates;

        public List<TemplateForIncident> AllIncidents
        {
            get { return allTemplates; }
            set { allTemplates = value; }
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

            var diceRoll = rng.Next(0, this.allTemplates.Count);
            return this.allTemplates[diceRoll].CreateIncident();
        }
    }
}
