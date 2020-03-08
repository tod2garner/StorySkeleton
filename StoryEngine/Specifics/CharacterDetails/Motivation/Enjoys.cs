using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Enjoys : ASpecific
    {
        public Enjoys() : base("Enjoys")
        {
            this.variations.Add(new Detail(5, "Surprise-Adrenaline"));
            this.variations.Add(new Detail(10, "Novelty-Adventure"));
            this.variations.Add(new Detail(5, "Research-Discovery"));
            this.variations.Add(new Detail(5, "Mastery of skill"));
            this.variations.Add(new Detail(10, "Familiarity"));
            this.variations.Add(new Detail(5, "Challenge"));
            this.variations.Add(new Detail(10, "Prestige-Status"));
            this.variations.Add(new Detail(5, "Collecting"));
            this.variations.Add(new Detail(10, "Creating"));
            this.variations.Add(new Detail(10, "Competition"));
            this.variations.Add(new Detail(10, "Collaboration"));
            this.variations.Add(new Detail(5, "Music"));
            this.variations.Add(new Detail(5, "Art"));
            this.variations.Add(new Detail(5, "Nature"));

        }
    }
}
