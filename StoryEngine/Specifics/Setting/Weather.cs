using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.Weather
{
    [DataContract]
    public class Weather
    {
        [DataMember]
        private string description;
        public string Description { get { return description; } }

        public string Randomize(Random rng)
        {
            var summary = "Weather: ";

            var t = new Temperature().Randomize(rng);
            var p = new Precipitation().Randomize(rng);
            var w = new Wind().Randomize(rng);

            summary += t;

            if(p != "None")
            {
                summary += ", " + p;
                if (t != "Freezing")
                    summary += " rain";
                else
                    summary += " snow/ice";
            }

            summary += ", " + w;

            description = summary;
            return description;
        }
    }

    public class Temperature : ASpecific
    {
        public Temperature() : base("Temperature")
        {
            this.variations.Add(new Detail(10, "Freezing"));
            this.variations.Add(new Detail(20, "Cold"));
            this.variations.Add(new Detail(40, "Temperate"));
            this.variations.Add(new Detail(20, "Warm"));
            this.variations.Add(new Detail(10, "Hot"));
        }
    }

    public class Precipitation : ASpecific
    {
        public Precipitation() : base("Precipitation")
        {
            this.variations.Add(new Detail(60, "None"));
            this.variations.Add(new Detail(10, "Sporadic"));
            this.variations.Add(new Detail(10, "Light"));
            this.variations.Add(new Detail(10, "Steady"));
            this.variations.Add(new Detail(10, "Heavy"));
        }
    }

    public class Wind : ASpecific
    {
        public Wind() : base("Wind")
        {
            this.variations.Add(new Detail(40, "Still"));
            this.variations.Add(new Detail(30, "Light wind"));
            this.variations.Add(new Detail(20, "Windy"));
            this.variations.Add(new Detail(10, "Strong winds"));
        }
    }
}
