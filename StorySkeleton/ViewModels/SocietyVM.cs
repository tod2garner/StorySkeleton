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

        public SocietyVM() { }

        public List<Character> AllCharacters { get { return MyBase.AllCharacters; } }
        
        public int SelectedId
        {
            get { return SelectedId; }
            set
            {
                if(value != SelectedId)
                {
                    SelectedId = value;
                    UpdateSelectedCharacter();
                }
            }
        }

        public CharacterVM SelectedCharacter;

        private void UpdateSelectedCharacter()
        {
            var theBaseChar = MyBase.AllCharacters.FirstOrDefault(c => c.Id == SelectedId);
            SelectedCharacter.MyBase = theBaseChar;
        }
    }
}
