using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.SocietyGenerators
{
    public class RelationshipGenerator_Default : IRelationshipGenerator
    {
        public Relationship CreateRelationship(Character self, Character other)
        {
            EthicsScale initialTrust;
            EthicsScale intialEthics;

            switch(self.BaseSuspicion)
            {
                case SuspicionScale.Naive:
                    initialTrust = EthicsScale.Cooperate;
                    break;
                case SuspicionScale.Relaxed:
                case SuspicionScale.Average:
                    initialTrust = EthicsScale.Coexist;
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
                    intialEthics = EthicsScale.Cooperate;
                    break;
                case Morality.Reciprocate:
                    intialEthics = EthicsScale.Coexist;
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
