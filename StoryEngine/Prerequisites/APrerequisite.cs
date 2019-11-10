using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    /// <summary>
    /// Defines conditions that participants must meet to be included in an incident
    /// </summary>
    public abstract class APrerequisite : IPrerequisite
    {
        //#TODO        

        //Concrete classes to create:
        //      proportionalRelations
        //      proportionalTrust_Smaller
        //      proportionalTrust_Larger

        //Future:
        //  character trait rule (e.g. baseSuspicion min/max)
        //      used for interactions with minor/un-named characters?

        public abstract bool TryToFulfillFromScratch(SocietySnapshot currentCast, List<IPrerequisite> otherPrereqs, Random rng = null);

        public abstract bool IsMetByCurrentParticipants();

        public abstract bool WouldBeMetBySuggestedParticipant(Character candidate, string nameOfRole);

        protected abstract bool AreRoleMinMaxCountsMet();
    }
}
