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
    enum EthicsScale
    {                   //I see them as a...
        Murder = -10,        //demon
        Beat = -3,           //savage
        Exploit = -1,        //tool
        Coexist = 0,         //peer
        Cooperate = 1,       //teammate
        Embrace = 3,         //friend
        Confide = 10,        //confidant
    }

    enum SuspicionScale
    {
        Naive = 1,
        Relaxed = 2,
        Average = 3,
        Guarded = 5,
        Paranoid = 7
    }

    enum Morality
    {
        Exploit,        //hawk
        Reciprocate,    //match
        Forgive         //dove
    }
}
