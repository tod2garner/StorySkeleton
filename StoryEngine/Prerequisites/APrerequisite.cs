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
        //Specifically, need way to align participant selection across roles, as prereqs are by role        

        //Abstract classes to create:
        //  quantity rule (min/max role count) 
        //      - by default, if no min role count on event, role can be left empty and filled by unnamed minor character
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
        
        public bool CanBeFulfilled(SocietySnapshot currentCast)
        {
            throw new NotImplementedException();
        }
    }
}
