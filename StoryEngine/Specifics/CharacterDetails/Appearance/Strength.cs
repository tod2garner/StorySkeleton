using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Strength : ASpecific
    {
        public Strength() : base("Strength")
        {
            this.variations.Add(new Detail(5, "Mighty"));
            this.variations.Add(new Detail(25, "Strong"));
            this.variations.Add(new Detail(40, "Average strength"));
            this.variations.Add(new Detail(25, "Weak"));
            this.variations.Add(new Detail(5, "Frail"));
        }
    }
}
