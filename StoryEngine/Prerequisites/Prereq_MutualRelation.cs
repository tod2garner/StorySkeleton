using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    [KnownType(typeof(MutualTrust))]
    [KnownType(typeof(MutualEthics))]
    [DataContract]
    public abstract class Prereq_MutualRelation : ARolePrerequisite
    {
        public Prereq_MutualRelation(Role whichRole)
        {
            this.role = whichRole;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public Prereq_MutualRelation() { }

        public override bool IsMetByCurrentParticipants()
        {
            foreach (Character a in role.Participants)
            {
                if (role.Participants.Any(b => (a.Id != b.Id) && (HaveMutualRelationThatPassesBenchmark(a, b) == false)))
                    return false;
            }

            return this.AreRoleMinMaxCountsMet();
        }

        public override bool WouldBeMetBySuggestedParticipant(Character candidate, string nameOfRole)
        {
            if (nameOfRole == this.role.RoleName)
            {
                if (role.Participants.Any(a => HaveMutualRelationThatPassesBenchmark(a, candidate) == false))
                    return false;
            }

            return true;
        }

        public override bool IsCharacterViableFirstCandidateForRole(Character candidate, string nameOfRole, SocietySnapshot currentCast)
        {
            if (nameOfRole == this.role.RoleName)
            {
                int minParticipants = role.MinCount.HasValue ? role.MinCount.Value : 0;
                int countQualifyingRelations = currentCast.AllCharacters.Count(b => HaveMutualRelationThatPassesBenchmark(candidate, b));

                if (countQualifyingRelations < minParticipants)
                    return false;
            }

            return true;
        }

        protected abstract bool HaveMutualRelationThatPassesBenchmark(Character a, Character b);

        protected abstract bool PassesBenchmark(EthicsScale value);
    }

    [KnownType(typeof(MutualTrust_Min))]
    [KnownType(typeof(MutualTrust_Max))]
    [DataContract]
    public abstract class MutualTrust : Prereq_MutualRelation
    {
        [DataMember]
        protected EthicsScale benchmarkTrust;

        public MutualTrust(EthicsScale benchmark, Role whichRole) : base(whichRole)
        {
            benchmarkTrust = benchmark;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public MutualTrust() { }

        protected override bool HaveMutualRelationThatPassesBenchmark(Character a, Character b)
        {
            var trustAtoB = a.GetTrustTowards(b);
            var trustBtoA = b.GetTrustTowards(a);

            return PassesBenchmark(trustAtoB) && PassesBenchmark(trustBtoA);
        }
    }

    /// <summary>
    /// Every character in the role must trust each other character in the role at least this much (inclusive min)
    /// </summary>
    public class MutualTrust_Min : MutualTrust
    {
        /// <param name="minimum">Inclusive minimum trust value</param>
        public MutualTrust_Min(EthicsScale minimum, Role whichRole) : base(minimum, whichRole) { }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public MutualTrust_Min() { }

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
    public class MutualTrust_Max : MutualTrust
    {
        /// <param name="maximum">Inclusive maximum trust value</param>
        public MutualTrust_Max(EthicsScale maximum, Role whichRole) : base(maximum, whichRole) { }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public MutualTrust_Max() { }

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
    
    [KnownType(typeof(MutualEthics_Min))]
    [KnownType(typeof(MutualEthics_Max))]
    [DataContract]
    public abstract class MutualEthics : Prereq_MutualRelation
    {
        [DataMember]
        protected EthicsScale benchmarkEthics;

        public MutualEthics(EthicsScale benchmark, Role whichRole) : base(whichRole)
        {
            benchmarkEthics = benchmark;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public MutualEthics() { }

        protected override bool HaveMutualRelationThatPassesBenchmark(Character a, Character b)
        {
            return PassesBenchmark(a.GetEthicsTowards(b)) && PassesBenchmark(b.GetEthicsTowards(a));
        }
    }

    public class MutualEthics_Min : MutualEthics
    {
        /// <param name="minimum">Inclusive minimum ethics value</param>
        public MutualEthics_Min(EthicsScale minimum, Role whichRole) : base(minimum, whichRole) { }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public MutualEthics_Min() { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkEthics;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRole = replacementRoles.FirstOrDefault(r => r.RoleName == this.role.RoleName);
            var theCopy = new MutualEthics_Min(this.benchmarkEthics, matchingRole);
            return theCopy;
        }
    }

    public class MutualEthics_Max : MutualEthics
    {
        /// <param name="maximum">Inclusive maximum ethics value</param>
        public MutualEthics_Max(EthicsScale maximum, Role whichRole) : base(maximum, whichRole) { }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public MutualEthics_Max() { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkEthics;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRole = replacementRoles.FirstOrDefault(r => r.RoleName == this.role.RoleName);
            var theCopy = new MutualEthics_Max(this.benchmarkEthics, matchingRole);
            return theCopy;
        }
    }
}
