using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StoryEngine
{
    //Must tag XmlInclude all derived classes for XML serialization to work
    [XmlInclude(typeof(Outcome_ChangeTrust))]
    public abstract class AOutcome : IOutcome
    {
        //#TODO
        //  For list of participants, partial inclusion for one or both sides
        // Future: personalilty change outcomes

        public abstract List<string> ExecuteAndGiveSummary();

        public abstract AOutcome Copy(List<Role> replacementRoles);
    }
}
