using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics
{
    public class CharacterSpecifics
    {
        public CharacterSpecifics()
        {
            description = new List<string>();

            theAppearance = new Appearance();
            thePersonality = new Personality();
            theBackground = new Background();
            theMotivation = new Motivation();
        }

        private Appearance theAppearance;
        public Appearance TheAppearance { get { return theAppearance; } }

        private Personality thePersonality;
        public Personality ThePersonality { get { return thePersonality; } }

        private Background theBackground;
        public Background TheBackground { get { return theBackground; } }

        private Motivation theMotivation;
        public Motivation TheMotivation { get { return theMotivation; } }

        private List<string> description;
        public List<string> Description { get { return description; } }

        public void Randomize(Random rng)
        {
            description.Clear();
            theAppearance.Randomize(rng);
            thePersonality.Randomize(rng);
            theBackground.Randomize(rng);
            theMotivation.Randomize(rng);

            description.AddRange(theAppearance.Description);
            description.AddRange(thePersonality.Description);
            description.AddRange(theBackground.Description);
            description.AddRange(theMotivation.Description);
        }
    }
}