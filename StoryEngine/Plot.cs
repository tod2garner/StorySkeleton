using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    /// <summary>
    /// Contains not only sequence of events, but also character information at each point in time
    /// </summary>
    public class Plot
    {
        public const int MAX_EVENT_COUNT = 100;
        public const int MAX_CHARACTER_COUNT = 20;

        public Plot(SocietySnapshot givenStartingCast)
        {
            startingCast = givenStartingCast;
            theEvents = new List<IEvent>();
            theCastOverTime = new List<SocietySnapshot>();
        }

        private List<IEvent> theEvents;
        public List<IEvent> TheEvents { get { return theEvents; } }

        /// <summary>
        /// Character information before Event #1
        /// </summary>
        private SocietySnapshot startingCast;

        private List<SocietySnapshot> theCastOverTime;
        /// <summary>
        /// List of character information, where the 1st snapshot is after Event #1, the 2nd after Event #2, etc.
        /// </summary>
        public List<SocietySnapshot> TheCastOverTime { get { return theCastOverTime; } }

    }
}
