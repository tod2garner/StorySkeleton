using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public interface IPrerequisite
    {
        bool TryToFulfill(SocietySnapshot currentCast, Random rng = null);
    }
}
