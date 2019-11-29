using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryEngine;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StorySkeleton.ViewModels
{
    public class TrendVM_Relation : TrendVM_Base
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

        public override void DrawGraph()
        {
            GraphCanvas = new Canvas();
            GraphCanvas.Width = 400;
            GraphCanvas.Height = 200;
            GraphCanvas.Background = Brushes.Black;

            DrawHorizontalBenchmarks();

            if(allFreeVariables.Count >= 4)
            {
                var dashStyle1 = new DoubleCollection() { 3, 4, 6, 4 };
                var dashStyle2 = new DoubleCollection() { 2, 6, 4, 3 };
                var dashStyle3 = new DoubleCollection() { 5, 7, 2, 4 };
                var dashStyle4 = new DoubleCollection() { 4, 2, 8, 5 };

                DrawPolyline(TimeVariable, allFreeVariables[0], 3, Brushes.Blue, dashStyle1);
                DrawPolyline(TimeVariable, allFreeVariables[1], 3, Brushes.Green, dashStyle2);
                DrawPolyline(TimeVariable, allFreeVariables[2], 3, Brushes.Red, dashStyle3);
                DrawPolyline(TimeVariable, allFreeVariables[3], 3, Brushes.DarkOrange, dashStyle4);
            }

            OnPropertyChanged("GraphCanvas");
        }

        public override void UpdateTrendLines()
        {
            allFreeVariables = new List<List<int>>();

            if(idCharacterA != IdCharacterB)
            {
                allFreeVariables.Add(GetPoints_Score(idCharacterA, idCharacterB, true));//Trust A->B
                allFreeVariables.Add(GetPoints_Score(idCharacterA, idCharacterB, false));//Ethics A->B
                allFreeVariables.Add(GetPoints_Score(idCharacterB, idCharacterA, true));//Trust B->A
                allFreeVariables.Add(GetPoints_Score(idCharacterB, idCharacterA, false));//Ethics B->A
            }

            DrawGraph();
        }

        public void DrawHorizontalBenchmarks()
        {
            var xValues = new List<int>(2) { 0, TimeVariable.Max() };
            var dashStyle = new DoubleCollection() { 1, 8 };
            foreach (EthicsScale e in Enum.GetValues(typeof(EthicsScale)))
            {
                var theY = (int)e * Relationship.SCALE_FOR_GAPS_BETWEEN_TRUST_LEVELS;
                var yValues = new List<int>(2) { theY, theY };

                DrawPolyline(xValues, yValues, 0.5, Brushes.AntiqueWhite, dashStyle);
            }
        }

        protected List<int> GetPoints_Score(int idOne, int idTwo, bool GetTrust_NotEthics)
        {
            var allTrustScores = new List<int>();

            var charOne_atstart = thePlot.StartingCast.AllCharacters.First(c => c.Id == idOne);
            var startScore = GetTrust_NotEthics ? charOne_atstart.GetTrust_Score_TowardsId(idTwo) : charOne_atstart.GetEthics_Score_TowardsId(idTwo);

            allTrustScores.Add(startScore ?? 0);

            foreach (SocietySnapshot s in thePlot.TheCastOverTime)
            {
                var charOne = s.AllCharacters.First(c => c.Id == idOne);
                var theScore = GetTrust_NotEthics ? charOne.GetTrust_Score_TowardsId(idTwo) : charOne.GetEthics_Score_TowardsId(idTwo);

                allTrustScores.Add(theScore ?? 0);
            }

            return allTrustScores;
        }

        protected override double Scale_Y_value(int y)
        {

            //#TODOD - change to log scale
            var maxY = 1.25 * (int)EthicsScale.Confide * Relationship.SCALE_FOR_GAPS_BETWEEN_TRUST_LEVELS;
            var minY = 1.25 * (int)EthicsScale.Murder * Relationship.SCALE_FOR_GAPS_BETWEEN_TRUST_LEVELS;

            var drawnMax = GraphCanvas.Height;

            double scaledY = (double)(y - minY) * (double)drawnMax / (double)(maxY - minY);
            scaledY = Math.Min(scaledY, drawnMax - 5);
            scaledY = Math.Max(scaledY, 5);

            return drawnMax - scaledY;//inverting so that Confide is at top and Murder is at bottom
        }
    }
}
