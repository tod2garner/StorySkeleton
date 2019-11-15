using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine.SocietyGenerators;

namespace StoryEngine
{
    public class Character
    {
        private static IRelationshipGenerator relationshipGenerator;
        public static IRelationshipGenerator RelationshipGenerator
        {
            get
            {
                if (relationshipGenerator == null)
                    relationshipGenerator = new RelationshipGenerator_Default();

                return relationshipGenerator;
            }
            set { relationshipGenerator = value; }
        }

        public Character(int givenId, string givenName)
        {
            this.id = givenId;
            this.name = givenName;
            allRelations = new List<Relationship>();

            this.baseSuspicion = SuspicionScale.Average;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public Character()
        {
            allRelations = new List<Relationship>();
            this.baseSuspicion = SuspicionScale.Average;
        }

        private int id;
        public int Id { get { return id; } }

        private string name;
        public string Name { get { return name; } }

        private List<Relationship> allRelations;
        public List<Relationship> AllRelations { get { return allRelations; } }

        private SuspicionScale baseSuspicion;
        /// <summary>
        /// Impacts how quick/slow a character is to gain trust in a relationship
        /// </summary>
        public SuspicionScale BaseSuspicion
        {
            get { return baseSuspicion; }
            set { baseSuspicion = value; }
        }

        private Morality baseMorality;
        /// <summary>
        /// Impacts how quick/slow a character is to change treatment of others, or ethics.
        /// (Normally in a relationship a character's ethics match their trust level.)
        /// </summary>
        public Morality BaseMorality
        {
            get { return baseMorality; }
            set { baseMorality = value; }
        }

        public Character Copy()
        {
            var theCopy = new Character(this.Id, this.Name);
            theCopy.BaseMorality = this.BaseMorality;
            theCopy.BaseSuspicion = this.BaseSuspicion;

            foreach (Relationship r in this.AllRelations)
                theCopy.AllRelations.Add(r.Copy());

            return theCopy;
        }

        public void CreateRelationshipWith(Character target)
        {
            if (target.Id == this.Id)
                throw new ArgumentException("Cannot create relationship with self");

            if (this.AllRelations.Any(r => r.OtherId == target.Id))
                throw new ArgumentException("Relationship with target already exists");

            var newRelation = RelationshipGenerator.CreateRelationship(this, target);
            this.AllRelations.Add(newRelation);
        }

        public string ChangeTrust(int magnitude, Character target)
        {
            if (magnitude == 0 || this.Id == target.Id)
                return string.Empty;

            string textSummary = "";

            if (this.IsAcquaintedWith(target.Id) == false)
            {
                CreateRelationshipWith(target);
                textSummary += string.Format("{0} meets {1} for the first time. ", this.Name, target.Name);
            }

            var relation = this.AllRelations.First(r => r.OtherId == target.Id);
            relation.ChangeTrust(magnitude, this.BaseSuspicion, this.BaseMorality);

            textSummary += this.DescribeTrustTowards(target, true);

            return textSummary;
        }

        public bool IsAcquaintedWith(int otherCharacterId)
        {
            return AllRelations.Any(r => r.OtherId == otherCharacterId);
        }

        public bool IsTrustLevelMutual(Character other)
        {
            var myTrustTowardsThem = this.GetTrustTowardsId(other.Id);
            var theirTrustTowardsMe = other.GetTrustTowardsId(this.Id);

            return myTrustTowardsThem == theirTrustTowardsMe;
        }

        public bool IsEthicsLevelMutual(Character other)
        {
            var myEthicsTowardsThem = this.GetEthicsTowardsId(other.Id);
            var theirEthicsTowardsMe = other.GetEthicsTowardsId(this.Id);

            return myEthicsTowardsThem == theirEthicsTowardsMe;
        }

        public EthicsScale? GetTrustTowardsId(int otherCharacterId)
        {
            if (this.IsAcquaintedWith(otherCharacterId) == false)
                return null;
            else
            {
                var relation = this.AllRelations.First(r => r.OtherId == otherCharacterId);
                return relation.Trust;
            }
        }

        public EthicsScale GetTrustTowards(Character other)
        {
            var theTrust = GetTrustTowardsId(other.Id);

            if (theTrust.HasValue)
                return theTrust.Value;
            else
            {
                //strangers, check potential relation if they met
                var futureRelation = Character.RelationshipGenerator.CreateRelationship(this, other);
                theTrust = futureRelation.Trust;
            }

            return theTrust.Value;
        }

        public EthicsScale? GetEthicsTowardsId(int otherCharacterId)
        {
            if (this.IsAcquaintedWith(otherCharacterId) == false)
                return null;
            else
            {
                var relation = this.AllRelations.First(r => r.OtherId == otherCharacterId);
                return relation.Ethics;
            }
        }

        public EthicsScale GetEthicsTowards(Character other)
        {
            var theEthics = GetEthicsTowardsId(other.Id);

            if (theEthics.HasValue)
                return theEthics.Value;
            else
            {
                //strangers, check potential relation if they met
                var futureRelation = Character.RelationshipGenerator.CreateRelationship(this, other);
                theEthics = futureRelation.Ethics;
            }

            return theEthics.Value;
        }
        
        public string DescribeTrustTowards(Character other, bool IncludeDurability = false)
        {
            var theTrust = GetTrustTowardsId(other.Id);
            if (false == theTrust.HasValue)
                return string.Format("{0} has never met {1}");

            var summary = string.Format("{0} expects {1} {2} him/her", this.Name, other.Name, theTrust.Value.ToCustomString());

            if(IncludeDurability)
            {
                var relation = this.AllRelations.First(r => r.OtherId == other.Id);
                summary += " - " + relation.DescribeTrustDurability();
            }

            return summary;
        }

        public string DescribeEthicsTowards(Character other, bool IncludeDurability = false)
        {
            var theEthics = GetEthicsTowardsId(other.Id);
            if (false == theEthics.HasValue)
                return string.Format("{0} has never met {1}");

            var summary = string.Format("{0} {1} {2}", this.Name, theEthics.Value.ToCustomString(), other.Name);

            if (IncludeDurability)
            {
                var relation = this.AllRelations.First(r => r.OtherId == other.Id);
                summary += " - " + relation.DescribeEthicsDurability();
            }

            return summary;
        }
    }
}
