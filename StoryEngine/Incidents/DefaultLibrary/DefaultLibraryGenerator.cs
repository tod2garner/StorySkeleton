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

        public static void GenerateFilesForDefaultLibrary()
        {
            var defaultCollection = new CollectionOfIncidentTemplates();

            defaultCollection.TheTemplates.Add(CreateTemplateManually.AccidentalOffense());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Agression_Social());
            defaultCollection.TheTemplates.Add(CreateTemplateManually.Cooperation_Social());

            defaultCollection.SaveToXML(SAVE_FILE_PATH + "collection1.xml");
        }

    }
}
