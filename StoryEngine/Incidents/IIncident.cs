using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public interface IIncident
    {        
        bool TryToFulfillAllPrerequisites(SocietySnapshot currentCast);
        int GetTotalOutcomePercentChance();
        void RollDiceAndExecuteOneOutcome(SocietySnapshot currentCast, Random rng = null);
        string GetTextSummary();
    }
}
