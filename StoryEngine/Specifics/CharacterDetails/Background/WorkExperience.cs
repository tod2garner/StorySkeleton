using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics.CharacterDetails
{
    public class WorkExperience : ASpecific
    {
        public WorkExperience() : base("Experience in Industry")
        {
            this.variations.Add(new Detail(5, "Agriculture"));
            this.variations.Add(new Detail(2, "Military"));
            this.variations.Add(new Detail(2, "Guard"));
            this.variations.Add(new Detail(2, "Merchant"));
            this.variations.Add(new Detail(1, "Criminal-Thief"));
            this.variations.Add(new Detail(1, "Government-Politics"));
            this.variations.Add(new Detail(1, "Transport-Shipping"));
            this.variations.Add(new Detail(1, "Sailor"));
            this.variations.Add(new Detail(1, "Scribe"));
            this.variations.Add(new Detail(1, "Construction"));
            this.variations.Add(new Detail(1, "Tailor"));
            this.variations.Add(new Detail(1, "Weaver"));
            this.variations.Add(new Detail(1, "Carpentry"));
            this.variations.Add(new Detail(1, "Mining"));
            this.variations.Add(new Detail(1, "Smithing"));
            this.variations.Add(new Detail(1, "Chandler-Candlemaker"));
            this.variations.Add(new Detail(1, "Animals-Husbandry"));
            this.variations.Add(new Detail(1, "Pottery"));
            this.variations.Add(new Detail(1, "Winery"));
            this.variations.Add(new Detail(1, "Brewer"));
            this.variations.Add(new Detail(1, "Tavern"));
            this.variations.Add(new Detail(1, "Medicine-Herbalist"));
            this.variations.Add(new Detail(1, "Messenger"));
            this.variations.Add(new Detail(1, "Research"));
            this.variations.Add(new Detail(1, "Cartographer"));
            this.variations.Add(new Detail(1, "Cartwright"));
            this.variations.Add(new Detail(1, "Miller"));
            this.variations.Add(new Detail(1, "Cobler"));
            this.variations.Add(new Detail(1, "Cooper"));
            this.variations.Add(new Detail(1, "Baker"));
            this.variations.Add(new Detail(1, "Butcher"));
            this.variations.Add(new Detail(1, "Fletcher"));
            this.variations.Add(new Detail(1, "Fisher"));
            this.variations.Add(new Detail(1, "Tanner"));
            this.variations.Add(new Detail(1, "Masonry"));
            this.variations.Add(new Detail(1, "Lumber"));
            this.variations.Add(new Detail(1, "Glassblower"));
            this.variations.Add(new Detail(1, "Lawyer-Negotiator"));
            this.variations.Add(new Detail(1, "Lender-Broker"));
            this.variations.Add(new Detail(1, "Laundry"));
            this.variations.Add(new Detail(1, "Entertainer"));
            this.variations.Add(new Detail(1, "Artist"));
            this.variations.Add(new Detail(1, "Religion"));
        }
    }
}
