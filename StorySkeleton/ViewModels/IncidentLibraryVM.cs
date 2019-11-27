using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorySkeleton.ViewModels
{
    public class IncidentLibraryVM : ViewModel_Base
    {
        public StoryEngine.LibraryOfIncidents MyBase;

        public IncidentLibraryVM()
        {
            //#TODO - fix later to allow user to select between different libraries
            MyBase = StoryEngine.Incidents.DefaultLibrary.DefaultLibraryGenerator.LoadDefaultLibraryFromFile();
        }
    }
}
