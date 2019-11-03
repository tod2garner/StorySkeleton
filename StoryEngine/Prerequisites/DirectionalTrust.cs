using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class DirectionalTrust : ACrossRolePrerequisite
    {
        protected EthicsScale benchmarkTrust_AtoB;

        public DirectionalTrust(IncidentRole roleA, IncidentRole roleB, EthicsScale trust_AtoB)
        {
            benchmarkTrust_AtoB = trust_AtoB;
            this.roleAlpha = roleA;
            this.roleBeta = roleB;
        }

        public override bool IsMetByCurrentParticipants()
        {
            foreach (Character a in roleAlpha.Participants)
            {
                if (roleBeta.Participants.Any(b => PassesBenchmark(a.GetTrustTowards(b.Id)) == false))
                    return false;
            }

            return true;
        }

        public override bool TryToFulfillFromScratch(SocietySnapshot currentCast, Random rng = null)
        {
            throw new NotImplementedException();
        }

        protected virtual bool PassesBenchmark(EthicsScale? theValue)
        {
            if (theValue.HasValue == false) //#TODO - overwrite method for situations where a null relationship would meet benchmark
                return false;

            return PassesBenchmark(theValue.Value);
        }

        protected abstract bool PassesBenchmark(EthicsScale theValue);
    }

    public class DirectionalTrust_Min : DirectionalTrust
    {
        /// <param name="minimum">Inclusive minimum trust value</param>
        public DirectionalTrust_Min(IncidentRole roleA, IncidentRole roleB, EthicsScale minimum) : base(roleA, roleB, minimum) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust_AtoB;
        }
    }

    public class DirectionalTrust_Max : MutualTrust
    {
        /// <param name="maximum">Inclusive maximum trust value</param>
        public DirectionalTrust_Max(IncidentRole roleA, IncidentRole roleB, EthicsScale maximum) : base(roleA, roleB, maximum) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust_AtoB;
        }
    }
}
