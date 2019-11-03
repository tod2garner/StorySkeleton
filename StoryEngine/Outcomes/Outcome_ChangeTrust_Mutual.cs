using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Outcome_ChangeTrust_Mutual : MultiOutcome
    {
        public Outcome_ChangeTrust_Mutual(int magnitude, List<Character> selfList, List<Character> targetList)
        {
            this.theOutcomes.Add(new Outcome_ChangeTrust(magnitude, selfList, targetList));
            this.theOutcomes.Add(new Outcome_ChangeTrust(magnitude, targetList, selfList));
        }        
    }
}
