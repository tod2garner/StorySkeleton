using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Outcome_ChangeTrust : AOutcome
    {
        private string name;
        public string OutcomeName { get { return name; } }

        private int magnitude;
        public int Magnitude { get { return magnitude; } }

        private Role beingChanged;
        public Role BeingChanged { get { return beingChanged; } }

        private Role towards;
        public Role Towards { get { return towards; } }

        public Outcome_ChangeTrust(int magnitudeOfTrustChange, Role change, Role target, String givenName)
        {
            magnitude = magnitudeOfTrustChange;
            beingChanged = change;
            towards = target;
            this.name = givenName;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public Outcome_ChangeTrust() { }

        public override AOutcome Copy(List<Role> replacementRoles)
        {
            var matchBeingChanged = replacementRoles.FirstOrDefault(r => r.RoleName == this.beingChanged.RoleName);
            var matchTowards = replacementRoles.FirstOrDefault(r => r.RoleName == this.towards.RoleName);

            var theCopy = new Outcome_ChangeTrust(this.magnitude, matchBeingChanged, matchTowards, this.name);
            return theCopy;
        }

        public override List<string> ExecuteAndGiveSummary()
        {
            var textSummary = new List<string>();

            if (beingChanged.Participants.Any(a => towards.Participants.Any(b => a.Id != b.Id)))
            {
                textSummary.Add(string.Format("OUTCOME: {0}", this.name));
                textSummary.Add(DescribeOutcomeParticipants());
            }

            foreach (Character s in beingChanged.Participants)
            {
                foreach (Character t in towards.Participants)
                {
                    var description = s.ChangeTrust(magnitude, t);

                    if (description.Length > 0)
                        textSummary.Add(string.Format("    - {0}", description));
                }
            }

            return textSummary;
        }

        public string DescribeOutcomeParticipants()
        {
            string description = "  ";

            for (int i = 0; i < beingChanged.Participants.Count; i++)
            {
                if (0 == beingChanged.Participants.Count - 1)
                    description += beingChanged.Participants[i].Name;
                else if (i != beingChanged.Participants.Count - 1)
                    description += beingChanged.Participants[i].Name + ", ";
                else
                    description += "and " + beingChanged.Participants[i].Name;
            }

            description += magnitude > 0 ? " gain some trust towards " : " lose some trust towards ";

            if (beingChanged.RoleName == towards.RoleName)
            {
                description += "each other";
            }
            else
            {
                for (int i = 0; i < towards.Participants.Count; i++)
                {
                    if (0 == towards.Participants.Count - 1)
                        description += towards.Participants[i].Name;
                    else if (i != towards.Participants.Count - 1)
                        description += towards.Participants[i].Name + ", ";
                    else
                        description += "and " + towards.Participants[i].Name;
                }
            }

            return description;
        }
    }
}
