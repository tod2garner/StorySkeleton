using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine.Specifics.CharacterDetails;

namespace StoryEngine.Specifics
{
    public class Background
    {
        public Background()
        {
            description = new List<string>();

            theBornClass = new BornClass();
            theCurrentClass = new CurrentClass();
            theWorkExperience = new WorkExperience();
        }

        private BornClass theBornClass;
        private CurrentClass theCurrentClass;
        private WorkExperience theWorkExperience;

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