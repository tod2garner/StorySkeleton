using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class AIncident : IIncident
    {
        //#TODO Future: triggers, chance to trigger other incident(s)

        //Concrete classes
        //          One role - participants
        //          Two role - acting, acted upon
        //          3 roles  - acting, acted upon, interrupting (e.g. hero rescues victim from attackers)
        //Future:
        //      Add classes with observer roles
        //      Multi-stage events - recursive triggers, and option to force a final trigger (e.g. deception)

        public AIncident()
        {
            prerequisites = new List<IPrerequisite>();
            allPossibleOutcomes = new List<PossibleResult>();
        }
        
        protected List<IPrerequisite> prerequisites;
        public List<IPrerequisite> MyPrerequisites { get { return prerequisites; } }
        
        private List<PossibleResult> allPossibleOutcomes;
        public List<PossibleResult> AllPossibleOutcomes { get { return allPossibleOutcomes; } }


        public bool CanAllPrerequisitesBeMet(SocietySnapshot currentCast)
        {
            var primaryPrereq = MyPrerequisites.First(); //first in list always given priority
            primaryPrereq.TryToFulfillFromScratch(currentCast);

            return !prerequisites.Any(p => p.IsMetByCurrentParticipants() == false);
        }

        public bool IsOutcomeTotal100Percent()
        {
            int totalOutcomePercent = 0;

            foreach (PossibleResult p in allPossibleOutcomes)
            {
                totalOutcomePercent += p.PercentChance;
            }

            return totalOutcomePercent == 100;
        }
    }
}
