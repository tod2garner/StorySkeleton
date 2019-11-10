using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class Prereq_DirectionalRelation : ACrossRolePrerequisite
    {
        public Prereq_DirectionalRelation(Role roleA, Role roleB)
        {
            this.roleAlpha = roleA;
            this.roleBeta = roleB;
        }

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

        public override bool TryToFulfillFromScratch(SocietySnapshot currentCast, List<IPrerequisite> otherPrereqs, Random rng = null)
        {
            throw new NotImplementedException();//#TODO implement otherPrerequs

            int minRoleB = roleBeta.MinCount.HasValue ? roleBeta.MinCount.Value : 0;

            List<Character> alphaCandidates = new List<Character>();
            foreach (Character a in currentCast.AllCharacters)
            {
                int countQualifyingRelations = currentCast.AllCharacters.Count(b => (b.Id != a.Id) && HasDirectionalRelationThatPassesBenchmark(a, b));

                if (countQualifyingRelations >= minRoleB)
                    alphaCandidates.Add(a);
            }

            if (alphaCandidates.Any() == false)
                return false;

            if (rng == null)
                rng = new Random();

            var firstAlpha = alphaCandidates[rng.Next(0, alphaCandidates.Count)];
            roleAlpha.Participants.Add(firstAlpha);
            alphaCandidates.Remove(firstAlpha);

            var myBetaCandidates = currentCast.AllCharacters.Where(b => (b.Id != firstAlpha.Id) && HasDirectionalRelationThatPassesBenchmark(firstAlpha, b)).ToList();
            var testedBetaCandidates = TestCandidatesAgainstOtherPrereqs(myBetaCandidates, roleBeta.RoleName, otherPrereqs);

            AIncident.AddParticipantsRandomly(roleBeta, testedBetaCandidates, rng);

            alphaCandidates = alphaCandidates.Where(a => false == roleBeta.Participants.Any(b => a.Id == b.Id)).ToList();
            var remainingAlphaCandidates = alphaCandidates.Where(a => false == roleBeta.Participants.Any(b => false == HasDirectionalRelationThatPassesBenchmark(a, b))).ToList();

            AIncident.AddParticipantsRandomly(roleAlpha, remainingAlphaCandidates, rng);

            return this.IsMetByCurrentParticipants();
        }

        protected abstract bool HasDirectionalRelationThatPassesBenchmark(Character a, Character b);
    }

    public abstract class DirectionalEthics : Prereq_DirectionalRelation
    {
        protected EthicsScale benchmarkEthics_AtoB;

        public DirectionalEthics(EthicsScale ethics_AtoB, Role roleA, Role roleB) : base(roleA, roleB)
        {
            benchmarkEthics_AtoB = ethics_AtoB;
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

    public abstract class DirectionalTrust : Prereq_DirectionalRelation
    {
        protected EthicsScale benchmarkTrust_AtoB;

        public DirectionalTrust(EthicsScale trust_AtoB, Role roleA, Role roleB) : base(roleA, roleB)
        {
            benchmarkTrust_AtoB = trust_AtoB;
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

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkEthics_AtoB;
        }
    }

    public class DirectionalEthics_Max : DirectionalEthics
    {
        /// <param name="maximum_AtoB">Inclusive maximum ethics value</param>
        public DirectionalEthics_Max(EthicsScale maximum_AtoB, Role roleA, Role roleB) : base(maximum_AtoB, roleA, roleB) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkEthics_AtoB;
        }
    }

    public class DirectionalTrust_Min : DirectionalTrust
    {
        /// <param name="minimum_AtoB">Inclusive minimum trust value</param>
        public DirectionalTrust_Min(EthicsScale minimum_AtoB, Role roleA, Role roleB) : base(minimum_AtoB, roleA, roleB) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust_AtoB;
        }
    }

    public class DirectionalTrust_Max : DirectionalTrust
    {
        /// <param name="maximum_AtoB">Inclusive maximum trust value</param>
        public DirectionalTrust_Max(EthicsScale maximum_AtoB, Role roleA, Role roleB) : base(maximum_AtoB, roleA, roleB) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust_AtoB;
        }
    }
}
