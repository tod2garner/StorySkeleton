using StoryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorySkeleton.ViewModels
{
    public class SocietyVM
    {
        public SocietySnapshot MyBase;

        public SocietyVM(SocietySnapshot givenBase)
        {
            MyBase = givenBase;
            SelectedCharacter = new CharacterVM(null, MyBase);
            SelectedId = 0;
        }

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
