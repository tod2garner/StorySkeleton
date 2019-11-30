using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Specifics
{
    public class Setting
    {
        public Setting()
        {
            description = new List<string>();

            theTime = new TimeOfDay();
            theLocation = new Location.Location();
            theWeather = new Weather.Weather();
        }

        private TimeOfDay theTime;
        private Location.Location theLocation;
        private Weather.Weather theWeather;

        private List<string> description;
        public List<string> Description { get { return description; } }

        public void Randomize(Random rng)
        {
            description.Add("Time of day: " + theTime.Randomize(rng));
            description.Add(theLocation.Randomize(rng));
            description.Add(theWeather.Randomize(rng));
        }
    }
}
