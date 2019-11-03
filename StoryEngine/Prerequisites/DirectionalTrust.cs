using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class DirectionalTrust : ACrossRolePrerequisite
    {
        protected EthicsScale benchmarkTrust_Forward;//from A to B
        protected EthicsScale benchmarkTrust_Returned;//from B to A

        public DirectionalTrust(IncidentRole roleA, IncidentRole roleB, EthicsScale trust_AtoB, EthicsScale trust_BtoA)
        {
            benchmarkTrust = benchmark;
            this.role = whichRole;
        }

        public override bool IsMetByCurrentParticipants()
        {
            foreach (Character a in role.Participants)
            {
                if (role.Participants.Any(b => (a.Id != b.Id) && (HaveMutualTrustThatPassesBenchmark(a, b) == false)))
                    return false;
            }

            return true;
        }

        public override bool TryToFulfillFromScratch(SocietySnapshot currentCast, Random rng = null)
        {
            throw new NotImplementedException();
        }
    }
}
