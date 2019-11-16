using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    /// <summary>
    /// Restriction relative to two roles
    /// </summary>
    [KnownType(typeof(Prereq_DirectionalRelation))]//Must tag KnownType for all derived classes for XML serialization to work
    [DataContract]
    public abstract class ACrossRolePrerequisite : APrerequisite
    {
        [DataMember]
        protected Role roleAlpha;

        [DataMember]
        protected Role roleBeta;

        protected override bool AreRoleMinMaxCountsMet()
        {
            return roleAlpha.AreMinAndMaxMet() && roleBeta.AreMinAndMaxMet();
        }
    }
}
