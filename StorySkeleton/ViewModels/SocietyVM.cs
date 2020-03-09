using StoryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Win32;

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
                if (value != selectedId)
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

            if (SelectedCharacter != null)
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

        public void SaveToFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Character file (*.cast)|*.cast";
            saveFileDialog.FileName = "Society1.cast";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
                MyBase.SaveToFile(saveFileDialog.FileName);
        }

        private ICommand command_SaveToFile;
        public ICommand Command_SaveToFile { get { return command_SaveToFile ?? (command_SaveToFile = new RelayCommand(SaveToFile, CanExecute_SaveToFile)); } }

        public bool CanExecute_SaveToFile() { return true; }
    }
}
