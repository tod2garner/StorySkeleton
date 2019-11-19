using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    [DataContract]
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

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public Relationship() { }

        [DataMember]
        private int selfId;
        /// <summary>
        /// Id of person who's perspective this relationship is from
        /// </summary>
        public int SelfId { get { return selfId; } }

        [DataMember]
        private int otherId;
        /// <summary>
        /// Id of other person in relationship
        /// </summary>
        public int OtherId { get { return otherId; } }

        [DataMember]
        private EthicsScale trust;
        /// <summary>
        /// I expect they would be willing to ___ me (help/cheat/murder...)
        /// </summary>
        public EthicsScale Trust
        {
            get { return trust; }
            set { trust = value; }
        }

        [DataMember]
        private EthicsScale ethics;
        /// <summary>
        /// I would be willing to ___ them (confide in/help/cheat...)
        /// </summary>
        public EthicsScale Ethics
        {
            get { return ethics; }
            set { ethics = value; }
        }

        [DataMember]
        private int durabilityOfTrust;
        /// <summary>
        /// If positive, progress towards next higher trust level. If negative, progress towards next lowest level.
        /// </summary>
        public int DurabilityOfTrust { get { return durabilityOfTrust; } }

        [DataMember]
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
                level = level.HigherLevel();
                durability -= threshold.Value;
                UpdateLevelFromDurability(ref level, ref durability);
            }                
            else if (!movingUpwards && durability <= threshold)
            {
                level = level.LowerLevel();
                durability -= threshold.Value;
                UpdateLevelFromDurability(ref level, ref durability);
            }                
        }

        private int? GapToNextLevel(EthicsScale currentLevel, bool movingUpwards)
        {
            int next = 0;
            if (movingUpwards)
                next = (int)currentLevel.HigherLevel();
            else
                next = (int)currentLevel.LowerLevel();

            int current = (int)currentLevel;

            if (next == current) //already at max/min level
                return null;

            int gap = (next - current) * SCALE_FOR_GAPS_BETWEEN_TRUST_LEVELS;

            return gap;
        }
        
        public string DescribeTrustDurability()
        {
            return DescribeDurability(trust, durabilityOfTrust);
        }

        public string DescribeEthicsDurability()
        {
            return DescribeDurability(ethics, durabilityOfEthics);
        }

        protected string DescribeDurability(EthicsScale trustOrEthics, int durability)
        {
            if (durability == 0)
                return string.Empty;

            int? threshold = durability > 0 ? GapToNextLevel(trustOrEthics, true) : GapToNextLevel(trustOrEthics, false);

            if (threshold == null)
                return string.Empty;

            var percentDurability = System.Math.Abs((100 * durability) / threshold.Value);
            var nextLevel = durability > 0 ? trustOrEthics.HigherLevel() : trustOrEthics.LowerLevel();


            var summary = string.Format("{0}% of the way to [{1}]", percentDurability, nextLevel.ToString());
            return summary;
        }
    }
}
