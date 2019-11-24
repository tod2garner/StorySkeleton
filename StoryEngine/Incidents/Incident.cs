using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Incident : AIncident
    {
        public Incident(string givenName) : base(givenName) { }

        private Tone theTone;
        public Tone TheTone { get { return theTone; } }

        public EnergyLevel TheEnergyVariation;
        public Pleasantness TheStressVariation;

        public void SetToneRandomly(Random rng)
        {
            if (rng == null)
                rng = new Random();

            var possibilities = IncidentEnumExtensions.GetPossibleTones(TheEnergyVariation, TheStressVariation);
            var diceRoll = rng.Next(0, possibilities.Count);
            theTone = possibilities[diceRoll];
        }

        public override void InitializeTextSummary()
        {
            base.InitializeTextSummary();

            var summarizeTone = string.Format("EMOTIONAL TONE: {0}", this.theTone.ToCustomString());
            this.textSummary.Add(summarizeTone);
        }

    }
}
