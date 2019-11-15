using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StoryEngine
{
    /// <summary>
    /// Restriction relative to two roles
    /// </summary>
    [XmlInclude(typeof(Prereq_DirectionalRelation))]
    public abstract class ACrossRolePrerequisite : APrerequisite
    {
        protected Role roleAlpha;
        protected Role roleBeta;

        protected override bool AreRoleMinMaxCountsMet()
        {
            return roleAlpha.AreMinAndMaxMet() && roleBeta.AreMinAndMaxMet();
        }
    }
}
