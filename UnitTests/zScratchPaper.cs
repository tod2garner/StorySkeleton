using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryEngine;
using StoryEngine.SocietyGenerators;
using StoryEngine.PlotGenerators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class zScratchPaper
    {
        [TestMethod]
        public void aaa__aSaveTemplateToFile()
        {
            StoryEngine.Incidents.DefaultLibrary.DefaultLibraryGenerator.GenerateFilesForDefaultLibrary();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void aaa__ScratchPaper_PlotFromScratch()
        {
            //Create a simple plot from scratch
            Random rng = new Random();

            int characterCount = 15;
            StartingCastGenerator_Default characterFactory = new StartingCastGenerator_Default();
            SocietySnapshot currentCast = characterFactory.CreateStartingCast(characterCount, rng);
            Plot thePlot = new Plot(currentCast);

            //Prepare psuedo "event library"            
            var accidentalOffense = createIncidentManually_AccidentalOffense();
            var socialAgression = createIncidentManually_Agression_Social();
            var socialCooperation = createIncidentManually_Cooperation_Social();
            //NOTE - must create NEW instace of event for each use (or create a way to reset participants)

            //Create sequence of events
            //Event #1
            var ableToFillRoles = accidentalOffense.TryToPopulateIncident(currentCast, rng);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(accidentalOffense, currentCast, rng);

            //Event #2
            ableToFillRoles = socialAgression.TryToPopulateIncident(currentCast, rng);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialAgression, currentCast, rng);

            //Event #3
            ableToFillRoles = socialCooperation.TryToPopulateIncident(currentCast, rng);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialCooperation, currentCast, rng);

            //Event #4
            socialAgression = createIncidentManually_Agression_Social();
            ableToFillRoles = socialAgression.TryToPopulateIncident(currentCast, rng);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialAgression, currentCast, rng);

            //Event #5
            socialCooperation = createIncidentManually_Cooperation_Social();
            ableToFillRoles = socialCooperation.TryToPopulateIncident(currentCast, rng);
            Assert.IsTrue(ableToFillRoles);
            thePlot.ExecuteIncidentAndStoreAfter(socialCooperation, currentCast, rng);


            //Display narrative
            var theTextNarrative = thePlot.CompileTextNarrative();
            foreach (string s in theTextNarrative)
                Debug.WriteLine(s);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void aaa_GenerateNewRandomPlot()
        {
            var theGenerator = new PlotGenerator_Default();
            var theLibrary = StoryEngine.Incidents.DefaultLibrary.DefaultLibraryGenerator.LoadDefaultLibraryFromFile();

            var thePlot = theGenerator.GenerateNewPlot(theLibrary, null, 20);


            //Print narrative
            var theTextNarrative = thePlot.CompileTextNarrative();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\\temp\\RandomPlot.txt"))
            {
                foreach (string line in theTextNarrative)
                    file.WriteLine(line);
            }

            Assert.IsTrue(true);

            throw new NotImplementedException();
        }

        private IIncident createIncidentManually_AccidentalOffense()
        {
            return StoryEngine.Incidents.DefaultLibrary.CreateTemplateManually.AccidentalOffense().CreateIncident(null);
        }

        private IIncident createIncidentManually_Agression_Social()
        {
            return StoryEngine.Incidents.DefaultLibrary.CreateTemplateManually.Aggression_Social().CreateIncident(null);
        }

        private IIncident createIncidentManually_Cooperation_Social()
        {
            return StoryEngine.Incidents.DefaultLibrary.CreateTemplateManually.Cooperation_Social().CreateIncident(null);
        }

    }
}
