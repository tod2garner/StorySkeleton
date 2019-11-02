using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class IncidentLibrary
    {
        public IncidentLibrary()
        {
            LoadFromFile();
        }

        private List<IIncident> allIncidents;

        public List<IIncident> AllIncidents
        {
            get { return allIncidents; }
            set { allIncidents = value; }
        }
                       

        private void LoadFromFile()
        {
            throw new NotImplementedException();
        }

        private void SaveToFile()
        {
            throw new NotImplementedException();
        }
    }
}
