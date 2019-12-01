using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StoryEngine
{
    [DataContract]
    public class CollectionOfIncidentTemplates : AObjectWithProbability
    {
        public CollectionOfIncidentTemplates(int theProbabilityScore) : base(theProbabilityScore)
        {
            theTemplates = new List<TemplateForIncident>();
        }

        [DataMember]
        private List<TemplateForIncident> theTemplates;
        public List<TemplateForIncident> TheTemplates
        {
            get { return theTemplates; }
            set { theTemplates = value; }
        }

        public void LoadFromFile()
        {
            throw new NotImplementedException(); //#TODO
        }

        public void SaveToFile()
        {
            throw new NotImplementedException();//#TODO
        }

        public Incident GetRandomIncident(Random rng)
        {
            return GetRandomIncident(rng, Pleasantness.EitherPleasantOrNot, EnergyLevel.EitherLowOrHigh);
        }

        public Incident GetRandomIncident(Random rng, Pleasantness reqd_p, EnergyLevel reqd_e)
        {
            //First, exclude by Rarity
            var matchRarity = IncidentEnumExtensions.GetRandomFrequency_Weighted(rng);
            var possibleTemplates = this.TheTemplates.Where(t => t.TheFrequency == matchRarity).ToList();

            //Next, exclude by EnergyLevel
            if (reqd_e != EnergyLevel.EitherLowOrHigh)
                possibleTemplates = possibleTemplates.Where(t => t.IsHighEnergy == EnergyLevel.EitherLowOrHigh || t.IsHighEnergy == reqd_e).ToList();

            //Finally, exclude by Pleasantness
            if (reqd_p != Pleasantness.EitherPleasantOrNot)
                possibleTemplates = possibleTemplates.Where(t => t.IsPleasant == Pleasantness.EitherPleasantOrNot || t.IsPleasant == reqd_p).ToList();

            if (false == possibleTemplates.Any())
                return null;

            var diceRoll = rng.Next(0, possibleTemplates.Count);
            return possibleTemplates[diceRoll].CreateIncident(rng, reqd_p, reqd_e);
        }
    }
}
