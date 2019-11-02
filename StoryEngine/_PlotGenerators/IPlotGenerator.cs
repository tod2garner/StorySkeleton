using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.PlotGenerators
{
    public interface IPlotGenerator
    {
        //Implement:
        //      fully random events (ignoring prerequisites)
        //      random events, only limited to prerequisites
        //      genre random - blend of probabilities for genre (i.e. drama vs. action vs. mystery)
        //      story arch, based on emotional magnitude scores (example: follow conflict-climax-resolve pattern, or others)
        //Future:
        //      given list of events to incorporate, fill in around them (using any method above)

        Plot GenerateNewPlot();
    }
}
