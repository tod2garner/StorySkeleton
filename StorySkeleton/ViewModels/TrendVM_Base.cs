﻿using System;
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

        public Canvas GraphCanvas { get; protected set; }

        public virtual void DrawGraph()
        {
            GraphCanvas = new Canvas();
            GraphCanvas.Width = 400;
            GraphCanvas.Height = 200;
            GraphCanvas.Background = Brushes.Black;

            foreach (List<int> variable in allFreeVariables)
            {
                DrawPolyline(timeVariable, variable, 1, null);
            }

            OnPropertyChanged("GraphCanvas");
        }

        protected void DrawPolyline(List<int> xAxisPoints, List<int> yAxisPoints, double strokeSize, Brush theBrush, DoubleCollection dashPattern = null)
        {
            Polyline line = new Polyline();
            PointCollection collection = new PointCollection();

            for (int i = 0; i < xAxisPoints.Count; i++)
            {
                var scaledX = Scale_X_value(xAxisPoints[i]);
                var scaledY = Scale_Y_value(yAxisPoints[i]);
                var thePoint = new Point(scaledX, scaledY);
                collection.Add(thePoint);
            }

            line.Points = collection;

            if (theBrush == null)
                theBrush = new SolidColorBrush(Colors.Blue);

            line.Stroke = theBrush;
            line.StrokeThickness = strokeSize;

            if (dashPattern != null)
                line.StrokeDashArray = dashPattern;

            GraphCanvas.Children.Add(line);
        }

        protected double Scale_X_value(int x)
        {
            var maxX = this.TimeVariable.Max();

            var drawnMax = GraphCanvas.Width;

            double scaledX = (double)x * (double)drawnMax / (double)maxX;

            return scaledX;
        }

        protected abstract double Scale_Y_value(int y);
    }
}
