using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class LibraryOfIncidents
    {
        public LibraryOfIncidents()
        {
            allCollections = new List<CollectionOfIncidentTemplates>();
        }

        private List<CollectionOfIncidentTemplates> allCollections;
        public List<CollectionOfIncidentTemplates> AllCollections { get { return allCollections; } }


        public CollectionOfIncidentTemplates ChooseRandomCollection(Random rng)
        {
            return AObjectWithProbability.PickOne(AllCollections, rng);
        }
    }
}
