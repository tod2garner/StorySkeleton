﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    [DataContract]
    public class PossibleResult
    {
        public PossibleResult(int thePercentChance)
        {

            if (thePercentChance > 100 || thePercentChance < 1)
                throw new ArgumentOutOfRangeException();

            this.percentChance = thePercentChance;
            this.theOutcomes = new List<AOutcome>();
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public PossibleResult()
        {
            this.theOutcomes = new List<AOutcome>();
        }

        [DataMember]
        private List<AOutcome> theOutcomes;
        public List<AOutcome> TheOutcomes { get { return theOutcomes; } }

        [DataMember]
        private int percentChance;
        /// <summary>
        /// Between 1 and 100
        /// </summary>
        public int PercentChance
        {
            get { return percentChance; }
        }

        public List<string> Execute()
        {
            var textSummary = new List<string>();
            foreach (var o in TheOutcomes)
            {
                textSummary.AddRange(o.ExecuteAndGiveSummary());
            }

            return textSummary;
        }

        public PossibleResult Copy(List<Role> replacementRoles)
        {
            var theCopy = new PossibleResult(this.percentChance);

            foreach (AOutcome o in theOutcomes)
                theCopy.theOutcomes.Add(o.Copy(replacementRoles));

            return theCopy;
        }
    }
}
