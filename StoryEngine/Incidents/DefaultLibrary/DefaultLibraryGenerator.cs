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
            theLibrary.AllCollections.Add(theCollection);
            return theLibrary;
        }

        public static void GenerateFilesForDefaultLibrary()
        {
            var defaultCollection = new CollectionOfIncidentTemplates(100);

            //#TODO - align emotional tone with outcomes?

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
            defaultCollection.TheTemplates.Add(CreateTemplateManually.SocialGathering());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Message_Received());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Message_Lost());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.AcquireTool());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.EquipmentFailure());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Injury_Accidental());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.RestAndRecover());

            //#TODO - break into separate collection
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Lost());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.NaturalDisaster());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Disease());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Weather_Challenging());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.DangerousAnimal());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Survival());

            defaultCollection.SaveToXML(SAVE_FILE_PATH + "collection1.xml");
        }

    }
}
