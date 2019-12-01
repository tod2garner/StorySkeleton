using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine.PlotGenerators;
using System.Windows.Input;

namespace StorySkeleton.ViewModels
{
    public class PlotGeneratorVM : ViewModel_Base
    {
        public PlotGeneratorVM()
        {
            MyBase = new PlotGenerator_FullRandom();//#TODO - allow user to select between different types of generators
            TheLibrary = new IncidentLibraryVM();
            MaxEventCount = 25;
        }

        public APlotGenerator MyBase;
        public IncidentLibraryVM TheLibrary;

        private int maxEventCount;
        public int MaxEventCount
        {
            get { return maxEventCount; }
            set { maxEventCount = value; OnPropertyChanged("MaxEventCount"); }
        }

        private PlotVM thePlotVM;
        public PlotVM ThePlotVM { get { return thePlotVM; } }


        private ICommand command_GeneratePlot;
        public ICommand Command_GeneratePlot
        {
            get
            {
                return command_GeneratePlot ?? (command_GeneratePlot = new RelayCommand(GeneratePlot, CanExecute_GeneratePlot));
            }
        }
        public bool CanExecute_GeneratePlot() { return true; }

        public void GeneratePlot()
        {
            //#TODO - allow user to setups starting cast first
            var thePlot = MyBase.GenerateNewPlot(TheLibrary.MyBase, null, MaxEventCount);
            thePlotVM = new PlotVM(thePlot);
            OnPropertyChanged("ThePlotVM");
        }
    }
}
