using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class SpeechStyle : ASpecific
    {
        public SpeechStyle() : base("Style of Speech")
        {
            this.variations.Add(new Detail(10, "Elegant"));
            this.variations.Add(new Detail(20, "Polite"));
            this.variations.Add(new Detail(20, "Casual"));
            this.variations.Add(new Detail(20, "Crude"));
            this.variations.Add(new Detail(20, "Blunt"));
            this.variations.Add(new Detail(10, "Formal"));
        }
    }
}
