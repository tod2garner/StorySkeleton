using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Beauty : ASpecific
    {
        public Beauty() : base("Beauty")
        {
            this.variations.Add(new Detail(5, "Ugly"));
            this.variations.Add(new Detail(30, "Unattractive"));
            this.variations.Add(new Detail(30, "Plain"));
            this.variations.Add(new Detail(30, "Attractive"));
            this.variations.Add(new Detail(5, "Rare beauty"));
        }
    }
}
