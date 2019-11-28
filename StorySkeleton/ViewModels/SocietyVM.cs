using StoryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorySkeleton.ViewModels
{
    public class SocietyVM : ViewModel_Base
    {
        public SocietySnapshot MyBase;

        public SocietyVM(SocietySnapshot givenBase)
        {
            MyBase = givenBase;
            SelectedId = 0;
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
                OnPropertyChanged("SelectedCharacter");
            }
        }

        private void UpdateSelectedCharacter()
        {
            var theBaseChar = MyBase.AllCharacters.FirstOrDefault(c => c.Id == SelectedId);
            SelectedCharacter = new CharacterVM(theBaseChar, MyBase);
        }
    }
}
