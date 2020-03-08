using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class Age : ASpecific
    {
        public Age() : base("Age")
        {
            this.variations.Add(new Detail(5, "Child"));
            this.variations.Add(new Detail(10, "Teenager"));
            this.variations.Add(new Detail(20, "20s"));
            this.variations.Add(new Detail(30, "30s"));
            this.variations.Add(new Detail(15, "40s"));
            this.variations.Add(new Detail(10, "50s"));
            this.variations.Add(new Detail(5, "60s"));
            this.variations.Add(new Detail(5, "Elderly"));
        }
    }
}
