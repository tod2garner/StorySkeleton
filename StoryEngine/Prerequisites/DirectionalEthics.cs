﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class DirectionalEthics : ACrossRolePrerequisite
    {
        protected EthicsScale benchmarkTrust_AtoB;

        public DirectionalEthics(IncidentRole roleA, IncidentRole roleB, EthicsScale trust_AtoB)
        {
            benchmarkTrust_AtoB = trust_AtoB;
            this.roleAlpha = roleA;
            this.roleBeta = roleB;
        }

        public override bool IsMetByCurrentParticipants()
        {
            foreach (Character a in roleAlpha.Participants)
            {
                if (roleBeta.Participants.Any(b => HasDirectionalEthicsThatPassesBenchmark(a,b) == false))
                    return false;
            }

            return true;
        }

        public override bool TryToFulfillFromScratch(SocietySnapshot currentCast, Random rng = null)
        {
            throw new NotImplementedException(); //#TODO
        }

        protected virtual bool HasDirectionalEthicsThatPassesBenchmark(Character a, Character b)
        {
            return PassesBenchmark(a.GetEthicsTowards(b.Id));
        }

        protected virtual bool PassesBenchmark(EthicsScale? theValue)
        {
            if (theValue.HasValue == false) //#TODO - overwrite method for situations where a null relationship would meet benchmark
                return false;

            return PassesBenchmark(theValue.Value);
        }

        protected abstract bool PassesBenchmark(EthicsScale theValue);
    }

    
    public class DirectionalEthics_Min : DirectionalEthics
    {
        /// <param name="minimum">Inclusive minimum ethics value</param>
        public DirectionalEthics_Min(IncidentRole roleA, IncidentRole roleB, EthicsScale minimum) : base(roleA, roleB, minimum) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust_AtoB;
        }
    }

    public class DirectionalEthics_Max : DirectionalEthics
    {
        /// <param name="maximum">Inclusive maximum ethics value</param>
        public DirectionalEthics_Max(IncidentRole roleA, IncidentRole roleB, EthicsScale maximum) : base(roleA, roleB, maximum) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust_AtoB;
        }
    }

    public abstract class DirectionalTrust : DirectionalEthics
    {
        protected override bool HasDirectionalEthicsThatPassesBenchmark(Character a, Character b)
        {
            return PassesBenchmark(a.GetTrustTowards(b.Id));
        }
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

    public class DirectionalTrust_Max : DirectionalTrust
    {
        /// <param name="maximum">Inclusive maximum trust value</param>
        public DirectionalTrust_Max(IncidentRole roleA, IncidentRole roleB, EthicsScale maximum) : base(roleA, roleB, maximum) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust_AtoB;
        }
    }
}