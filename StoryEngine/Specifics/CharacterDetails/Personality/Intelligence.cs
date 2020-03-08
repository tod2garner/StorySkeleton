using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Intelligence : ASpecific
    {
        public Intelligence() : base("Intelligence")
        {
            this.variations.Add(new Detail(10, "Brilliant"));
            this.variations.Add(new Detail(20, "Clever"));
            this.variations.Add(new Detail(40, "Average intelligence"));
            this.variations.Add(new Detail(20, "Slow-witted"));
            this.variations.Add(new Detail(10, "Dull minded"));
        }
    }
}
