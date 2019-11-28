using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorySkeleton.ViewModels
{
    public class MainWindowVM : ViewModel_Base
    {
        public MainWindowVM()
        {
            TheGenerator = new PlotGeneratorVM();
            ThePlot = TheGenerator.GeneratePlotVM();

            RibbonIndex = 1;
        }

        public ViewModel_Base VM_of_Current_View;

        //Possible main views:
        public PlotGeneratorVM TheGenerator;
        public PlotVM ThePlot;
        public SocietyVM StartingCast { get { return ThePlot.StartingCast; } }

        private int ribbonIndex;
        public int RibbonIndex
        {
            get { return ribbonIndex; }
            set
            {
                if(ribbonIndex != value)
                {
                    ribbonIndex = value;

                    switch(ribbonIndex)
                    {
                        case 0:
                            VM_of_Current_View = TheGenerator;
                            break;
                        case 1:
                            VM_of_Current_View = StartingCast;
                            break;
                        case 2:
                        default:
                            VM_of_Current_View = ThePlot;
                            break;
                    }

                    OnPropertyChanged("VM_of_Current_View");
                }
            }
        }
               
        
    }
}
