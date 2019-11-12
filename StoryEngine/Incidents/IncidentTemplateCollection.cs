using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class IncidentTemplateCollection
    {
        public IncidentTemplateCollection()
        {
            LoadFromFile();
        }

        private List<TemplateForIncident> allTemplates;

        public List<TemplateForIncident> AllIncidents
        {
            get { return allTemplates; }
            set { allTemplates = value; }
        }
                       

        private void LoadFromFile()
        {
            throw new NotImplementedException(); //#TODO
        }

        private void SaveToFile()
        {
            throw new NotImplementedException();//#TODO
        }
    }
}
