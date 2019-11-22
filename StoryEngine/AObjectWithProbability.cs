using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    [DataContract]
    [KnownType(typeof(PossibleResult))]
    [KnownType(typeof(CollectionOfIncidentTemplates))]
    public abstract class AObjectWithProbability
    {
        public AObjectWithProbability(int theProbabilityScore)
        {
            this.probabilityScore = theProbabilityScore;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public AObjectWithProbability() { }

        [DataMember]
        private int probabilityScore;
        public int ProbabilityScore
        {
            get { return probabilityScore; }
        }

        public static T PickOne<T>(IEnumerable<T> theList, Random rng) where T : AObjectWithProbability
        {
            if (rng == null)
                rng = new Random();

            var totalPercentChanceOfCollections = theList.Sum(t => t.probabilityScore);
            var diceRoll = rng.Next(0, totalPercentChanceOfCollections);

            foreach (T p in theList)
            {
                if (diceRoll < p.probabilityScore)
                {
                    return p;
                }

                diceRoll -= p.probabilityScore;
            }

            return null;
        }
    }
    
}
