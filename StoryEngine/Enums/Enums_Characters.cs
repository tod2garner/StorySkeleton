using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{   
    public enum SuspicionScale
    {
        Naive = 1,
        Relaxed = 2,
        Average = 3,
        Guarded = 5,
        Paranoid = 7
    }

    public enum Morality
    {
        Exploit = -1,        //hawk
        Reciprocate = 0,     //match
        Forgive = 1          //dove
    }
}
