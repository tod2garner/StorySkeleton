using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public abstract class MultiOutcome : AOutcome
    {
        protected List<IOutcome> theOutcomes;

        public override void Execute()
        {
            foreach (IOutcome o in theOutcomes)
                o.Execute();
        }        
    }
}
