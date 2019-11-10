using System;
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

        public override bool IsMetByCurrentParticipants()
        {
            foreach (Character a in role.Participants)
            {
                if (role.Participants.Any(b => (a.Id != b.Id) && (HaveMutualTrustThatPassesBenchmark(a, b) == false)))
                    return false;
            }

            return this.AreRoleMinMaxCountsMet();
        }

        //Rather than checking for the largest possible group with mutual trust,
        // we simply use a random starting point and try to form a qualifying group.
        public override bool TryToFulfillFromScratch(SocietySnapshot currentCast, Random rng = null)
        {
            int maxParticipants = role.MaxCount.HasValue ? role.MaxCount.Value : Role.DEFAULT_ROLE_MAX_COUNT;
            int minParticipants = role.MinCount.HasValue ? role.MinCount.Value : 0; //role can be left empty, assumed filled by unnamed minor characters

            List<Character> candidates = new List<Character>();
            foreach (Character a in currentCast.AllCharacters)
            {
                int countQualifyingRelations = currentCast.AllCharacters.Count(b => (b.Id != a.Id) && HaveMutualTrustThatPassesBenchmark(a, b));

                if (countQualifyingRelations >= minParticipants)
                    candidates.Add(a);
            }

            if (candidates.Any() == false)
                return false;

            if (rng == null)
                rng = new Random();

            var firstChar = candidates[rng.Next(0, candidates.Count)];
            role.Participants.Add(firstChar);
            candidates.Remove(firstChar);

            var finalCandidates = candidates.Where(b => HaveMutualTrustThatPassesBenchmark(b, firstChar)).ToList();

            maxParticipants = System.Math.Min(maxParticipants, finalCandidates.Count);
            int targetCount = rng.Next(minParticipants, maxParticipants + 1);

            while (finalCandidates.Any() && role.Participants.Count < targetCount)
            {
                var nextChar = finalCandidates[rng.Next(0, finalCandidates.Count)];
                finalCandidates.Remove(nextChar);

                if (role.Participants.Any(p => HaveMutualTrustThatPassesBenchmark(p, nextChar) == false))
                    continue;

                role.Participants.Add(nextChar);
            }

            return this.IsMetByCurrentParticipants();
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

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust;
        }
    }

    /// <summary>
    /// Every character in the role must trust each other character in the role no greater than max (inclusive)
    /// </summary>
    public class MutualTrust_Max : Prereq_MutualTrust
    {
        /// <param name="maximum">Inclusive maximum trust value</param>
        public MutualTrust_Max(EthicsScale maximum, Role whichRole) : base(maximum, whichRole) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust;
        }
    }

    public abstract class MutualEthics : Prereq_MutualTrust
    {
        public MutualEthics(EthicsScale benchmark, Role whichRole) : base(benchmark, whichRole) { }

        protected override bool HaveMutualTrustThatPassesBenchmark(Character a, Character b)
        {
            return PassesBenchmark(a.GetEthicsTowards(b)) && PassesBenchmark(b.GetEthicsTowards(a));
        }
    }

    public class MutualEthics_Min : MutualEthics
    {
        /// <param name="minimum">Inclusive minimum ethics value</param>
        public MutualEthics_Min(EthicsScale minimum, Role whichRole) : base(minimum, whichRole) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust;
        }
    }

    public class MutualEthics_Max : MutualEthics
    {
        /// <param name="maximum">Inclusive maximum ethics value</param>
        public MutualEthics_Max(EthicsScale maximum, Role whichRole) : base(maximum, whichRole) { }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust;
        }
    }
}
