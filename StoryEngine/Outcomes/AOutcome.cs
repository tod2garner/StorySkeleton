using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class AOutcome : IOutcome
    {
        //#TODO
        //  For list of participants, partial inclusion for one or both sides
        // Future: personalilty change outcomes

        public abstract List<string> Execute();
    }
}
