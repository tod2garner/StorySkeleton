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

        [DataMember]
        private Age theAge;
        [DataMember]
        private Beauty theBeauty;
        [DataMember]
        private Dexterity theDexterity;
        [DataMember]
        private Grooming theGrooming;
        [DataMember]
        private Height theHeight;
        [DataMember]
        private Strength theStrength;
        [DataMember]
        private Weight theWeight;

        [DataMember]
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