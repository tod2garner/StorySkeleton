using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Outcome_ChangeTrust_Divergent : MultiOutcome
    {
        public Outcome_ChangeTrust_Divergent(int magnitude_AtoB, int magnitude_BtoA, List<Character> listA, List<Character> listB)
        {
            this.theOutcomes.Add(new Outcome_ChangeTrust(magnitude_AtoB, listA, listB));
            this.theOutcomes.Add(new Outcome_ChangeTrust(magnitude_BtoA, listB, listA));
        }
    }
}
