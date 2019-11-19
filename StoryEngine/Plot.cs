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
        public Plot(SocietySnapshot givenStartingCast)
        {
            startingCast = givenStartingCast;
            theIncidents = new List<IIncident>();
            theCastOverTime = new List<SocietySnapshot>();
        }

        private List<IIncident> theIncidents;
        public List<IIncident> TheIncidents { get { return theIncidents; } }

        /// <summary>
        /// Character information before Event #1
        /// </summary>
        private SocietySnapshot startingCast;

        private List<SocietySnapshot> theCastOverTime;
        /// <summary>
        /// List of character information, where the 1st snapshot is after Event #1, the 2nd after Event #2, etc.
        /// </summary>
        public List<SocietySnapshot> TheCastOverTime { get { return theCastOverTime; } }
        

        public void ExecuteIncidentAndStoreAfter(IIncident oneIncident, SocietySnapshot currentCast, Random rng = null)
        {
            oneIncident.RollDiceAndExecuteOneOutcome(currentCast, rng);
            this.TheIncidents.Add(oneIncident);
            this.TheCastOverTime.Add(currentCast.Copy());
        }

        public List<string> CompileTextNarrative()
        {
            var textNarrative = new List<string>();

            textNarrative.AddRange(this.startingCast.GetTextSummary());
            textNarrative.Add(string.Empty);

            foreach(IIncident x in this.TheIncidents)
            {
                textNarrative.AddRange(x.GetTextSummary());
                textNarrative.Add(string.Empty);
            }

            return textNarrative;
        }

    }
}
