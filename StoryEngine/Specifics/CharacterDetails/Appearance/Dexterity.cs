using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Dexterity : ASpecific
    {
        public Dexterity() : base("Dexterity")
        {
            this.variations.Add(new Detail(5, "Rapid"));
            this.variations.Add(new Detail(25, "Fast"));
            this.variations.Add(new Detail(40, "Average dexterity"));
            this.variations.Add(new Detail(25, "Slow"));
            this.variations.Add(new Detail(5, "Clumsy"));
        }
    }
}
