using StoryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorySkeleton.ViewModels
{
    public class IncidentVM : ViewModel_Base
    {
        //Have actual list of Roles (with names of particpants to display)
        //Instead of VM for each Prereq and Outcome for now, just have strings


        public IncidentVM(Incident givenBase)
        {
            MyBase = givenBase;
        }

        public Incident MyBase;

        public string Name { get { return MyBase.Name; } }

        public List<Role> AllParticipantRoles { get { return MyBase.AllParticipantRoles; } }

        public List<string> OutcomeText { get { return MyBase.OutcomeTextSummary; } }

        public Visibility HasAnyOutcomeText { get { return MyBase.OutcomeTextSummary.Any() ? Visibility.Visible : Visibility.Collapsed; } }
        
        public string DescribeTone { get { return MyBase.TheTone.ToCustomString(); } }

        public System.Windows.Media.Brush ColorOfTone
        {
            get
            {
                switch(MyBase.TheTone)
                {
                    case Tone.Calm:
                        return System.Windows.Media.Brushes.White;
                    case Tone.Empathy:
                        return System.Windows.Media.Brushes.Green;
                    case Tone.Sadness:
                        return System.Windows.Media.Brushes.RoyalBlue;
                    case Tone.Shame:
                        return System.Windows.Media.Brushes.DarkSlateGray;
                    case Tone.Confusion:
                        return System.Windows.Media.Brushes.Gray;
                    case Tone.Apathy:
                        return System.Windows.Media.Brushes.LightGray;
                    case Tone.Curiousity:
                        return System.Windows.Media.Brushes.Purple;
                    case Tone.Joy:
                        return System.Windows.Media.Brushes.Yellow;
                    case Tone.Confidence:
                        return System.Windows.Media.Brushes.Orange;
                    case Tone.Shock:
                        return System.Windows.Media.Brushes.Brown;
                    case Tone.Anger:
                        return System.Windows.Media.Brushes.Red;
                    case Tone.Fear:
                    default:
                        return System.Windows.Media.Brushes.Black;
                }
            }
        }

    }
}
