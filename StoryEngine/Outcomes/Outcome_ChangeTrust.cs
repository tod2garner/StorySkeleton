using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Outcome_ChangeTrust : AOutcome
    {
        private int magnitude;
        public int Magnitude { get { return magnitude; } }

        private Role beingChanged;
        public Role BeingChanged { get { return beingChanged; } }

        private Role towards;
        public Role Towards { get { return towards; } }

        public Outcome_ChangeTrust(int magnitudeOfTrustChange, Role change, Role target)
        {
            magnitude = magnitudeOfTrustChange;
            beingChanged = change;
            towards = target;
        }
        
        public override List<string> Execute()
        {
            var textSummary = new List<string>();
            textSummary.Add("  Outcome: Change Trust");

            foreach (Character s in beingChanged.Participants)
            {
                foreach (Character t in towards.Participants)
                {
                    var description = s.ChangeTrust(magnitude, t);

                    if(description.Length > 0)
                        textSummary.Add(string.Format("    - {0}", description));                    
                }
            }
            return textSummary;
        }
    }
}
