using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class PossibleOutcome
    {
        public PossibleOutcome(int thePercentChance, IOutcome givenOutcome)
        {
            if(givenOutcome == null)
                throw new ArgumentNullException();

            if (thePercentChance > 100 || thePercentChance < 1)
                throw new ArgumentOutOfRangeException();

            this.percentChance = thePercentChance;
            this.theOutcome = givenOutcome;
        }
        
        private IOutcome theOutcome;
        public IOutcome TheOutcome
        {
            get { return theOutcome; }
        }

        private int percentChance;
        /// <summary>
        /// Between 1 and 100
        /// </summary>
        public int PercentChance
        {
            get { return percentChance; }
        }
    }
}
