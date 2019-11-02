using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.PlotGenerators
{
    public abstract class APlotGenerator : IPlotGenerator
    {
        public const int MAX_EVENT_COUNT = 100;
        public const int MAX_CHARACTER_COUNT = 20;
        protected const int DEFAULT_STARTING_CHARACTER_COUNT = 5;

        //Implement concrete classes:
        //      fully random events (ignoring prerequisites)
        //      random events, only limited to prerequisites
        //      genre random - blend of probabilities for genre (i.e. drama vs. action vs. mystery)
        //      story arch, based on emotional magnitude scores (example: follow conflict-climax-resolve pattern, or others)
        //Future:
        //      given list of events to incorporate, fill in around them (using any method above)

        private Plot plotInProgress;
        private SocietySnapshot currentCast;
        private List<IEvent> eventLibrary;

        public Plot GenerateNewPlot(List<IEvent> givenEventLibrary)
        {
            this.eventLibrary = givenEventLibrary;

            currentCast = GetStartingCast();
            plotInProgress = new Plot(currentCast);

            CreateSequenceOfEvents();

            return plotInProgress;
        }

        protected SocietySnapshot GetStartingCast()
        {
            StoryEngine.SocietyGenerators.StartingCastGenerator_Default castFactory = new SocietyGenerators.StartingCastGenerator_Default();
            return castFactory.CreateStartingCast(DEFAULT_STARTING_CHARACTER_COUNT);
        }

        protected void StoreSnapshot()
        {
            plotInProgress.TheCastOverTime.Add(currentCast.Copy());
        }

        protected abstract void CreateSequenceOfEvents();
    }
}
