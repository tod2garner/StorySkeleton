using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public interface IPrerequisite
    {
        bool IsMetByCurrentParticipants();

        bool IsCharacterViableFirstCandidateForRole(Character candidate, string nameOfRole, SocietySnapshot currentCast);

        bool WouldBeMetBySuggestedParticipant(Character candidate, string nameOfRole);
    }
}
