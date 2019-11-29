using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace StorySkeleton.ViewModels
{
    public abstract class TrendVM_Base : ViewModel_Base
    {
        public TrendVM_Base(int givenEventCount)
        {
            UpdateTimeVariable(givenEventCount);
        }

        private List<int> timeVariable;
        public List<int> TimeVariable { get { return timeVariable; } }

        protected List<List<int>> allFreeVariables;
        public List<List<int>> AllFreeVariables { get { return allFreeVariables; } }

        protected void UpdateTimeVariable(int givenEventCount)
        {
            timeVariable = new List<int>(givenEventCount);
            for (int i = 0; i <= givenEventCount; i++)
                timeVariable.Add(i);
        }

        public abstract void UpdateTrendLines();

        public Canvas GraphCanvas { get; private set; }

        public virtual void DrawGraph()
        {
            GraphCanvas = new Canvas();

            foreach (List<int> variable in allFreeVariables)
            {
                DrawPolyline(timeVariable, variable, null);//#TODO - draw different colors
            }

            OnPropertyChanged("GraphCanvas");
        }

        private void DrawPolyline(List<int> xAxisPoints, List<int> yAxisPoints, Brush theBrush)
        {
            Polyline line = new Polyline();
            PointCollection collection = new PointCollection();

            for (int i = 0; i < xAxisPoints.Count; i++)
            {
                collection.Add(new Point(xAxisPoints[i], yAxisPoints[i]));
            }

            line.Points = collection;

            if (theBrush == null)
                theBrush = new SolidColorBrush(Colors.Black);

            line.Stroke = theBrush;
            line.StrokeThickness = 1;

            GraphCanvas.Children.Add(line);
        }

    }
}
