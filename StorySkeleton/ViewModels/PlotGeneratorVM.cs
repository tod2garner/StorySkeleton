using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine.PlotGenerators;

namespace StorySkeleton.ViewModels
{
    public class PlotGeneratorVM : ViewModel_Base
    {
        public APlotGenerator MyBase;

        public IncidentLibraryVM TheLibrary;

        public int MaxEventCount { get; set; }

        public PlotGeneratorVM()
        {
            MyBase = new PlotGenerator_Default();//#TODO - allow user to select between different types of generators
            TheLibrary = new IncidentLibraryVM();
        }

        public PlotVM GeneratePlotVM()
        {
            //#TODO - allow user to setups starting cast first
            var thePlot = MyBase.GenerateNewPlot(TheLibrary.MyBase, null, MaxEventCount);
            var plotVM = new PlotVM();
            plotVM.MyBase = thePlot;

            return plotVM;
        }
    }
}
