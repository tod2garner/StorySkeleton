using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics
{
    public class TimeOfDay : ASpecific
    {
        public TimeOfDay() : base("Time Of Day")
        {
            this.variations.Add(new Detail(5, "Pre-Dawn"));
            this.variations.Add(new Detail(5, "Dawn"));
            this.variations.Add(new Detail(15, "Morning"));
            this.variations.Add(new Detail(20, "Mid-day"));
            this.variations.Add(new Detail(20, "Afternoon"));
            this.variations.Add(new Detail(20, "Dusk"));
            this.variations.Add(new Detail(10, "Early Night"));
            this.variations.Add(new Detail(5, "Late Night"));
        }
    }
}
