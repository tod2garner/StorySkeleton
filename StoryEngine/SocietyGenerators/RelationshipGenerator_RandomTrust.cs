using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.SocietyGenerators
{
    public class RelationshipGenerator_RandomTrust : IRelationshipGenerator
    {
        public Relationship CreateRelationship(Character self, Character other, Random rng)
        {
            if (other.IsAcquaintedWith(self.Id))
                return MirrorTrustFromOther(self, other, rng);
            else
                return NewRandomTrustRelation(self, other, rng);
        }

        protected Relationship NewRandomTrustRelation(Character self, Character other, Random rng)
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
                    initialTrust = EthicsScale.Befriend;
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

            EthicsScale intialEthics = GetProbableEthics(self.BaseMorality, initialTrust, rng);

            var r = new Relationship(self.Id, other.Id, initialTrust, intialEthics);

            return r;
        }

        protected Relationship MirrorTrustFromOther(Character self, Character other, Random rng)
        {
            EthicsScale initialTrust;
            var otherTrust = other.GetTrustTowards(self);

            bool coinflip = rng.Next(0, 2) == 0;

            if (self.BaseSuspicion < other.BaseSuspicion)
                initialTrust = coinflip ? otherTrust.HigherLevel() : otherTrust;
            else if (self.BaseSuspicion > other.BaseSuspicion)
                initialTrust = coinflip ? otherTrust.LowerLevel() : otherTrust;
            else
                initialTrust = otherTrust;

            EthicsScale intialEthics = GetProbableEthics(self.BaseMorality, initialTrust, rng);

            var r = new Relationship(self.Id, other.Id, initialTrust, intialEthics);

            return r;
        }

        protected EthicsScale GetProbableEthics(Morality theMorality, EthicsScale theTrust, Random rng)
        {
            EthicsScale theEthics;
            bool coinflip = rng.Next(0, 2) == 0;

            if (theMorality == Morality.Forgive)
                theEthics = coinflip ? theTrust.HigherLevel() : theTrust;
            else if (theMorality == Morality.Exploit)
                theEthics = coinflip ? theTrust.LowerLevel() : theTrust;
            else //Reciprocate
                theEthics = theTrust;

            return theEthics;
        }
    }
}
