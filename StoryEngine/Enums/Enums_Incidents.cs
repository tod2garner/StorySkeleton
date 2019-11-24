using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    /// <summary>
    /// Relative rarity of incident - within specific genre/collection (e.g. violence is not rare within action genre)
    /// </summary>
    public enum Frequency
    {
        Often = 0,
        Periodically = 1,
        Rarely = 2,
        ExtremelyRarely = 3
    }

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
        Apathy,     //resignation, bored        Grey_Light  

        Curiousity, //intrigue, surprise        Purple
        Joy,        //cheer, humor, excitement  Yellow
        Confidence, //determined, hopeful       Orange

        Shock,      //disgust, horror           Brown 
        Anger,      //resentment, disdain       Red
        Fear,       //anxiety, terror           Black
    }

    public static class IncidentEnumExtensions
    {
        public static Frequency GetRandomFrequency_Weighted(Random rng)
        {
            var diceRoll = rng.Next(0, 15); //#TODO - replace magic hard-set numbers

            if (diceRoll == 0)
                return Frequency.ExtremelyRarely;   //0
            else if (diceRoll <= 2)
                return Frequency.Rarely;            //1, 2
            else if (diceRoll <= 6)
                return Frequency.Periodically;      //3, 4, 5, 6
            else
                return Frequency.Often;             //7, 8, 9, 10, 11, 12, 13, 14
        }

        public static string ToCustomString(this Tone me)
        {
            switch(me)
            {
                case Tone.Calm:
                    return "[White] - Calm, Peace, Relief";
                case Tone.Empathy:
                    return "[Green] - Empathy, Compassion, Connection";
                case Tone.Sadness:
                    return "[Blue] - Sadness, Melancholy";
                case Tone.Shame:
                    return "[Charcoal] - Self-Doubt, Inferiority, Guilt, Shame";
                case Tone.Confusion:
                    return "[Smoke] - Confusion, Suspicion, Turmoil";
                case Tone.Apathy:
                    return "[Grey] - Boredom, Resignation, Apathy";
                case Tone.Curiousity:
                    return "[Purple] - Curiousity, Intrigue, Surprise";
                case Tone.Joy:
                    return "[Yellow] - Excitement, Humor, Joy";
                case Tone.Confidence:
                    return "[Orange] - Hope, Confidence, Determination";
                case Tone.Shock:
                    return "[Brown] - Disgust, Shock, Horror";
                case Tone.Anger:
                    return "[Red] - Resentment, Anger, Disdain";
                case Tone.Fear:
                default:
                    return "[Black] - Anxiety, Fear, Terror";
            }
        }

        public static bool IsPleasant(this Tone me)
        {
            switch (me)
            {
                case Tone.Calm:
                case Tone.Empathy:
                case Tone.Curiousity:
                case Tone.Joy:
                case Tone.Confidence:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsHighEnergy(this Tone me)
        {
            switch (me)
            {
                case Tone.Curiousity:
                case Tone.Joy:
                case Tone.Confidence:
                case Tone.Shock:
                case Tone.Anger:
                case Tone.Fear:
                    return true;
                default:
                    return false;
            }
        }

        public static List<Tone> GetPossibleTones(EnergyLevel energy, Pleasantness stress)
        {
            var possibleTones = new List<Tone>();

            if (energy != EnergyLevel.AlwaysHighEnergy)
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
