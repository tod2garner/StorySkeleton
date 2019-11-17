using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryEngine;
using StoryEngine.PlotGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UtPlotGenerator_Default
    {
        private PlotGenerator_Default theGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            theGenerator = new PlotGenerator_Default();           
        }

        [TestMethod]
        public void PlotGeneratorDefault_GetNextEventRandomly()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PlotGeneratorDefault_GenerateNewPlot()
        {
            throw new NotImplementedException();
        }
    }
}
