using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics
{

    [DataContract]
    public abstract class ASpecific
    {
        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public ASpecific() { }

        public ASpecific(string givenName)
        {
            this.name = givenName;
            variations = new List<Detail>();
        }

        [DataMember]
        private string name;
        public string Name { get { return name; } }

        [DataMember]
        protected List<Detail> variations;
        public List<Detail> Variations { get { return variations; } }

        public string Randomize(Random rng)
        {
            return AObjectWithProbability.PickOne(variations, rng).Description;
        }
    }

    [DataContract]
    public class Detail : AObjectWithProbability
    {
        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public Detail() { }

        public Detail(int givenProbability, string givenDescription) : base(givenProbability)
        {
            description = givenDescription;
        }

        [DataMember]
        private string description;
        public string Description { get { return description; } }
    }
}
