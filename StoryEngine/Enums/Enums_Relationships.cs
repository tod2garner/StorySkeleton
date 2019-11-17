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
                    return "would not bother";
                case EthicsScale.Cooperate:
                    return "would reliably [Cooperate] with";
                case EthicsScale.Embrace:
                    return "would be a loyal [Friend] to";
                case EthicsScale.Confide:
                default:
                    return "would be a trusted [Confidant] for";
            }
        }
    }
}
