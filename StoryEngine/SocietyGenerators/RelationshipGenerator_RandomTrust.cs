using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.SocietyGenerators
{
    public class RelationshipGenerator_RandomTrust : IRelationshipGenerator
    {
        public Relationship CreateRelationship(Character self, Character other)
        {
            return CreateRelationship(self, other, null);
        }

        public Relationship CreateRelationship(Character self, Character other, Random rng = null)
        {
            if (other.IsAcquaintedWith(self.Id))
                return MirrorTrustFromOther(self, other);
            else
                return NewRandomTrustRelation(self.Id, other.Id, rng);
        }

        protected Relationship NewRandomTrustRelation(int selfId, int otherId, Random rng = null)
        {
            EthicsScale initialTrust;

            if (rng == null)
                rng = new Random();

            var diceRoll = rng.Next(16);

            switch (diceRoll)
            {
                case 0:
                    initialTrust = EthicsScale.Confide;
                    break;
                case 1:
                case 2:
                    initialTrust = EthicsScale.Embrace;
                    break;
                case 3:
                case 4:
                case 5:
                    initialTrust = EthicsScale.Cooperate;
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    initialTrust = EthicsScale.Coexist;
                    break;
                case 10:
                case 11:
                case 12:
                    initialTrust = EthicsScale.Exploit;
                    break;
                case 13:
                case 14:
                    initialTrust = EthicsScale.Beat;
                    break;
                default:
                    initialTrust = EthicsScale.Murder;
                    break;
            }

            var intialEthics = initialTrust;

            var r = new Relationship(selfId, otherId, initialTrust, intialEthics);

            return r;
        }

        protected Relationship MirrorTrustFromOther(Character self, Character other)
        {
            var initialTrust = other.GetTrustTowards(self);
            var intialEthics = initialTrust;

            var r = new Relationship(self.Id, other.Id, initialTrust, intialEthics);

            return r;
        }
    }
}
