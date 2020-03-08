using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class BornClass : ASpecific
    {
        public BornClass() : base("Social class born into")
        {
            this.variations.Add(new Detail(10, "Noble"));
            this.variations.Add(new Detail(20, "Wealthy"));
            this.variations.Add(new Detail(40, "Middle-class"));
            this.variations.Add(new Detail(25, "Poor"));
            this.variations.Add(new Detail(5, "Outcast"));
        }
    }
}
