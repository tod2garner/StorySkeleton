using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine.Specifics.CharacterDetails;

namespace StoryEngine.Specifics
{
    public class Appearance
    {
        public Appearance()
        {
            description = new List<string>();

            theAge = new Age();
            theBeauty = new Beauty();
            theDexterity = new Dexterity();
            theGrooming = new Grooming();
            theHeight = new Height();
            theStrength = new Strength();
            theWeight = new Weight();
        }

        private Age theAge;
        private Beauty theBeauty;
        private Dexterity theDexterity;
        private Grooming theGrooming;
        private Height theHeight;
        private Strength theStrength;
        private Weight theWeight;

        private List<string> description;
        public List<string> Description { get { return description; } }

        public void Randomize(Random rng)
        {
            description.Clear();
            description.Add(theAge.Randomize(rng));
            description.Add(theBeauty.Randomize(rng));
            description.Add(theHeight.Randomize(rng));
            description.Add(theWeight.Randomize(rng));
            description.Add(theGrooming.Randomize(rng));
            description.Add(theStrength.Randomize(rng));
            description.Add(theDexterity.Randomize(rng));
        }
    }
}