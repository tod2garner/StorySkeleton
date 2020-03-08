using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Height : ASpecific
    {
        public Height() : base("Height")
        {
            this.variations.Add(new Detail(5, "Giant"));
            this.variations.Add(new Detail(25, "Tall"));
            this.variations.Add(new Detail(40, "Average height"));
            this.variations.Add(new Detail(25, "Short"));
            this.variations.Add(new Detail(5, "Tiny"));
        }
    }
}
