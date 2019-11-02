using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.PlotGenerators
{
    public abstract class APlotGenerator : IPlotGenerator
    {
        protected const int DEFAULT_STARTING_CHARACTER_COUNT = 5;

        public Plot GenerateNewPlot()
        {
            Plot p = new Plot(GetStartingCast());

            LoadLibraryOfPossibleEvents();
            CreateSequenceOfEvents();

            return p;
        }
        

        protected SocietySnapshot GetStartingCast()
        {
            StoryEngine.SocietyGenerators.StartingCastGenerator_Default castFactory = new SocietyGenerators.StartingCastGenerator_Default();
            return castFactory.CreateStartingCast(DEFAULT_STARTING_CHARACTER_COUNT);
        }

        protected void LoadLibraryOfPossibleEvents()
        {
            throw new NotImplementedException();
        }

        protected void StoreSnapshotAfterEvent()
        {
            throw new NotImplementedException();
        }

        protected abstract void CreateSequenceOfEvents();
    }
}
