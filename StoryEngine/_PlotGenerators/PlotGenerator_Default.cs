using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.PlotGenerators
{
    public class PlotGenerator_Default : APlotGenerator
    {
        protected override void CreateSequenceOfEvents(int maxNumIncidents, Random rng)
        {

            for (int i = 0; i < maxNumIncidents; i++)
            {
                var nextIncident = this.GetNextEventRandomly(rng);

                if (nextIncident == null) //Incident prerequisites not met, try again
                    continue;

                plotInProgress.ExecuteIncidentAndStoreAfter(nextIncident, this.currentCast, rng);
            }
        }

        protected IIncident GetNextEventRandomly(Random rng)
        {
            var chosenCollection = this.possibleIncidents.ChooseRandomCollection(rng);
            var chosenIncident = chosenCollection.GetRandomIncident(rng);

            var popluatedSucessfully = chosenIncident.TryToPopulateIncident(this.currentCast, rng);
            if (popluatedSucessfully == false)
                return null;  //Exclude events that fail prerequisites - including check vs. maxNumCharacters
            else
                return chosenIncident;
        }
    }
}
