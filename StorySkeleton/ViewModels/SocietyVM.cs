using StoryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StorySkeleton.ViewModels
{
    public class SocietyVM : ViewModel_Base
    {
        public SocietySnapshot MyBase;

        public SocietyVM(SocietySnapshot givenBase)
        {
            MyBase = givenBase;
            UpdateSelectedCharacter();
        }

        public List<Character> AllCharacters { get { return MyBase?.AllCharacters; } }

        private int selectedId;
        public int SelectedId
        {
            get { return selectedId; }
            set
            {
                if(value != selectedId)
                {
                    selectedId = value;
                    UpdateSelectedCharacter();
                }
            }
        }

        private CharacterVM selectedCharacter;
        public CharacterVM SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }

        private void UpdateSelectedCharacter()
        {
            var theBaseChar = MyBase.AllCharacters.FirstOrDefault(c => c.Id == SelectedId);

            if(SelectedCharacter != null)
                SelectedCharacter.PropertyChanged -= CharacterVM_PropertyChanged;

            SelectedCharacter = new CharacterVM(theBaseChar, MyBase);
            SelectedCharacter.PropertyChanged += CharacterVM_PropertyChanged;
        }
               
        void CharacterVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //#TODO - FIX - for now, force to change characters and back to trigger update
            var realID = selectedId;
            var tempID = (selectedId == 0) ? 1 : 0;
            SelectedId = tempID;            
            SelectedId = realID;
        }
    }
}
