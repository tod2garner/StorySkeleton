using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.PlotGenerators
{
    public class PlotGenerator_TonePatterns : APlotGenerator
    {
        protected const int MAX_CONSECUTIVE_LOW_ENERGY = 2;
        protected const int MAX_CONSECUTIVE_PLEASANT = 2;
        protected const int MAX_CONSECUTIVE_HIGH_ENERGY = 5;
        protected const int MAX_CONSECUTIVE_UNPLEASANT = 8;
        protected const int MAX_CONSECUTIVE_OFTEN = 3;

        protected int consecutive_LowEnergy;
        protected int consecutive_Pleasant;
        protected int consecutive_HighEnergy;
        protected int consecutive_Unpleasant;
        protected int consecutive_Often;

        protected Incident fromPriorRound;

        protected override void CreateSequenceOfEvents(int maxNumIncidents, Random rng)
        {
            for (int i = 0; i < maxNumIncidents; i++)
            {
                var isJustBeforeLast = (i > maxNumIncidents - 4) && (i != maxNumIncidents - 1);//Two prior
                var isLast = (i == maxNumIncidents - 1);
                var nextIncident = this.GetNextEvent(rng, isJustBeforeLast, isLast);

                if (nextIncident == null) //Incident prerequisites not met, try again
                    continue;

                plotInProgress.ExecuteIncidentAndStoreAfter(nextIncident, this.currentCast, rng);
            }
        }

        protected IIncident GetNextEvent(Random rng, bool isJustBeforeEnding, bool isFinalEvent)
        {
            var chosenCollection = this.possibleIncidents.ChooseRandomCollection(rng);

            Pleasantness allowedPleasantness = Pleasantness.EitherPleasantOrNot;
            EnergyLevel allowedEnergy = EnergyLevel.EitherLowOrHigh;

            GetAllowedEnergyAndPleasantness(ref allowedPleasantness, ref allowedEnergy, isFinalEvent);
            Frequency minFrequency = GetMinFrequency(isJustBeforeEnding);

            var chosenIncident = chosenCollection.GetRandomIncident(rng, allowedPleasantness, allowedEnergy, minFrequency);

            var popluatedSucessfully = chosenIncident?.TryToPopulateIncident(this.currentCast, rng) ?? false;
            if (popluatedSucessfully == false)
                return null;
            else
            {
                UpdatePatternTracking(chosenIncident);
                return chosenIncident;
            }
        }

        protected Frequency GetMinFrequency(bool isJustBeforeEnding)
        {
            Frequency minF = Frequency.Often;

            if (isJustBeforeEnding)
                minF = fromPriorRound.TheFrequency < Frequency.Rarely ? Frequency.ExtremelyRarely : Frequency.Rarely;
            else if (consecutive_Often >= MAX_CONSECUTIVE_OFTEN)
                minF = Frequency.Rarely;

            return minF;
        }

        protected void GetAllowedEnergyAndPleasantness(ref Pleasantness p, ref EnergyLevel e, bool isFinalEvent)
        {
            if (isFinalEvent)
            {
                //Final event has opposite energy of the one before it
                if (fromPriorRound.TheTone.IsHighEnergy())
                    e = EnergyLevel.AlwaysLowEnergy;
                else
                    e = EnergyLevel.AlwaysHighEnergy;
            }
            else if (consecutive_HighEnergy >= MAX_CONSECUTIVE_HIGH_ENERGY)
                e = EnergyLevel.AlwaysLowEnergy;
            else if (consecutive_LowEnergy >= MAX_CONSECUTIVE_LOW_ENERGY)
                e = EnergyLevel.AlwaysHighEnergy;

            if (consecutive_Pleasant >= MAX_CONSECUTIVE_PLEASANT)
                p = Pleasantness.NeverPleasant;
            else if (consecutive_Unpleasant >= MAX_CONSECUTIVE_UNPLEASANT)
                p = Pleasantness.AlwaysPleasant;

            //Future:
            //  Toggle to allow hook for follow-up story. If true,
            //  allow high energy unpleasant as final event. If false,
            //  50% chance to have happy ending vs 50% chance to cut final event short and have tragedy
        }

        protected void UpdatePatternTracking(Incident chosen)
        {
            Update_ConsecutivePleasantness(chosen);
            Update_ConsecutiveEnergyLevel(chosen);
            Update_ConsecutiveFrequency(chosen);

            fromPriorRound = chosen;
        }

        protected void Update_ConsecutivePleasantness(Incident chosen)
        {
            var isPleasant_Now = chosen.TheTone.IsPleasant();
            var isPleasant_Prior = fromPriorRound?.TheTone.IsPleasant() ?? !isPleasant_Now;

            if (isPleasant_Now == isPleasant_Prior)
            {
                if (isPleasant_Now)
                    consecutive_Pleasant++;
                else
                    consecutive_Unpleasant++;
            }
            else
            {
                if (isPleasant_Now)
                {
                    consecutive_Unpleasant = 0;
                    consecutive_Pleasant = 1;
                }
                else
                {
                    consecutive_Pleasant = 0;
                    consecutive_Unpleasant = 1;
                }
            }
        }

        protected void Update_ConsecutiveEnergyLevel(Incident chosen)
        {
            var isHighEnergy_Now = chosen.TheTone.IsHighEnergy();
            var isHighEnergy_Prior = fromPriorRound?.TheTone.IsHighEnergy() ?? !isHighEnergy_Now;

            if (isHighEnergy_Now == isHighEnergy_Prior)
            {
                if (isHighEnergy_Now)
                    consecutive_HighEnergy++;
                else
                    consecutive_LowEnergy++;
            }
            else
            {
                if (isHighEnergy_Now)
                {
                    consecutive_LowEnergy = 0;
                    consecutive_HighEnergy = 1;
                }
                else
                {
                    consecutive_HighEnergy = 0;
                    consecutive_LowEnergy = 1;
                }
            }
        }

        protected void Update_ConsecutiveFrequency(Incident chosen)
        {
            var isOften_Now = chosen.TheTone.IsHighEnergy();
            var isOften_Prior = fromPriorRound?.TheTone.IsHighEnergy() ?? !isOften_Now;

            if (isOften_Now == isOften_Prior)
            {
                if (isOften_Now)
                    consecutive_Often++;
            }
            else
            {
                if (isOften_Now)
                {
                    consecutive_Often = 1;
                }
                else
                {
                    consecutive_Often = 0;
                }
            }
        }
    }
}
