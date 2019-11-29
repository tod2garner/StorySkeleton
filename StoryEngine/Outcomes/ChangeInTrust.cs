using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    [DataContract]
    public class ChangeInTrust : AOutcome
    {
        [DataMember]
        private int magnitude;
        public int Magnitude { get { return magnitude; } }

        [DataMember]
        private Role beingChanged;
        public Role BeingChanged { get { return beingChanged; } }

        [DataMember]
        private Role towards;
        public Role Towards { get { return towards; } }

        public ChangeInTrust(int magnitudeOfTrustChange, Role change, Role target, String givenName)
        {
            magnitude = magnitudeOfTrustChange;
            beingChanged = change;
            towards = target;
            this.name = givenName;
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public ChangeInTrust() { }

        public override AOutcome Copy(List<Role> replacementRoles)
        {
            var matchBeingChanged = replacementRoles.FirstOrDefault(r => r.RoleName == this.beingChanged.RoleName);
            var matchTowards = replacementRoles.FirstOrDefault(r => r.RoleName == this.towards.RoleName);

            var theCopy = new ChangeInTrust(this.magnitude, matchBeingChanged, matchTowards, this.name);
            return theCopy;
        }

        public override List<string> ExecuteAndGiveSummary()
        {
            var textSummary = new List<string>();

            if (beingChanged.Participants.Any(a => towards.Participants.Any(b => a.Id != b.Id)))
            {
                textSummary.Add(this.name);
                textSummary.Add(DescribeOutcomeParticipants());
            }

            foreach (Character s in beingChanged.Participants)
            {
                foreach (Character t in towards.Participants)
                {
                    var description = s.ChangeTrust(magnitude, t);

                    foreach(string d in description)
                        textSummary.Add(string.Format("    - {0}", d));
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
