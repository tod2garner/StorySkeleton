using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Incidents.DefaultLibrary
{
    public static class DefaultLibraryGenerator
    {
        private const string SAVE_FILE_PATH = "C:\\temp\\DefaultLibrary\\";

        public static LibraryOfIncidents LoadDefaultLibraryFromFile()
        {
            //#TODO - fix with relative references & multiple collections
            var path = "C:\\temp\\DefaultLibrary\\collection1.xml";

            var theLibrary = new LibraryOfIncidents();
            var theCollection = SerializeXML.LoadFromXML<CollectionOfIncidentTemplates>(path);
            var theTuple = new Tuple<int, CollectionOfIncidentTemplates>(100, theCollection);
            theLibrary.AllCollections.Add(theTuple);
            return theLibrary;
        }

        public static void GenerateFilesForDefaultLibrary()
        {
            var defaultCollection = new CollectionOfIncidentTemplates();

            defaultCollection.TheTemplates.Add(CreateTemplateManually.AccidentalOffense());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Agression_Social());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Argument_Personal());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Betrayal_Emotional());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Betrayal_Social());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Conversation_Personal());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Cooperation_Social());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Cooperation_Utilitarian());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Deception());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Rejection_Emotional());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Rejection_Social());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.SacrificeForOther());

            //#TODO - break into separate collection
            defaultCollection.TheTemplates.Add(CreateTemplateManually.SelfImprovement());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Training());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Travel());

            //#TODO - break into separate collection
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Lost());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.NaturalDisaster());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Disease());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Weather_Challenging());

            defaultCollection.SaveToXML(SAVE_FILE_PATH + "collection1.xml");
        }

    }
}
