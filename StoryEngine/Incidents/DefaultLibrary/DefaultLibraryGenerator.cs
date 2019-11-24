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
            //#TODO - fix with relative references

            var collectionNames = new List<string>();
            collectionNames.Add("CharacterDevelopment.xml");
            collectionNames.Add("Generic.xml");
            collectionNames.Add("Action.xml");
            collectionNames.Add("Survival.xml");

            var theLibrary = new LibraryOfIncidents();

            foreach(string name in collectionNames)
            {
                var path = SAVE_FILE_PATH + name;
                var theCollection = SerializeXML.LoadFromXML<CollectionOfIncidentTemplates>(path);
                theLibrary.AllCollections.Add(theCollection);
            }
            
            return theLibrary;
        }

        public static void GenerateFilesForDefaultLibrary()
        {
            //#TODO - align emotional tone with outcomes

            var characterDevelopment = new CollectionOfIncidentTemplates(30);
            var generic = new CollectionOfIncidentTemplates(40);
            var action = new CollectionOfIncidentTemplates(20);
            var survival = new CollectionOfIncidentTemplates(10);

            characterDevelopment.TheTemplates.Add(CreateTemplateManually.AccidentalEmbarrassment());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.AccidentalOffense());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Aggression_Social());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Argument_Personal());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Betrayal_Emotional());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Betrayal_Social());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Conversation_Personal());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Cooperation_Social());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Cooperation_Utilitarian());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Deception());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.ImpulsiveDecision());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Internal_Realization());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Internal_Struggle());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Rejection_Emotional());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Rejection_Social());
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.SacrificeForOther());

            characterDevelopment.SaveToXML(SAVE_FILE_PATH + "CharacterDevelopment.xml");            

            generic.TheTemplates.Add(CreateTemplateManually.SelfImprovement());
            generic.TheTemplates.Add(CreateTemplateManually.Training());
            generic.TheTemplates.Add(CreateTemplateManually.Travel());
            generic.TheTemplates.Add(CreateTemplateManually.SocialGathering());
            generic.TheTemplates.Add(CreateTemplateManually.Message_Received());
            generic.TheTemplates.Add(CreateTemplateManually.Message_Lost());
            generic.TheTemplates.Add(CreateTemplateManually.AcquireTool());
            generic.TheTemplates.Add(CreateTemplateManually.EquipmentFailure());
            generic.TheTemplates.Add(CreateTemplateManually.Injury_Accidental());
            generic.TheTemplates.Add(CreateTemplateManually.RestAndRecover());
            generic.TheTemplates.Add(CreateTemplateManually.IndustrialDisaster());
            generic.TheTemplates.Add(CreateTemplateManually.Luck_Bad());
            generic.TheTemplates.Add(CreateTemplateManually.Luck_Good());
            generic.TheTemplates.Add(CreateTemplateManually.OrganizedCompetition());

            generic.SaveToXML(SAVE_FILE_PATH + "Generic.xml");
            
            action.TheTemplates.Add(CreateTemplateManually.Aggression_Violent());
            action.TheTemplates.Add(CreateTemplateManually.Aggression_Murderous());
            action.TheTemplates.Add(CreateTemplateManually.Persuit_NonViolent());
            action.TheTemplates.Add(CreateTemplateManually.Persuit_Violent());
            action.TheTemplates.Add(CreateTemplateManually.Hide());

            action.SaveToXML(SAVE_FILE_PATH + "Action.xml");

            survival.TheTemplates.Add(CreateTemplateManually.Lost());
            survival.TheTemplates.Add(CreateTemplateManually.NaturalDisaster());
            survival.TheTemplates.Add(CreateTemplateManually.Disease());
            survival.TheTemplates.Add(CreateTemplateManually.Weather_Challenging());
            survival.TheTemplates.Add(CreateTemplateManually.DangerousAnimal());
            survival.TheTemplates.Add(CreateTemplateManually.Survival());

            survival.SaveToXML(SAVE_FILE_PATH + "Survival.xml");
        }

    }
}
