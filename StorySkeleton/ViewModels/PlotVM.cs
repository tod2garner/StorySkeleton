using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorySkeleton.ViewModels
{
    public class PlotVM : ViewModel_Base
    {
        public PlotVM()
        {
            StartingCast = new SocietyVM();
        }

        public SocietyVM StartingCast;

        public StoryEngine.Plot MyBase
        {
            get { return MyBase; }
            set
            {
                MyBase = value;
                UpdateStartingCast();
            }
        }

        private void UpdateStartingCast()
        {
            StartingCast.MyBase = MyBase.StartingCast;
        }


    }
}
