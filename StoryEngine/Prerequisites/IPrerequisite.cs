using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public interface IPrerequisite
    {
        bool TryToFulfillFromScratch(SocietySnapshot currentCast, List<IPrerequisite> otherPrereqs, Random rng = null);

        bool IsMetByCurrentParticipants();

        bool WouldBeMetBySuggestedParticipant(Character candidate, string nameOfRole);
    }
}
