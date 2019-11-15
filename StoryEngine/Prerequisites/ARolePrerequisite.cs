using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StoryEngine
{
    /// <summary>
    /// Restriction for a single role
    /// </summary>
    [XmlInclude(typeof(Prereq_MutualRelation))]
    public abstract class ARolePrerequisite : APrerequisite
    {
        protected Role role;

        protected override bool AreRoleMinMaxCountsMet()
        {
            return role.AreMinAndMaxMet();
        }
    }
}
