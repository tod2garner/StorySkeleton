using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class CoreDesire : ASpecific
    {
        public CoreDesire() : base("Core Desire")
        {
            this.variations.Add(new Detail(5, "Safety-Food-Water-Shelter"));
            this.variations.Add(new Detail(15, "Autonomy-Freedom-Control"));
            this.variations.Add(new Detail(20, "Respect"));
            this.variations.Add(new Detail(20, "Belonging"));
            this.variations.Add(new Detail(20, "Acceptance-Love"));
            this.variations.Add(new Detail(10, "Hope-Dream-Goal"));
            this.variations.Add(new Detail(10, "Enjoyment-Fun-Adventure"));
        }
    }
}
