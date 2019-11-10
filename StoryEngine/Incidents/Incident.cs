using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Incident : AIncident
    {
        public Incident(string givenName) : base(givenName)
        {
            allRoles = new List<Role>();
        }

        private List<Role> allRoles;
        public List<Role> AllParticipantRoles { get { return allRoles; } }

        public override void PopulateAllRolesRandomly(SocietySnapshot currentCast, Random rng = null)
        {
            if (rng == null)
                rng = new Random();

            var nonParticipants = currentCast.AllCharacters.ToList();//must copy list to avoid changes to original

            foreach (Role r in this.allRoles)
            {
                AIncident.AddParticipantsRandomly(r, nonParticipants, rng);
                nonParticipants = nonParticipants.Where(n => false == r.Participants.Any(p => n.Id == p.Id)).ToList();
            }
        }
    }
}
