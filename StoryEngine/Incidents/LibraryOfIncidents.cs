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
            allCollections = new List<Tuple<int, CollectionOfIncidentTemplates>>();
        }

        private List<Tuple<int, CollectionOfIncidentTemplates>> allCollections;
        /// <summary>
        /// List of template collections, each with an assigned rarity as the 1st tuple item
        /// </summary>
        public List<Tuple<int, CollectionOfIncidentTemplates>> AllCollections { get { return allCollections; } }

    }
}
