using StoryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorySkeleton.ViewModels
{
    public class CharacterVM : ViewModel_Base
    {

        public CharacterVM(Character givenBase, SocietySnapshot society)
        {
            storedSociety = society;
            MyBase = givenBase;
        }

        /// <summary>
        /// Used only to get names of other characters in relationships
        /// </summary>
        private SocietySnapshot storedSociety;

        private Character myBase;
        public Character MyBase
        {
            get { return myBase; }
            set
            {
                myBase = value;
                LoadAllNames(storedSociety);
            }
        }

        public string Name { get { return MyBase.Name; } }
        public Morality BaseMorality { get { return MyBase.BaseMorality; } }
        public SuspicionScale BaseSuspicion { get { return MyBase.BaseSuspicion; } }

        public Visibility HasAnyRelationships { get { return MyBase.AllRelations.Any() ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility HasZero_Confide { get { return names_Confide.Count == 0 ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility HasZero_Friend { get { return names_Friend.Count == 0 ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility HasZero_Cooperate { get { return names_Cooperate.Count == 0 ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility HasZero_Coexist { get { return names_Coexist.Count == 0 ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility HasZero_Exploit { get { return names_Exploit.Count == 0 ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility HasZero_Beat { get { return names_Beat.Count == 0 ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility HasZero_Murder { get { return names_Murder.Count == 0 ? Visibility.Collapsed : Visibility.Visible; } }

        private List<string> names_Confide;
        public List<string> Names_Confide { get { return names_Confide; } }
        private List<string> names_Friend;
        public List<string> Names_Friend { get { return names_Friend; } }
        private List<string> names_Cooperate;
        public List<string> Names_Cooperate { get { return names_Cooperate; } }
        private List<string> names_Coexist;
        public List<string> Names_Coexist { get { return names_Coexist; } }
        private List<string> names_Exploit;
        public List<string> Names_Exploit { get { return names_Exploit; } }
        private List<string> names_Beat;
        public List<string> Names_Beat { get { return names_Beat; } }
        private List<string> names_Murder;
        public List<string> Names_Murder { get { return names_Murder; } }

        public void LoadAllNames(SocietySnapshot society)
        {
            names_Confide = LoadNames_OfGivenTrust(society, EthicsScale.Confide);
            names_Friend = LoadNames_OfGivenTrust(society, EthicsScale.Befriend);
            names_Cooperate = LoadNames_OfGivenTrust(society, EthicsScale.Cooperate);
            names_Coexist = LoadNames_OfGivenTrust(society, EthicsScale.Coexist);
            names_Exploit = LoadNames_OfGivenTrust(society, EthicsScale.Exploit);
            names_Beat = LoadNames_OfGivenTrust(society, EthicsScale.Beat);
            names_Murder = LoadNames_OfGivenTrust(society, EthicsScale.Murder);


            OnPropertyChanged("names_Confide");
            OnPropertyChanged("names_Friend");
            OnPropertyChanged("names_Cooperate");
            OnPropertyChanged("names_Coexist");
            OnPropertyChanged("names_Exploit");
            OnPropertyChanged("names_Beat");
            OnPropertyChanged("names_Murder");
        }

        public List<string> LoadNames_OfGivenTrust(SocietySnapshot society, EthicsScale givenTrust)
        {
            var theRelations = MyBase.AllRelations.Where(r => r.Trust == givenTrust);
            var theNames = new List<string>();

            foreach (Relationship r in theRelations)
                theNames.Add(society.AllCharacters.First(c => c.Id == r.OtherId).Name);

            return theNames;
        }
    }
}
