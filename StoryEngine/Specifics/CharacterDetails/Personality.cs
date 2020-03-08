using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine.Specifics.CharacterDetails;

namespace StoryEngine.Specifics
{
    public class Personality
    {
        public Personality()
        {
            description = new List<string>();

            theCharisma = new Charisma();
            theIntelligence = new Intelligence();
            theSocialOpenness = new SocialOpenness();
            theSpeechStyle = new SpeechStyle();
            theWisdom = new Wisdom();
        }

        private Charisma theCharisma;
        private Intelligence theIntelligence;
        private SocialOpenness theSocialOpenness;
        private SpeechStyle theSpeechStyle;
        private Wisdom theWisdom;

        private List<string> description;
        public List<string> Description { get { return description; } }

        public void Randomize(Random rng)
        {
            description.Clear();
            description.Add(theCharisma.Randomize(rng));
            description.Add(theIntelligence.Randomize(rng));
            description.Add(theWisdom.Randomize(rng));
            description.Add(theSocialOpenness.Randomize(rng));
            description.Add(theSpeechStyle.Randomize(rng) + " speech");
        }
    }
}