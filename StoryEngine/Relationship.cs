using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Relationship
    {
        //#TODO - move const to config file later?
        private const int SCALE_FOR_GAPS_BETWEEN_TRUST_LEVELS = 100;//Dictates how many events it takes to change trust levels

        public Relationship(int givenSelfId, int givenOtherId, EthicsScale initialTrust, EthicsScale initialEthics)
        {
            this.selfId = givenSelfId;
            this.otherId = givenOtherId;
            this.trust = initialTrust;
            this.ethics = initialEthics;
        }

        public Relationship() { }//Parameterless constructor req'd for XML serialization

        private int selfId;
        /// <summary>
        /// Id of person who's perspective this relationship is from
        /// </summary>
        public int SelfId { get { return selfId; } }

        private int otherId;
        /// <summary>
        /// Id of other person in relationship
        /// </summary>
        public int OtherId { get { return otherId; } }

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
        
        private int durabilityOfTrust;
        /// <summary>
        /// If positive, progress towards next higher trust level. If negative, progress towards next lowest level.
        /// </summary>
        public int DurabilityOfTrust { get { return durabilityOfTrust; } }

        private int durabilityOfEthics;
        /// <summary>
        /// If positive, progress towards next higher ethics level. If negative, progress towards next lowest level.
        /// </summary>
        public int DurabilityOfEthics { get { return durabilityOfEthics; } }

        public Relationship Copy()
        {
            var theCopy = new Relationship(this.SelfId, this.OtherId, this.Trust, this.Ethics);
            theCopy.durabilityOfEthics = this.durabilityOfEthics;
            theCopy.durabilityOfTrust = this.durabilityOfTrust;

            return theCopy;
        }

        /// <summary>
        /// Increases or decreases trust, and also updates ethics to match. 
        /// Both changes are offset by suspicion and morality.
        /// </summary>
        /// <param name="magnitude">Non-zero, either positive or negative, 1 = 1 bonding event.</param>
        /// <param name="characterBaseSuspicion"></param>
        /// <param name="characterMorality"></param>
        public void ChangeTrust(int magnitude, SuspicionScale characterBaseSuspicion, Morality characterMorality)
        {
            magnitude *= SCALE_FOR_GAPS_BETWEEN_TRUST_LEVELS;

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

            UpdateLevelFromDurability(ref trust, ref durabilityOfTrust);
            UpdateLevelFromDurability(ref ethics, ref durabilityOfEthics);
        }

        //#TODO - change private methods to be protected, and add Mock class to be unit tested

        private void UpdateLevelFromDurability(ref EthicsScale level, ref int durability)
        {
            bool movingUpwards = durability > 0;
            int? threshold = GapToNextLevel(level, movingUpwards);

            if (threshold == null) //already at max/min level
                return;

            if (movingUpwards && durability >= threshold)
            {
                level = HigherLevel(level);
                durability -= threshold.Value;
                UpdateLevelFromDurability(ref level, ref durability);
            }                
            else if (!movingUpwards && durability <= threshold)
            {
                level = LowerLevel(level);
                durability -= threshold.Value;
                UpdateLevelFromDurability(ref level, ref durability);
            }                
        }

        private int? GapToNextLevel(EthicsScale currentLevel, bool movingUpwards)
        {
            int next = 0;
            if (movingUpwards)
                next = (int)HigherLevel(currentLevel);
            else
                next = (int)LowerLevel(currentLevel);

            int current = (int)currentLevel;

            if (next == current) //already at max/min level
                return null;

            int gap = (next - current) * SCALE_FOR_GAPS_BETWEEN_TRUST_LEVELS;

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
        
        public string DescribeTrustDurability()
        {
            if(durabilityOfTrust == 0)
                return string.Empty;

            int? threshold = durabilityOfTrust > 0 ? GapToNextLevel(trust, true) : GapToNextLevel(trust, false);

            if (threshold == null)
                return string.Empty;

            var percentDurability = System.Math.Abs((100 * durabilityOfTrust) / threshold.Value);
            var nextLevel = durabilityOfTrust > 0 ? HigherLevel(trust) : LowerLevel(trust);

            var summary = string.Format("{0}% of the way to [{1}]", percentDurability, nextLevel.ToString());
            return summary;
        }

        public string DescribeEthicsDurability()
        {
            if (durabilityOfEthics == 0)
                return string.Empty;

            int? threshold = durabilityOfEthics > 0 ? GapToNextLevel(trust, true) : GapToNextLevel(trust, false);

            if (threshold == null)
                return string.Empty;

            var percentDurability = System.Math.Abs((100 * durabilityOfEthics) / threshold.Value);
            var nextLevel = durabilityOfEthics > 0 ? HigherLevel(trust) : LowerLevel(ethics);


            var summary = string.Format("{0}% of the way to [{1}]", percentDurability, nextLevel.ToString());
            return summary;
        }
    }
}
