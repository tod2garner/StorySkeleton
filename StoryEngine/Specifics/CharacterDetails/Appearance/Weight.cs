using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Weight : ASpecific
    {
        public Weight() : base("Weight")
        {
            this.variations.Add(new Detail(5, "Fat"));
            this.variations.Add(new Detail(25, "Stout"));
            this.variations.Add(new Detail(40, "Average weight"));
            this.variations.Add(new Detail(25, "Slender"));
            this.variations.Add(new Detail(5, "Slight"));
        }
    }
}
