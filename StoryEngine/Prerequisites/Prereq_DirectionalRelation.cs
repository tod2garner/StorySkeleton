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

        public override bool TryToFulfillFromScratch(SocietySnapshot currentCast, Random rng = null)
        {
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

            var betaCandidates = currentCast.AllCharacters.Where(b => (b.Id != firstAlpha.Id) && HasDirectionalRelationThatPassesBenchmark(firstAlpha, b)).ToList();

            AddParticipantsRandomly(roleBeta, betaCandidates, rng);

            alphaCandidates = alphaCandidates.Where(a => false == roleBeta.Participants.Any(b => a.Id == b.Id)).ToList();
            var remainingAlphaCandidates = alphaCandidates.Where(a => false == roleBeta.Participants.Any(b => false == HasDirectionalRelationThatPassesBenchmark(a, b))).ToList();

            AddParticipantsRandomly(roleAlpha, remainingAlphaCandidates, rng);

            return this.IsMetByCurrentParticipants();
        }

        protected static void AddParticipantsRandomly(Role theRole, List<Character> theCandidates, Random rng = null)
        {
            if (rng == null)
                rng = new Random();

            int min = theRole.MinCount.HasValue ? theRole.MinCount.Value : 0; //role can be left empty, assumed filled by unnamed minor characters
            int max = theRole.MaxCount.HasValue ? theRole.MaxCount.Value : Role.DEFAULT_ROLE_MAX_COUNT;
            max = System.Math.Min(max, theCandidates.Count);

            if (min > max)
                return;

            int targetCount = rng.Next(min, max + 1);

            for (int i = 0; i < targetCount; i++)
            {
                if (theCandidates.Count == 0)
                    break;

                var nextChosen = theCandidates[rng.Next(0, theCandidates.Count)];
                theRole.Participants.Add(nextChosen);
                theCandidates.Remove(nextChosen);
            }
        }

        protected abstract bool HasDirectionalRelationThatPassesBenchmark(Character a, Character b);                
    }
    
    public abstract class DirectionalEthics : Prereq_DirectionalRelation
    {
        protected EthicsScale benchmarkTrust_AtoB;

        public DirectionalEthics(Role roleA, Role roleB, EthicsScale trust_AtoB) : base(roleA, roleB)
        {
            benchmarkTrust_AtoB = trust_AtoB;
        }

        protected override bool HasDirectionalRelationThatPassesBenchmark(Character a, Character b)
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

    public abstract class DirectionalTrust : Prereq_DirectionalRelation
    {
        protected EthicsScale benchmarkTrust_AtoB;

        public DirectionalTrust(Role roleA, Role roleB, EthicsScale trust_AtoB) : base(roleA, roleB)
        {
            benchmarkTrust_AtoB = trust_AtoB;
        }

        protected override bool HasDirectionalRelationThatPassesBenchmark(Character a, Character b)
        {
            return PassesBenchmark(a.GetTrustTowards(b.Id));
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
        public DirectionalEthics_Min(Role roleA, Role roleB, EthicsScale minimum_AtoB) : base(roleA, roleB, minimum_AtoB) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust_AtoB;
        }
    }

    public class DirectionalEthics_Max : DirectionalEthics
    {
        /// <param name="maximum_AtoB">Inclusive maximum ethics value</param>
        public DirectionalEthics_Max(Role roleA, Role roleB, EthicsScale maximum_AtoB) : base(roleA, roleB, maximum_AtoB) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust_AtoB;
        }
    }

    public class DirectionalTrust_Min : DirectionalTrust
    {
        /// <param name="minimum_AtoB">Inclusive minimum trust value</param>
        public DirectionalTrust_Min(Role roleA, Role roleB, EthicsScale minimum_AtoB) : base(roleA, roleB, minimum_AtoB) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust_AtoB;
        }
    }

    public class DirectionalTrust_Max : DirectionalTrust
    {
        /// <param name="maximum_AtoB">Inclusive maximum trust value</param>
        public DirectionalTrust_Max(Role roleA, Role roleB, EthicsScale maximum_AtoB) : base(roleA, roleB, maximum_AtoB) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust_AtoB;
        }
    }
}
