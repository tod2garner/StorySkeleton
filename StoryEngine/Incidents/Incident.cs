using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public class Incident : AIncident
    {
        public Incident() : base()
        {
            allParticipants = new List<Role>();
        }

        private List<Role> allParticipants;
        public List<Role> AllParticipants { get { return allParticipants; } }

        public override void PopulateAllRolesRandomly(SocietySnapshot currentCast, Random rng = null)
        {
            if (rng == null)
                rng = new Random();

            var nonParticipants = currentCast.AllCharacters;

            foreach (Role r in this.allParticipants)
            {
                AIncident.AddParticipantsRandomly(r, nonParticipants, rng);
                nonParticipants = nonParticipants.Where(n => false == r.Participants.Any(p => n.Id == p.Id)).ToList();
            }
        }
    }
}
