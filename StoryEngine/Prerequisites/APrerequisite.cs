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

        //Abstract classes to create:
        //  relation rule
        //      for a single role (e.g. conversation = mutual min trust)
        //      for two roles (e.g. betrayal = ethics for one, trust for other)
        //Future:
        //  character trait rule (e.g. baseSuspicion min/max)
        //      used for interactions with minor/un-named characters?


        //Concrete classes to create:
        //      minTrust
        //      maxTrust
        //      exactTrust
        //      mutualTrust_Min/Max/Exact
        //      unevenTrust_Smaller
        //      unevenTrust_Larger
        //Future:
        //      ethics equivalents

        public abstract bool TryToFulfill(SocietySnapshot currentCast, Random rng = null);

        protected abstract bool AreRoleMinMaxCountsMet();
    }
}
