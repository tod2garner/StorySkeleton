using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Grooming : ASpecific
    {
        public Grooming() : base("Grooming")
        {
            this.variations.Add(new Detail(10, "Fashionably dressed"));
            this.variations.Add(new Detail(15, "Sharply dressed"));
            this.variations.Add(new Detail(15, "Casually dressed"));
            this.variations.Add(new Detail(10, "Eccentric clothing"));
            this.variations.Add(new Detail(20, "Simple clothing"));
            this.variations.Add(new Detail(10, "Ragged clothing"));
            this.variations.Add(new Detail(10, "Sloppy appearance"));
            this.variations.Add(new Detail(10, "Filthy appearance"));
        }
    }
}
