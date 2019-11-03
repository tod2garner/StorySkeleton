using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Outcome_ChangeTrust : AOutcome
    {
        private int magnitude;
        private List<Character> selves;
        private List<Character> targets;

        public Outcome_ChangeTrust(int magnitudeOfTrustChange, List<Character> selfList, List<Character> targetList)
        {
            magnitude = magnitudeOfTrustChange;
            selves = selfList;
            targets = targetList;
        }
        
        public override void Execute()
        {
            foreach (Character s in selves)
            {
                foreach (Character t in targets)
                {
                    s.ChangeTrust(magnitude, t);
                }
            }
        }
    }
}
