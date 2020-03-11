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
    public class Motivation
    {
        public Motivation()
        {
            description = new List<string>();

            theCoreDesire = new CoreDesire();
            theyEnjoy = new Enjoys();
        }

        [DataMember]
        private CoreDesire theCoreDesire;
        [DataMember]
        private Enjoys theyEnjoy;

        [DataMember]
        private List<string> description;
        public List<string> Description { get { return description; } }

        public void Randomize(Random rng)
        {
            description.Clear();
            description.Add("Core Desire: " + theCoreDesire.Randomize(rng));
            description.Add("Enjoys: " + theyEnjoy.Randomize(rng));
        }
    }
}