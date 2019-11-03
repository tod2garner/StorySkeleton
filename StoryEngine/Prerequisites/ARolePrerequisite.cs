using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    /// <summary>
    /// Restriction for a single role
    /// </summary>
    public abstract class ARolePrerequisite : APrerequisite
    {
        protected IncidentRole role;

        protected override bool AreRoleMinMaxCountsMet()
        {
            return role.AreMinAndMaxMet();
        }
    }
}
