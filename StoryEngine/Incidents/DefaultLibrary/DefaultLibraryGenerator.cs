using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.Incidents.DefaultLibrary
{
    public static class DefaultLibraryGenerator
    {
        private static string save_file_path = Path.Combine(Environment.CurrentDirectory, @"IncidentLibraries\DefaultLibrary\");

        public static LibraryOfIncidents LoadDefaultLibraryFromFile()
        {
            if(false == Directory.Exists(save_file_path)) //if folder does not exist, create it and save files manually
            {
                Directory.CreateDirectory(save_file_path);
                GenerateFilesForDefaultLibrary();
            }

            var collectionNames = new List<string>();
            collectionNames.Add("CharacterDevelopment.xml");
            collectionNames.Add("Generic.xml");
            collectionNames.Add("Action.xml");
            collectionNames.Add("Survival.xml");

            var theLibrary = new LibraryOfIncidents();

            foreach(string name in collectionNames)
            {
                var path = save_file_path + name;
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
            characterDevelopment.TheTemplates.Add(CreateTemplateManually.Rescue_Social());

            characterDevelopment.SaveToXML(save_file_path + "CharacterDevelopment.xml");

            generic.TheTemplates.Add(CreateTemplateManually.RoutineTask());
            generic.TheTemplates.Add(CreateTemplateManually.SelfImprovement());
            generic.TheTemplates.Add(CreateTemplateManually.Training());
            generic.TheTemplates.Add(CreateTemplateManually.Travel());
            generic.TheTemplates.Add(CreateTemplateManually.SocialGathering());
            generic.TheTemplates.Add(CreateTemplateManually.Message_Received());
            generic.TheTemplates.Add(CreateTemplateManually.Message_Sent());
            generic.TheTemplates.Add(CreateTemplateManually.Message_Lost());
            generic.TheTemplates.Add(CreateTemplateManually.AcquireTool());
            generic.TheTemplates.Add(CreateTemplateManually.EquipmentFailure());
            generic.TheTemplates.Add(CreateTemplateManually.Injury_Accidental());
            generic.TheTemplates.Add(CreateTemplateManually.RestAndRecover());
            generic.TheTemplates.Add(CreateTemplateManually.IndustrialDisaster());
            generic.TheTemplates.Add(CreateTemplateManually.Luck_Bad());
            generic.TheTemplates.Add(CreateTemplateManually.Luck_Good());
            generic.TheTemplates.Add(CreateTemplateManually.OrganizedCompetition());

            generic.SaveToXML(save_file_path + "Generic.xml");
            
            action.TheTemplates.Add(CreateTemplateManually.Aggression_Violent());
            action.TheTemplates.Add(CreateTemplateManually.Aggression_Murderous());
            action.TheTemplates.Add(CreateTemplateManually.Persuit_NonViolent());
            action.TheTemplates.Add(CreateTemplateManually.Persuit_Violent());
            action.TheTemplates.Add(CreateTemplateManually.Hide());
            action.TheTemplates.Add(CreateTemplateManually.CriminalAction_NonViolent());
            action.TheTemplates.Add(CreateTemplateManually.Betrayal_Violent());
            action.TheTemplates.Add(CreateTemplateManually.Trapped());
            action.TheTemplates.Add(CreateTemplateManually.Surrender());
            action.TheTemplates.Add(CreateTemplateManually.Sabotage());
            action.TheTemplates.Add(CreateTemplateManually.Rescue_Violent());

            action.SaveToXML(save_file_path + "Action.xml");

            survival.TheTemplates.Add(CreateTemplateManually.Lost());
            survival.TheTemplates.Add(CreateTemplateManually.NaturalDisaster());
            survival.TheTemplates.Add(CreateTemplateManually.Disease());
            survival.TheTemplates.Add(CreateTemplateManually.Weather_Challenging());
            survival.TheTemplates.Add(CreateTemplateManually.DangerousAnimal());
            survival.TheTemplates.Add(CreateTemplateManually.Survival());

            survival.SaveToXML(save_file_path + "Survival.xml");
        }

    }
}
