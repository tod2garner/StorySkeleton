using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine;

namespace StorySkeleton.ViewModels
{
    public class PlotVM : ViewModel_Base
    {
        public PlotVM(Plot givenBase)
        {
            myBase = givenBase;
            StartingCast = new SocietyVM(givenBase.StartingCast);
        }

        public SocietyVM StartingCast;

        private Plot myBase;
        public Plot MyBase
        {
            get { return myBase; }
            set
            {
                myBase = value;
                UpdateStartingCast();
            }
        }

        private void UpdateStartingCast()
        {
            StartingCast.MyBase = MyBase.StartingCast;
            OnPropertyChanged("StartingCast");
        }

        public List<IIncident> AllIncidents { get { return myBase.TheIncidents; } }

    }
}
