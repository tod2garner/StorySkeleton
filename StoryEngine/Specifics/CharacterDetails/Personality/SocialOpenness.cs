using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class SocialOpenness : ASpecific
    {
        public SocialOpenness() : base("Social Openness")
        {
            this.variations.Add(new Detail(10, "Heart-on-sleeve"));
            this.variations.Add(new Detail(20, "Talkative"));
            this.variations.Add(new Detail(40, "Responsive"));
            this.variations.Add(new Detail(20, "Quiet"));
            this.variations.Add(new Detail(10, "Stoic"));
        }
    }
}
