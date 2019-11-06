using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Outcome_ChangeTrust_Mutual : MultiOutcome
    {
        public Outcome_ChangeTrust_Mutual(int magnitude, List<Character> listA, List<Character> listB) : base()
        {
            this.theOutcomes.Add(new Outcome_ChangeTrust(magnitude, listA, listB));
            this.theOutcomes.Add(new Outcome_ChangeTrust(magnitude, listB, listA));
        }
    }
}
