using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class CurrentClass : ASpecific
    {
        public CurrentClass() : base("Current Social Standing")
        {
            this.variations.Add(new Detail(15, "Influential"));
            this.variations.Add(new Detail(20, "Wealthy"));
            this.variations.Add(new Detail(30, "Getting by"));
            this.variations.Add(new Detail(20, "Poor"));
            this.variations.Add(new Detail(15, "Outcast"));
        }
    }
}
