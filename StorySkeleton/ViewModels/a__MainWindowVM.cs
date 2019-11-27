using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorySkeleton.ViewModels
{
    public class MainWindowVM
    {
        //These two not yet wired up
        public PlotGeneratorVM TheGenerator;
        public PlotVM ThePlot;

        //In future remove StartingCast and re-locate into ThePlot
        public SocietyVM StartingCast;
    }
}
