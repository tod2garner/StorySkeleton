using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.PlotGenerators
{
    public abstract class APlotGenerator : IPlotGenerator
    {
        public const int MAX_INCIDENT_COUNT = 100;
        public const int MAX_CHARACTER_COUNT = 20; //#TODO - not currently used
        protected const int DEFAULT_STARTING_CHARACTER_COUNT = 5;

        //Implement concrete classes:
        //      random events, only limited to prerequisites
        //      story arch, based on emotional magnitude scores (example: follow conflict-climax-resolve pattern, or others)
        //Future:
        //      given list of events to incorporate, fill in around them (using any method above)

        protected Plot plotInProgress;
        protected SocietySnapshot currentCast;
        protected LibraryOfIncidents possibleIncidents;

        public Plot GenerateNewPlot(LibraryOfIncidents givenPossibleIncidents, SocietySnapshot startingCast = null, int? maxIncidentCount = null)
        {
            this.possibleIncidents = givenPossibleIncidents;

            currentCast = startingCast != null ? startingCast : GetStartingCast();
            int maxIncidents = maxIncidentCount != null ? maxIncidentCount.Value : MAX_INCIDENT_COUNT;

            plotInProgress = new Plot(currentCast);

            CreateSequenceOfEvents(maxIncidents);

            return plotInProgress;
        }

        protected virtual SocietySnapshot GetStartingCast()
        {
            StoryEngine.SocietyGenerators.StartingCastGenerator_Default castFactory = new SocietyGenerators.StartingCastGenerator_Default();
            return castFactory.CreateStartingCast(DEFAULT_STARTING_CHARACTER_COUNT);
        }

        protected abstract void CreateSequenceOfEvents(int maxNumIncidents);        
    }
}
