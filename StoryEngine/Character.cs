using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Character
    {
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

    }
}
