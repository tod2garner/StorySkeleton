using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public enum EnergyLevel
    {
        EitherLowOrHigh = 0,    //Example - social gathering, travel
        AlwaysHighEnergy = 1,   //Example - attack, betrayal
        AlwaysLowEnergy = -1    //Example - rest
    }

    public enum Pleasantness
    {
        EitherPleasantOrNot = 0,    //Example - social gathering, travel
        AlwaysPleasant = 1,         //Example - windfall
        NeverPleasant = -1          //Example - attack, betrayal
    }

    public enum Tone
    {
        Calm,       //peace, relief             White
        Empathy,    //compassion, connection    Green

        Sadness,    //meloncholy                Blue
        Shame,      //inferior, guilty          Blue_Dark   
        Confusion,  //turmoil, suspicion        Grey_Dark
        Apathy,     //listless, bored           Grey_Light  

        Curiousity, //intrigue                  Purple
        Excitement, //surprise, anticipation    Yellow_Pale
        Joy,        //cheer, humor              Yellow_Bright
        Confidence, //determined, hopeful       Orange

        Shock,      //disgust, horror           Brown 
        Anger,      //resentment, disdain       Red
        Fear,       //anxiety, terror           Black
    }
    public static class IncidentEnumExtensions
    {
        public static List<Tone> GetPossibleTones(EnergyLevel energy, Pleasantness stress)
        {
            var possibleTones = new List<Tone>();

            if(energy != EnergyLevel.AlwaysHighEnergy)
            {
                if (stress != Pleasantness.NeverPleasant)
                {
                    possibleTones.Add(Tone.Calm);
                    possibleTones.Add(Tone.Empathy);
                }

                if (stress != Pleasantness.AlwaysPleasant)
                {
                    possibleTones.Add(Tone.Sadness);
                    possibleTones.Add(Tone.Shame);
                    possibleTones.Add(Tone.Confusion);
                    possibleTones.Add(Tone.Apathy);
                }
            }

            if (energy != EnergyLevel.AlwaysLowEnergy)
            {
                if (stress != Pleasantness.NeverPleasant)
                {
                    possibleTones.Add(Tone.Curiousity);
                    possibleTones.Add(Tone.Excitement);
                    possibleTones.Add(Tone.Joy);
                    possibleTones.Add(Tone.Confidence);
                }

                if (stress != Pleasantness.AlwaysPleasant)
                {
                    possibleTones.Add(Tone.Shock);
                    possibleTones.Add(Tone.Anger);
                    possibleTones.Add(Tone.Fear);
                }
            }

            return possibleTones;
        }
    }
}
