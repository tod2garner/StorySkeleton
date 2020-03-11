using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine.Specifics.CharacterDetails;
using System.Runtime.Serialization;

namespace StoryEngine.Specifics
{
    [DataContract]
    public class Background
    {
        public Background()
        {
            description = new List<string>();

            theBornClass = new BornClass();
            theCurrentClass = new CurrentClass();
            theWorkExperience = new WorkExperience();
        }

        [DataMember]
        private BornClass theBornClass;
        [DataMember]
        private CurrentClass theCurrentClass;
        [DataMember]
        private WorkExperience theWorkExperience;

        [DataMember]
        private List<string> description;
        public List<string> Description { get { return description; } }

        public void Randomize(Random rng)
        {
            description.Clear();
            description.Add("Born " + theBornClass.Randomize(rng));
            description.Add("Currently " + theCurrentClass.Randomize(rng));
            description.Add("Work Experience: " + theWorkExperience.Randomize(rng));
        }
    }
}