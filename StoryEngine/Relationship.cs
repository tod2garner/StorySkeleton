using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Relationship
    {
        public Relationship(int givenOtherId, EthicsScale initialTrust, EthicsScale initialEthics)
        {
            this.otherId = givenOtherId;
            trust = initialTrust;
            ethics = initialEthics;
        }

        private int otherId;
        /// <summary>
        /// Id of other person in relationship
        /// </summary>
        public int OtherId
        {
            get { return otherId; }
        }

        private EthicsScale trust;
        /// <summary>
        /// I expect they would be willing to ___ me (help/cheat/murder...)
        /// </summary>
        public EthicsScale Trust
        {
            get { return trust; }
            set { trust = value; }
        }

        private EthicsScale ethics;
        /// <summary>
        /// I would be willing to ___ them (confide in/help/cheat...)
        /// </summary>
        public EthicsScale Ethics
        {
            get { return ethics; }
            set { ethics = value; }
        }

        /// <summary>
        /// If positive, progress towards next higher trust level. If negative, progress towards next lowest level.
        /// </summary>
        private int durabilityOfTrust;


        /// <summary>
        /// If positive, progress towards next higher ethics level. If negative, progress towards next lowest level.
        /// </summary>
        private int durabilityOfEthics;

        /// <summary>
        /// Increases or decreases trust, and also updates ethics to match. 
        /// Both changes are offset by suspicion and morality.
        /// </summary>
        /// <param name="magnitude">Non-zero, either positive or negative, 1 = 1 bonding event.</param>
        /// <param name="characterBaseSuspicion"></param>
        /// <param name="characterMorality"></param>
        public void ChangeTrust(int magnitude, SuspicionScale characterBaseSuspicion, Morality characterMorality)
        {
            magnitude *= 100; //Arbitrary, to avoid problems with integer division

            if (magnitude == 0)
                return;
            else if (magnitude > 1)
            {
                magnitude = magnitude / ((int)characterBaseSuspicion);//reduce increase in trust by suspicion 

                durabilityOfTrust += magnitude;

                if (characterMorality == Morality.Exploit)
                    magnitude = (magnitude * 3) / 4; //reduce increase in ethics by hawkish morality

                durabilityOfEthics += magnitude;
            }
            else
            {
                durabilityOfTrust += magnitude;

                if (characterMorality == Morality.Forgive)
                    magnitude = (magnitude * 3) / 4; //reduce decrease in ethics by dovish morality

                durabilityOfEthics += magnitude;
            }

            UpdateLevelFromDurability(trust, durabilityOfTrust);
            UpdateLevelFromDurability(ethics, durabilityOfEthics);
        }


        private void UpdateLevelFromDurability(EthicsScale level, int durability)
        {
            int threshold = GapToNextLevel(level, durability > 0);
            if (durability > threshold)
                trust = HigherLevel(level);
            else if (durability < threshold)
                trust = LowerLevel(level);
        }

        private int GapToNextLevel(EthicsScale currentLevel, bool movingUpwards)
        {
            int next = 0;
            if (movingUpwards)
                next = (int)HigherLevel(currentLevel);
            else
                next = (int)LowerLevel(currentLevel);

            int current = (int)currentLevel;
            int gap = (next - current) * 100;

            return gap;
        }

        private EthicsScale HigherLevel(EthicsScale current)
        {
            EthicsScale higher;
            switch (current)
            {
                case EthicsScale.Murder:
                    higher = EthicsScale.Beat;
                    break;
                case EthicsScale.Beat:
                    higher = EthicsScale.Exploit;
                    break;
                case EthicsScale.Exploit:
                    higher = EthicsScale.Coexist;
                    break;
                case EthicsScale.Coexist:
                    higher = EthicsScale.Cooperate;
                    break;
                case EthicsScale.Cooperate:
                    higher = EthicsScale.Embrace;
                    break;
                case EthicsScale.Embrace:
                    higher = EthicsScale.Confide;
                    break;
                default:
                    higher = current;
                    break;
            }

            return higher;
        }


        private EthicsScale LowerLevel(EthicsScale current)
        {
            EthicsScale lower;
            switch (current)
            {
                case EthicsScale.Murder:
                    lower = current;
                    break;
                case EthicsScale.Beat:
                    lower = EthicsScale.Murder;
                    break;
                case EthicsScale.Exploit:
                    lower = EthicsScale.Beat;
                    break;
                case EthicsScale.Coexist:
                    lower = EthicsScale.Exploit;
                    break;
                case EthicsScale.Cooperate:
                    lower = EthicsScale.Coexist;
                    break;
                case EthicsScale.Embrace:
                    lower = EthicsScale.Cooperate;
                    break;
                default:
                    lower = EthicsScale.Embrace;
                    break;
            }

            return lower;
        }

    }
}
