using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.PlotGenerators
{
    public interface IPlotGenerator
    {
        Plot GenerateNewPlot(LibraryOfIncidents givenPossibleIncidents, SocietySnapshot startingCast = null, int? maxIncidentCount = null);
    }
}
