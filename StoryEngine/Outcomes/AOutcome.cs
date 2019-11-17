using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    //Must tag KnownType for all derived classes for XML serialization to work
    [KnownType(typeof(ChangeInTrust))]
    [DataContract]
    public abstract class AOutcome : IOutcome
    {
        //#TODO
        //  For list of participants, partial inclusion for one or both sides
        // Future: personalilty change outcomes

        [DataMember]
        protected string name;
        public string OutcomeName { get { return name; } }

        public abstract List<string> ExecuteAndGiveSummary();

        public abstract AOutcome Copy(List<Role> replacementRoles);
    }
}
