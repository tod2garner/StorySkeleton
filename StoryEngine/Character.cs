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
        }

        private int id;
        public int Id
        {
            get { return id; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<Relationship> allRelations;
        public List<Relationship> AllRelations
        {
            get { return allRelations; }
            set { allRelations = value; }
        }

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
            var newRelation = RelationshipGenerator.CreateRelationship(this, target);
            this.AllRelations.Add(newRelation);
        }

        public bool IsAcquaintedWith(int otherCharacterId)
        {
            return AllRelations.Any(r => r.OtherId == otherCharacterId);
        }

        public void ChangeTrust(int magnitude, Character target)
        {
            if (this.IsAcquaintedWith(target.Id) == false)
                CreateRelationshipWith(target);

            var relation = this.AllRelations.First(r => r.OtherId == target.Id);
            relation.ChangeTrust(magnitude, this.BaseSuspicion, this.BaseMorality);
        }
    }
}
