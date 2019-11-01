using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class AOutcome : IOutcome
    {
        //Concrete classes to create:
        // MutualChangeInTrust (increase/decrease)
        // DirectionalChangeInTrust (one side changes, other does not)     
        // DivergentChangeInTrust (one side opposite other)
        // For list of participants, partial inclusion for one or both sides
        
        // Future: personalilty change outcomes

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
