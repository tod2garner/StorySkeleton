using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.SocietyGenerators
{
    public class StartingCastGenerator_Default : IStartingCastGenerator
    {
        private const int MAX_RELATIONSHIP_COUNT = 10;
        private const int PERCENT_CHANCE_FOR_ONEWAY_RELATIONSHIP = 5;

        public SocietySnapshot CreateStartingCast(int characterCount)
        {
            if (characterCount < 1)
                throw new ArgumentOutOfRangeException();

            var s = new SocietySnapshot();
            
            Random rng = new Random();

            RandomNameSelector nameFactory = new RandomNameSelector();
            List<string> allNames = nameFactory.SelectRandomNamesFromDefaultNameList(characterCount, rng);

            RandomCharacterGenerator_Default characterFactory = new RandomCharacterGenerator_Default();
            for (int i = 0; i < characterCount; i++)
                s.AllCharacters.Add(characterFactory.CreateCharacter(i, allNames[i], rng));

            if (characterCount > 1)
                CreateStartingRelationships(s, rng);

            return s;
        }

        protected void CreateStartingRelationships(SocietySnapshot s, Random rng = null)
        {
            Character.RelationshipGenerator = new RelationshipGenerator_Default();

            if(rng == null)
                rng = new Random();

            //Each character given min of 1 relationship, diminishing odds for each additional relationship
            foreach (Character c in s.AllCharacters)
            {
                if (c.AllRelations.Count >= MAX_RELATIONSHIP_COUNT)
                    continue;

                int diminishingChance = 100 / MAX_RELATIONSHIP_COUNT;

                for (int percentChance = 100 - (diminishingChance * c.AllRelations.Count); percentChance > 0; percentChance -= diminishingChance)
                {
                    var unrelated = s.AllCharacters.Where(o => (c.IsAcquaintedWith(o.Id) == false) && o.Id != c.Id).ToList();
                    if (unrelated.Any() == false)
                        break;

                    if (rng.Next(0, 100) < percentChance)
                    {
                        var other = unrelated[rng.Next(0, unrelated.Count)];
                        c.CreateRelationshipWith(other);

                        if (other.IsAcquaintedWith(c.Id) == false)
                        {
                            //Most relationships are two way 
                            //  rare for one person to trust another when other doesn't even know them
                            if (rng.Next(0, 100) < PERCENT_CHANCE_FOR_ONEWAY_RELATIONSHIP)
                                other.CreateRelationshipWith(c);
                        }
                    }
                }
            }
        }
    }
}
