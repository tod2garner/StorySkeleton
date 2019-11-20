using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public interface IIncident
    {        
        bool TryToPopulateIncident(SocietySnapshot currentCast, Random rng);
        int GetTotalOutcomePercentChance();
        void RollDiceAndExecuteOneOutcome(SocietySnapshot currentCast, Random rng);
        List<string> GetTextSummary();
    }
}
