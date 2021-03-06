﻿using System;
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
            return this.HaveMutualRelationThatPassesBenchmark(a, b);
        }

        public bool PublicPassesBenchmark(EthicsScale value)
        {
            return this.PassesBenchmark(value);
        }

        public bool PublicAreRoleMinMaxCountsMet()
        {
            return this.AreRoleMinMaxCountsMet();
        }
    }

    public class Mock_Prereq_DirectionalEthics_Max : DirectionalEthics_Max
    {
        public Mock_Prereq_DirectionalEthics_Max(EthicsScale maximum_AtoB, Role roleA, Role roleB) : base(maximum_AtoB, roleA, roleB) { }

        public bool PublicHasDirectionalRelationThatPassesBenchmark(Character a, Character b)
        {
            return this.HasDirectionalRelationThatPassesBenchmark(a, b);
        }

        public bool PublicPassesBenchmark(EthicsScale value)
        {
            return this.PassesBenchmark(value);
        }

        public bool PublicAreRoleMinMaxCountsMet()
        {
            return this.AreRoleMinMaxCountsMet();
        }
    }

}
