using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    /// <summary>
    /// Restriction relative to two roles
    /// </summary>
    public abstract class ACrossRolePrerequisite : APrerequisite
    {
        protected IncidentRole roleAlpha;
        protected IncidentRole roleBeta;

        protected override bool AreRoleMinMaxCountsMet()
        {
            return roleAlpha.AreMinAndMaxMet() && roleBeta.AreMinAndMaxMet();
        }
    }
}
