﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    //#TODO - add unit tests

    public abstract class MutualTrust : ARolePrerequisite
    {
        protected EthicsScale benchmarkTrust;

        public MutualTrust(EthicsScale benchmark, IncidentRole whichRole)
        {
            benchmarkTrust = benchmark;
            this.role = whichRole;
        }

        //Rather than checking for the largest possible group with mutual trust,
        // we simply use a random starting point and try to form a qualifying group.
        public override bool TryToFulfill(SocietySnapshot currentCast, Random rng = null)
        {
            int maxParticipants = role.MaxCount.HasValue ? role.MaxCount.Value : IncidentRole.DEFAULT_ROLE_MAX_COUNT;
            int minParticipants = role.MinCount.HasValue ? role.MinCount.Value : 0; //role can be left empty, assumed filled by unnamed minor characters
            
            List<Character> candidates = new List<Character>();
            foreach (Character a in currentCast.AllCharacters)
            {
                int countQualifyingMutualTrust = currentCast.AllCharacters.Count(b => (b.Id != a.Id) && PassesBenchmark(a.GetTrustTowards(b.Id)) && PassesBenchmark(b.GetTrustTowards(a.Id)));
                
                if (countQualifyingMutualTrust >= minParticipants)
                    candidates.Add(a);
            }

            if (candidates.Any() == false)
                return false;
            
            if (rng == null)
                rng = new Random();

            var firstChar = candidates[rng.Next(0, candidates.Count)];
            role.Participants.Add(firstChar);
            candidates.Remove(firstChar);

            var finalCandidates = candidates.Where(b => PassesBenchmark(firstChar.GetTrustTowards(b.Id)) && PassesBenchmark(b.GetTrustTowards(firstChar.Id))).ToList();

            maxParticipants = System.Math.Min(maxParticipants, finalCandidates.Count);
            int targetCount = rng.Next(minParticipants, maxParticipants);

            while (finalCandidates.Any() && role.Participants.Count < targetCount)
            {
                var nextChar = finalCandidates[rng.Next(0, finalCandidates.Count)];
                finalCandidates.Remove(nextChar);

                if (role.Participants.Any(p => PassesBenchmark(p.GetTrustTowards(nextChar.Id)) == false || PassesBenchmark(nextChar.GetTrustTowards(p.Id)) == false))
                    continue;

                role.Participants.Add(nextChar);
            }
            
            return this.AreRoleMinMaxCountsMet();
        }

        protected virtual bool PassesBenchmark(EthicsScale? value)
        {
            if (value.HasValue == false) //#TODO - overwrite method for situations where a null relationship would meet benchmark
                return false;

            return PassesBenchmark(value.Value);
        }

        protected abstract bool PassesBenchmark(EthicsScale value);
    }

    /// <summary>
    /// Every character in the role must trust each other character in the role at least this much (inclusive min)
    /// </summary>
    public class MutualTrust_Min : MutualTrust
    {
        /// <param name="minimum">Inclusive minimum trust value</param>
        public MutualTrust_Min(EthicsScale minimum, IncidentRole whichRole) : base(minimum, whichRole)
        {
            //Only calling base constructor
        }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value >= this.benchmarkTrust;
        }
    }

    /// <summary>
    /// Every character in the role must trust each other character in the role no greater than max (inclusive)
    /// </summary>
    public class MutualTrust_Max : MutualTrust
    {
        /// <param name="maximum">Inclusive maximum trust value</param>
        public MutualTrust_Max(EthicsScale maximum, IncidentRole whichRole) : base(maximum, whichRole)
        {
            //Only calling base constructor
        }

        protected override bool PassesBenchmark(EthicsScale value)
        {
            return value <= this.benchmarkTrust;
        }
    }
}
