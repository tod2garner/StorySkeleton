﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class Prereq_MutualTrust : ARolePrerequisite
    {
        protected EthicsScale benchmarkTrust;

        public Prereq_MutualTrust(EthicsScale benchmark, Role whichRole)
        {
            benchmarkTrust = benchmark;
            this.role = whichRole;
        }

        public Prereq_MutualTrust() { }//Parameterless constructor req'd for XML serialization

        public override bool IsMetByCurrentParticipants()
        {
            foreach (Character a in role.Participants)
            {
                if (role.Participants.Any(b => (a.Id != b.Id) && (HaveMutualTrustThatPassesBenchmark(a, b) == false)))
                    return false;
            }

            return this.AreRoleMinMaxCountsMet();
        }

        public override bool WouldBeMetBySuggestedParticipant(Character candidate, string nameOfRole)
        {
            if (nameOfRole == this.role.RoleName)
            {
                if (role.Participants.Any(a => HaveMutualTrustThatPassesBenchmark(a, candidate) == false))
                    return false;
            }

            return true;
        }

        public override bool IsCharacterViableFirstCandidateForRole(Character candidate, string nameOfRole, SocietySnapshot currentCast)
        {
            if (nameOfRole == this.role.RoleName)
            {
                int minParticipants = role.MinCount.HasValue ? role.MinCount.Value : 0;
                int countQualifyingRelations = currentCast.AllCharacters.Count(b => HaveMutualTrustThatPassesBenchmark(candidate, b));

                if (countQualifyingRelations < minParticipants)
                    return false;
            }

            return true;
        }

        protected virtual bool HaveMutualTrustThatPassesBenchmark(Character a, Character b)
        {
            var trustAtoB = a.GetTrustTowards(b);
            var trustBtoA = b.GetTrustTowards(a);

            return PassesBenchmark(trustAtoB) && PassesBenchmark(trustBtoA);
        }

        protected abstract bool PassesBenchmark(EthicsScale value);
    }

    /// <summary>
    /// Every character in the role must trust each other character in the role at least this much (inclusive min)
    /// </summary>
    public class MutualTrust_Min : Prereq_MutualTrust
    {
        /// <param name="minimum">Inclusive minimum trust value</param>
        public MutualTrust_Min(EthicsScale minimum, Role whichRole) : base(minimum, whichRole) { }

        public MutualTrust_Min() { }//Parameterless constructor req'd for XML serialization

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRole = replacementRoles.FirstOrDefault(r => r.RoleName == this.role.RoleName);
            var theCopy = new MutualTrust_Min(this.benchmarkTrust, matchingRole);
            return theCopy;
        }
    }

    /// <summary>
    /// Every character in the role must trust each other character in the role no greater than max (inclusive)
    /// </summary>
    public class MutualTrust_Max : Prereq_MutualTrust
    {
        /// <param name="maximum">Inclusive maximum trust value</param>
        public MutualTrust_Max(EthicsScale maximum, Role whichRole) : base(maximum, whichRole) { }

        public MutualTrust_Max() { }//Parameterless constructor req'd for XML serialization

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRole = replacementRoles.FirstOrDefault(r => r.RoleName == this.role.RoleName);
            var theCopy = new MutualTrust_Max(this.benchmarkTrust, matchingRole);
            return theCopy;
        }
    }

    public abstract class MutualEthics : Prereq_MutualTrust
    {
        public MutualEthics(EthicsScale benchmark, Role whichRole) : base(benchmark, whichRole) { }

        public MutualEthics() { }//Parameterless constructor req'd for XML serialization

        protected override bool HaveMutualTrustThatPassesBenchmark(Character a, Character b)
        {
            return PassesBenchmark(a.GetEthicsTowards(b)) && PassesBenchmark(b.GetEthicsTowards(a));
        }
    }

    public class MutualEthics_Min : MutualEthics
    {
        /// <param name="minimum">Inclusive minimum ethics value</param>
        public MutualEthics_Min(EthicsScale minimum, Role whichRole) : base(minimum, whichRole) { }

        public MutualEthics_Min() { }//Parameterless constructor req'd for XML serialization

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRole = replacementRoles.FirstOrDefault(r => r.RoleName == this.role.RoleName);
            var theCopy = new MutualEthics_Min(this.benchmarkTrust, matchingRole);
            return theCopy;
        }
    }

    public class MutualEthics_Max : MutualEthics
    {
        /// <param name="maximum">Inclusive maximum ethics value</param>
        public MutualEthics_Max(EthicsScale maximum, Role whichRole) : base(maximum, whichRole) { }

        public MutualEthics_Max() { }//Parameterless constructor req'd for XML serialization

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRole = replacementRoles.FirstOrDefault(r => r.RoleName == this.role.RoleName);
            var theCopy = new MutualEthics_Max(this.benchmarkTrust, matchingRole);
            return theCopy;
        }
    }
}
