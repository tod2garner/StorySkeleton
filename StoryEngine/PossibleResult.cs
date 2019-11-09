﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class PossibleResult
    {
        public PossibleResult(int thePercentChance)
        {

            if (thePercentChance > 100 || thePercentChance < 1)
                throw new ArgumentOutOfRangeException();

            this.percentChance = thePercentChance;
            this.theOutcomes = new List<IOutcome>();
        }

        private List<IOutcome> theOutcomes;
        public List<IOutcome> TheOutcomes { get { return theOutcomes; } }

        private int percentChance;
        /// <summary>
        /// Between 1 and 100
        /// </summary>
        public int PercentChance
        {
            get { return percentChance; }
        }

        public string Execute()
        {
            string textSummary = string.Empty;
            foreach (var o in TheOutcomes)
            {
                textSummary += o.Execute();
            }

            return textSummary.Trim();
        }
    }
}
