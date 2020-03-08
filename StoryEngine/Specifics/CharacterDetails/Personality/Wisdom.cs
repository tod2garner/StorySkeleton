using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Wisdom : ASpecific
    {
        public Wisdom() : base("Wisdom")
        {
            this.variations.Add(new Detail(10, "Deeply insightful"));
            this.variations.Add(new Detail(20, "Intuitive"));
            this.variations.Add(new Detail(40, "Average wisdom"));
            this.variations.Add(new Detail(20, "Impulsive"));
            this.variations.Add(new Detail(10, "Foolhardy"));
        }
    }
}
