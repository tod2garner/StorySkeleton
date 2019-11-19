using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    /// <summary>
    /// I would be willing to ___ that person/group
    /// </summary>
    public enum EthicsScale
    {                   //I see them as a...
        Murder = -10,        //demon
        Beat = -3,           //savage
        Exploit = -1,        //tool
        Coexist = 0,         //peer
        Cooperate = 1,       //teammate
        Embrace = 3,         //friend
        Confide = 10,        //confidant
    }
    
    public static class EnumExtensions
    {
        public static string ToCustomString(this EthicsScale me)
        {
            switch (me)
            {
                case EthicsScale.Murder:
                    return "would not hesitate to [Murder]";
                case EthicsScale.Beat:
                    return "would not hesitate to [Beat]";
                case EthicsScale.Exploit:
                    return "would not hesitate to [Exploit]";
                case EthicsScale.Coexist:
                    return "would reasonably [Coexist] with";
                case EthicsScale.Cooperate:
                    return "would reliably [Cooperate] with";
                case EthicsScale.Embrace:
                    return "would be a loyal [Friend] to";
                case EthicsScale.Confide:
                default:
                    return "would be a trusted [Confidant] for";
            }
        }

        public static EthicsScale HigherLevel(this EthicsScale me)
        {
            EthicsScale higher;
            switch (me)
            {
                case EthicsScale.Murder:
                    higher = EthicsScale.Beat;
                    break;
                case EthicsScale.Beat:
                    higher = EthicsScale.Exploit;
                    break;
                case EthicsScale.Exploit:
                    higher = EthicsScale.Coexist;
                    break;
                case EthicsScale.Coexist:
                    higher = EthicsScale.Cooperate;
                    break;
                case EthicsScale.Cooperate:
                    higher = EthicsScale.Embrace;
                    break;
                case EthicsScale.Embrace:
                    higher = EthicsScale.Confide;
                    break;
                default:
                    higher = me;
                    break;
            }

            return higher;
        }

        public static EthicsScale LowerLevel(this EthicsScale me)
        {
            EthicsScale lower;
            switch (me)
            {
                case EthicsScale.Murder:
                    lower = me;
                    break;
                case EthicsScale.Beat:
                    lower = EthicsScale.Murder;
                    break;
                case EthicsScale.Exploit:
                    lower = EthicsScale.Beat;
                    break;
                case EthicsScale.Coexist:
                    lower = EthicsScale.Exploit;
                    break;
                case EthicsScale.Cooperate:
                    lower = EthicsScale.Coexist;
                    break;
                case EthicsScale.Embrace:
                    lower = EthicsScale.Cooperate;
                    break;
                default:
                    lower = EthicsScale.Embrace;
                    break;
            }

            return lower;
        }
    }
}
