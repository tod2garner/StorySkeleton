using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace StoryEngine
{
    [KnownType(typeof(DirectionalEthics))]
    [KnownType(typeof(DirectionalTrust))]
    [DataContract]
    public abstract class Prereq_DirectionalRelation : ACrossRolePrerequisite
    {
        public Prereq_DirectionalRelation(Role roleA, Role roleB)
        {
            this.roleAlpha = roleA;
            this.roleBeta = roleB;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public Prereq_DirectionalRelation() { }

        public override bool IsMetByCurrentParticipants()
        {
            foreach (Character a in roleAlpha.Participants)
            {
                if (roleBeta.Participants.Any(b => HasDirectionalRelationThatPassesBenchmark(a, b) == false))
                    return false;
            }

            return this.AreRoleMinMaxCountsMet();
        }

        public override bool WouldBeMetBySuggestedParticipant(Character candidate, string nameOfRole)
        {
            if (nameOfRole == this.roleAlpha.RoleName)
            {
                if (roleBeta.Participants.Any(b => HasDirectionalRelationThatPassesBenchmark(candidate, b) == false))
                    return false;
            }
            else if (nameOfRole == this.roleBeta.RoleName)
            {
                if (roleAlpha.Participants.Any(a => HasDirectionalRelationThatPassesBenchmark(a, candidate) == false))
                    return false;
            }

            return true;
        }

        public override bool IsCharacterViableFirstCandidateForRole(Character candidate, string nameOfRole, SocietySnapshot currentCast)
        {
            if (nameOfRole == this.roleAlpha.RoleName)
            {
                int minRoleB = roleBeta.MinCount.HasValue ? roleBeta.MinCount.Value : 0;
                if (CountRelationsThatPassBenchmark(candidate) < minRoleB)
                    return false;
            }
            else if (nameOfRole == this.roleBeta.RoleName)
            {
                if(false == currentCast.AllCharacters.Any(a => HasDirectionalRelationThatPassesBenchmark(a, candidate)))
                    return false;
            }

            return true;
        }
        
        protected abstract int CountRelationsThatPassBenchmark(Character alpha);

        protected abstract bool HasDirectionalRelationThatPassesBenchmark(Character a, Character b);
    }

    [KnownType(typeof(DirectionalEthics_Min))]
    [KnownType(typeof(DirectionalEthics_Max))]
    [DataContract]
    public abstract class DirectionalEthics : Prereq_DirectionalRelation
    {
        [DataMember]
        protected EthicsScale benchmarkEthics_AtoB;

        public DirectionalEthics(EthicsScale ethics_AtoB, Role roleA, Role roleB) : base(roleA, roleB)
        {
            benchmarkEthics_AtoB = ethics_AtoB;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public DirectionalEthics() { }


        protected override int CountRelationsThatPassBenchmark(Character alpha)
        {
            return alpha.AllRelations.Count(r => PassesBenchmark(alpha.GetEthicsTowardsId(r.OtherId)));
        }

        protected override bool HasDirectionalRelationThatPassesBenchmark(Character a, Character b)
        {
            return PassesBenchmark(a.GetEthicsTowardsId(b.Id));
        }

        protected virtual bool PassesBenchmark(EthicsScale? theValue)
        {
            if (theValue.HasValue == false) //#TODO - overwrite method for situations where a null relationship would meet benchmark
                return false;

            return PassesBenchmark(theValue.Value);
        }

        protected abstract bool PassesBenchmark(EthicsScale theValue);
    }

    [KnownType(typeof(DirectionalTrust_Min))]
    [KnownType(typeof(DirectionalTrust_Max))]
    [DataContract]
    public abstract class DirectionalTrust : Prereq_DirectionalRelation
    {
        [DataMember]
        protected EthicsScale benchmarkTrust_AtoB;

        public DirectionalTrust(EthicsScale trust_AtoB, Role roleA, Role roleB) : base(roleA, roleB)
        {
            benchmarkTrust_AtoB = trust_AtoB;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public DirectionalTrust() { }


        protected override int CountRelationsThatPassBenchmark(Character alpha)
        {
            return alpha.AllRelations.Where(r => PassesBenchmark(alpha.GetTrustTowardsId(r.OtherId))).Count();
        }

        protected override bool HasDirectionalRelationThatPassesBenchmark(Character a, Character b)
        {
            return PassesBenchmark(a.GetTrustTowardsId(b.Id));
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
        /// <param name="minimum_AtoB">Inclusive minimum ethics value</param>
        public DirectionalEthics_Min(EthicsScale minimum_AtoB, Role roleA, Role roleB) : base(minimum_AtoB, roleA, roleB) { }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public DirectionalEthics_Min() { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkEthics_AtoB;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRoleA = replacementRoles.FirstOrDefault(r => r.RoleName == this.roleAlpha.RoleName);
            var matchingRoleB = replacementRoles.FirstOrDefault(r => r.RoleName == this.roleBeta.RoleName);

            var theCopy = new DirectionalEthics_Min(this.benchmarkEthics_AtoB, matchingRoleA, matchingRoleB);
            return theCopy;
        }
    }

    public class DirectionalEthics_Max : DirectionalEthics
    {
        /// <param name="maximum_AtoB">Inclusive maximum ethics value</param>
        public DirectionalEthics_Max(EthicsScale maximum_AtoB, Role roleA, Role roleB) : base(maximum_AtoB, roleA, roleB) { }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public DirectionalEthics_Max() { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkEthics_AtoB;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRoleA = replacementRoles.FirstOrDefault(r => r.RoleName == this.roleAlpha.RoleName);
            var matchingRoleB = replacementRoles.FirstOrDefault(r => r.RoleName == this.roleBeta.RoleName);

            var theCopy = new DirectionalEthics_Max(this.benchmarkEthics_AtoB, matchingRoleA, matchingRoleB);
            return theCopy;
        }
    }

    public class DirectionalTrust_Min : DirectionalTrust
    {
        /// <param name="minimum_AtoB">Inclusive minimum trust value</param>
        public DirectionalTrust_Min(EthicsScale minimum_AtoB, Role roleA, Role roleB) : base(minimum_AtoB, roleA, roleB) { }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public DirectionalTrust_Min() { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust_AtoB;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRoleA = replacementRoles.FirstOrDefault(r => r.RoleName == this.roleAlpha.RoleName);
            var matchingRoleB = replacementRoles.FirstOrDefault(r => r.RoleName == this.roleBeta.RoleName);

            var theCopy = new DirectionalTrust_Min(this.benchmarkTrust_AtoB, matchingRoleA, matchingRoleB);
            return theCopy;
        }
    }

    public class DirectionalTrust_Max : DirectionalTrust
    {
        /// <param name="maximum_AtoB">Inclusive maximum trust value</param>
        public DirectionalTrust_Max(EthicsScale maximum_AtoB, Role roleA, Role roleB) : base(maximum_AtoB, roleA, roleB) { }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public DirectionalTrust_Max() { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust_AtoB;
        }

        public override IPrerequisite Copy(List<Role> replacementRoles)
        {
            var matchingRoleA = replacementRoles.FirstOrDefault(r => r.RoleName == this.roleAlpha.RoleName);
            var matchingRoleB = replacementRoles.FirstOrDefault(r => r.RoleName == this.roleBeta.RoleName);

            var theCopy = new DirectionalTrust_Max(this.benchmarkTrust_AtoB, matchingRoleA, matchingRoleB);
            return theCopy;
        }
    }
}
