using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.PlotGenerators
{
    public class PlotGenerator_Default : APlotGenerator
    {
        protected override void CreateSequenceOfEvents(int maxNumIncidents, int maxNumCharacters)
        {
            var rng = new Random();

            for (int i = 0; i < maxNumIncidents; i++)
            {
                var nextIncident = this.GetNextEventRandomly(maxNumCharacters, rng);
                plotInProgress.ExecuteIncidentAndStoreAfter(nextIncident, this.currentCast, rng);
            }
        }

        protected override IIncident GetNextEventRandomly(int maxNumCharacters, Random rng)
        {
            //Which collection in library
            var chosenCollection = ChooseCollection(rng);

            //Exclude events that fail prerequisites - including check vs. maxNumCharacters

            //Choose randomly from remainder

            throw new NotImplementedException();
        }

        protected CollectionOfIncidentTemplates ChooseCollection(Random rng)
        {
            var totalPercentChanceOfCollections = this.possibleIncidents.AllCollections.Sum(t => t.Item1);

            var diceRoll = rng.Next(0, totalPercentChanceOfCollections);
            foreach (Tuple<int, CollectionOfIncidentTemplates> t in this.possibleIncidents.AllCollections)
            {
                if (diceRoll < t.Item1)
                {
                    return t.Item2;
                }

                diceRoll -= t.Item1;
            }

            return null;
        }
    }
}
