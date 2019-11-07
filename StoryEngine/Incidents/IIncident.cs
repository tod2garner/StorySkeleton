using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public interface IIncident
    {        
        bool CanAllPrerequisitesBeMet(SocietySnapshot currentCast);
        bool IsOutcomeTotal100Percent();
    }
}
