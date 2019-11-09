using System;
using StoryEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    public class Mock_Prereq_MutualTrust_Min : MutualTrust_Min
    {
        public Mock_Prereq_MutualTrust_Min(EthicsScale benchmark, Role whichRole) : base(benchmark, whichRole) { }

        public bool PublicHaveMutualTrustThatPassesBenchmark(Character a, Character b)
        {
            return this.HaveMutualTrustThatPassesBenchmark(a, b);
        }

        public bool PublicPassesBenchmark(EthicsScale value)
        {
            return this.PassesBenchmark(value);
        }

        public bool PublicAreRoleMinMaxCountsMet()
        {
            return this.AreRoleMinMaxCountsMet();
        }

        public Role GetRole()
        {
            return this.role;
        }

        public EthicsScale GetBenchmark()
        {
            return this.benchmarkTrust;
        }
    }

    public class Mock_Prereq_DirectionalEthics_Max : DirectionalEthics_Max
    {
        public Mock_Prereq_DirectionalEthics_Max(Role roleA, Role roleB, EthicsScale maximum_AtoB) : base(roleA, roleB, maximum_AtoB) { }

        public bool PublicHasDirectionalRelationThatPassesBenchmark(Character a, Character b)
        {
            return this.HasDirectionalRelationThatPassesBenchmark(a, b);
        }

        public bool PublicPassesBenchmark(EthicsScale value)
        {
            return this.PassesBenchmark(value);
        }

        public void PublicAddParticipantsRandomly(Role theRole, List<Character> theCandidates, Random rng = null)
        {
            DirectionalEthics_Max.AddParticipantsRandomly(theRole, theCandidates, rng);
        }

        public bool PublicAreRoleMinMaxCountsMet()
        {
            return this.AreRoleMinMaxCountsMet();
        }

        public Role GetRoleA()
        {
            return this.roleAlpha;
        }

        public Role GetRoleB()
        {
            return this.roleBeta;
        }

        public EthicsScale GetBenchmark_AtoB()
        {
            return this.benchmarkTrust_AtoB;
        }
    }

}
