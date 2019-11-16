using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    /// <summary>
    /// Restriction for a single role
    /// </summary>
    [KnownType(typeof(Prereq_MutualRelation))]//Must tag KnownType for all derived classes for XML serialization to work
    [DataContract]
    public abstract class ARolePrerequisite : APrerequisite
    {
        [DataMember]
        protected Role role;

        protected override bool AreRoleMinMaxCountsMet()
        {
            return role.AreMinAndMaxMet();
        }
    }
}
