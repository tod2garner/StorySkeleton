using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StoryEngine
{
    /// <summary>
    /// Defines conditions that participants must meet to be included in an incident
    /// </summary>
    [XmlInclude(typeof(ARolePrerequisite))]
    [XmlInclude(typeof(ACrossRolePrerequisite))]
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

        public abstract bool IsMetByCurrentParticipants();

        public abstract bool WouldBeMetBySuggestedParticipant(Character candidate, string nameOfRole);

        public abstract bool IsCharacterViableFirstCandidateForRole(Character candidate, string nameOfRole, SocietySnapshot currentCast);

        public abstract IPrerequisite Copy(List<Role> replacementRoles);

        protected abstract bool AreRoleMinMaxCountsMet();
    }
}
