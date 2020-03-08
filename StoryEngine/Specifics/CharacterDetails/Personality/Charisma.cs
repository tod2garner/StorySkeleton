using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Charisma : ASpecific
    {
        public Charisma() : base("Charisma")
        {
            this.variations.Add(new Detail(10, "Widely admired"));
            this.variations.Add(new Detail(20, "Likeable"));
            this.variations.Add(new Detail(40, "Average charisma"));
            this.variations.Add(new Detail(20, "Awkward"));
            this.variations.Add(new Detail(10, "Abrasive"));
        }
    }
}
