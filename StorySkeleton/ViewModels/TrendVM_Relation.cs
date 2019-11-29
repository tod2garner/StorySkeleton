using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine;

namespace StorySkeleton.ViewModels
{
    public class TrendVM_Relation :TrendVM_Base
    {
        public TrendVM_Relation(Plot givenPlot) : base(givenPlot.TheIncidents.Count)
        {
            thePlot = givenPlot;
            idCharacterA = 0;
            idCharacterB = 1;
            UpdateTrendLines();
        }

        private Plot thePlot;

        //#TODO - fix for if characters are added after starting cast
        public List<Character> AllCharacters { get { return thePlot.StartingCast.AllCharacters; } }

        private int idCharacterA;
        public int IdCharacterA
        {
            get { return idCharacterA; }
            set { idCharacterA = value; UpdateTrendLines(); OnPropertyChanged("IdCharacterA"); }
        }

        private int idCharacterB;
        public int IdCharacterB
        {
            get { return idCharacterB; }
            set { idCharacterB = value; UpdateTrendLines(); OnPropertyChanged("IdCharacterB"); }
        }

        public override void UpdateTrendLines()
        {
            allFreeVariables = new List<List<int>>();

            allFreeVariables.Add(GetPoints_Score(idCharacterA, idCharacterB, true));
            allFreeVariables.Add(GetPoints_Score(idCharacterA, idCharacterB, false));
            allFreeVariables.Add(GetPoints_Score(idCharacterB, idCharacterA, true));
            allFreeVariables.Add(GetPoints_Score(idCharacterB, idCharacterA, false));

            base.DrawGraph();
        }

        protected List<int> GetPoints_Score(int idOne, int idTwo, bool GetTrust_NotEthics)
        {
            var allTrustScores = new List<int>();
            
            var charOne_atstart = thePlot.StartingCast.AllCharacters.First(c => c.Id == idOne);
            var startScore = GetTrust_NotEthics ? charOne_atstart.GetTrust_Score_TowardsId(idTwo) : charOne_atstart.GetEthics_Score_TowardsId(idTwo);

            allTrustScores.Add(startScore ?? 0);

            foreach(SocietySnapshot s in thePlot.TheCastOverTime)
            {
                var charOne = s.AllCharacters.First(c => c.Id == idOne);
                var theScore = GetTrust_NotEthics ? charOne.GetTrust_Score_TowardsId(idTwo) : charOne.GetEthics_Score_TowardsId(idTwo);

                allTrustScores.Add(theScore ?? 0);
            }

            return allTrustScores;
        }
    }
}
