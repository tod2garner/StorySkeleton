using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.Location
{
    [DataContract]
    public class Location
    {
        [DataMember]
        private string description;
        public string Description { get { return description; } }

        public string Randomize(Random rng)
        {
            var summary = "Location: ";

            var category = new Location_Category().Randomize(rng);

            if (category == "Nature")
            {
                var feature = new WildFeature().Randomize(rng);
                if (feature != "None")
                    summary += feature + " in the ";

                summary += new Topography().Randomize(rng);
            }
            else
            {
                summary += new UrbanCondition().Randomize(rng) + " ";

                if (category == "Indoor")
                {
                    summary += new Building().Randomize(rng);
                }
                else
                {
                    summary += new Outdoor_Urban().Randomize(rng);
                }
            }

            description = summary;
            return description;
        }
    }

    public class Location_Category : ASpecific
    {
        public Location_Category() : base("Location Category")
        {
            this.variations.Add(new Detail(50, "Indoor"));
            this.variations.Add(new Detail(30, "Outdoor_Urban"));
            this.variations.Add(new Detail(20, "Nature"));
        }
    }

    public class Building : ASpecific
    {
        public Building() : base("Building")
        {
            this.variations.Add(new Detail(30, "Home"));
            this.variations.Add(new Detail(20, "Shop"));
            this.variations.Add(new Detail(20, "Assembly Hall"));
            this.variations.Add(new Detail(10, "Workspace"));
            this.variations.Add(new Detail(10, "Civic Center"));
        }
    }

    public class Outdoor_Urban : ASpecific
    {
        public Outdoor_Urban() : base("Urban Outdoor Area")
        {
            this.variations.Add(new Detail(40, "Street"));
            this.variations.Add(new Detail(20, "Courtyard"));
            this.variations.Add(new Detail(20, "Market"));
            this.variations.Add(new Detail(20, "Park"));
        }
    }

    public class UrbanCondition : ASpecific
    {
        public UrbanCondition() : base("Urban Condition")
        {
            this.variations.Add(new Detail(30, "Typical"));
            this.variations.Add(new Detail(10, "Run down"));
            this.variations.Add(new Detail(10, "Austere"));
            this.variations.Add(new Detail(10, "Cluttered"));
            this.variations.Add(new Detail(10, "Cozy"));
            this.variations.Add(new Detail(10, "Spacious"));
            this.variations.Add(new Detail(10, "Pristine"));
            this.variations.Add(new Detail(5, "Lavish"));
            this.variations.Add(new Detail(5, "Abandoned"));
        }
    }

    public class Topography : ASpecific
    {
        public Topography() : base("Topography")
        {
            this.variations.Add(new Detail(10, "Forest"));
            this.variations.Add(new Detail(10, "Wetlands"));
            this.variations.Add(new Detail(10, "Shoreline"));
            this.variations.Add(new Detail(10, "Barrens"));
            this.variations.Add(new Detail(10, "Hills"));
            this.variations.Add(new Detail(10, "Mountains"));
            this.variations.Add(new Detail(10, "Plains"));
        }
    }

    public class WildFeature : ASpecific
    {
        public WildFeature() : base("Wild Feature")
        {
            this.variations.Add(new Detail(30, "None"));
            this.variations.Add(new Detail(15, "Valley"));
            this.variations.Add(new Detail(15, "Outcropping"));
            this.variations.Add(new Detail(15, "Riverbed"));
            this.variations.Add(new Detail(10, "Trail"));
            this.variations.Add(new Detail(5, "Cliff"));
            this.variations.Add(new Detail(5, "Cavern"));
            this.variations.Add(new Detail(5, "Ruins"));
        }
    }


}
