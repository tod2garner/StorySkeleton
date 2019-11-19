using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.SocietyGenerators
{
    public class RelationshipGenerator_Default : IRelationshipGenerator
    {
        public Relationship CreateRelationship(Character self, Character other, Random rng = null)
        {
            EthicsScale initialTrust;
            EthicsScale intialEthics;

            if (rng == null)
                rng = new Random();

            bool goodFirstImpression = rng.Next(0, 2) == 0;

            switch (self.BaseSuspicion)
            {
                case SuspicionScale.Naive:
                    initialTrust = goodFirstImpression ? EthicsScale.Embrace : EthicsScale.Cooperate;
                    break;
                case SuspicionScale.Relaxed:
                    initialTrust = goodFirstImpression ? EthicsScale.Cooperate : EthicsScale.Coexist;
                    break;
                case SuspicionScale.Average:
                    initialTrust = goodFirstImpression ? EthicsScale.Coexist : EthicsScale.Exploit;
                    break;
                case SuspicionScale.Guarded:
                    initialTrust = EthicsScale.Exploit;
                    break;
                case SuspicionScale.Paranoid:
                default:
                    initialTrust = EthicsScale.Beat;
                    break;
            }

            switch (self.BaseMorality)
            {
                case Morality.Forgive:
                    intialEthics = goodFirstImpression ? EthicsScale.Cooperate : EthicsScale.Coexist;
                    break;
                case Morality.Reciprocate:
                    intialEthics = initialTrust;
                    break;
                case Morality.Exploit:
                default:
                    intialEthics = EthicsScale.Exploit;
                    break;
            }

            var r = new Relationship(self.Id, other.Id, initialTrust, intialEthics);

            return r;
        }
    }
}
