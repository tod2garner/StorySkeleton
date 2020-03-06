using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace StorySkeleton.ViewModels
{
    public class PlotVM : ViewModel_Base
    {
        public PlotVM(Plot givenBase)
        {
            myBase = givenBase;
            StartingCast = new SocietyVM(givenBase.StartingCast);
            allIncidents = new ObservableCollection<IncidentVM>();
            AllIncidents_Update();
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

        private ObservableCollection<IncidentVM> allIncidents;
        public ObservableCollection<IncidentVM> AllIncidents
        {
            get
            {
                return allIncidents;
            }
        }

        public void AllIncidents_Update()
        {
            allIncidents.Clear();
            var theIncidentsAsVM = myBase.TheIncidents.Select(i => new IncidentVM(i as Incident));

            foreach (var iVM in theIncidentsAsVM)
            {
                iVM.PropertyChanged += IncidentVM_PropertyChanged;
                allIncidents.Add(iVM);
            }

            OnPropertyChanged("AllIncidents");
        }


        void AllIncidents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (IncidentVM item in e.NewItems)
                    item.PropertyChanged += IncidentVM_PropertyChanged;

            if (e.OldItems != null)
                foreach (IncidentVM item in e.OldItems)
                    item.PropertyChanged -= IncidentVM_PropertyChanged;
        }

        void IncidentVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SettingText")
                AllIncidents_Update();//#TODO - fix later to not reset entire collection, only update one item
        }

        public int Count_Characters { get{ return StartingCast.AllCharacters.Count; } }
        public int Count_Incidents { get{ return AllIncidents.Count; } }
    }
}
