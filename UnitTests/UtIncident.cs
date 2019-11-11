using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UtIncident
    {
        Incident theIncident;

        [TestInitialize]
        public void TestInitialize()
        {
            theIncident = new Incident("testIncident");

            Assert.AreEqual("testIncident", theIncident.Name);
            Assert.IsNotNull(theIncident.AllPossibleOutcomes);
            Assert.IsNotNull(theIncident.MyPrerequisites);
            Assert.IsNotNull(theIncident.AllParticipantRoles);
        }

        private void Setup_Basic_Prerequisites()
        {
            var role1 = new Role("r1");
            role1.MinCount = 2;
            var role2 = new Role("r2");
            role2.MinCount = 1;
            role2.MaxCount = 3;

            theIncident.AllParticipantRoles.Add(role1);
            theIncident.AllParticipantRoles.Add(role2);
            theIncident.MyPrerequisites.Add(new DirectionalEthics_Max(EthicsScale.Exploit, role1, role2));
            theIncident.MyPrerequisites.Add(new MutualTrust_Min(EthicsScale.Cooperate, role1));

        }

        [TestMethod]
        public void OutcomeTotal_Is100Percent()
        {
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(12));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(13));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(15));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(25));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(35));

            Assert.AreEqual(100, theIncident.GetTotalOutcomePercentChance());
        }

        [TestMethod]
        public void OutcomeTotal_Is90Percent()
        {
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(10));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(11));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(12));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(23));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(34));

            Assert.AreEqual(90, theIncident.GetTotalOutcomePercentChance());
        }

        [TestMethod]
        public void TryToPopulateIncident_IsSuccessful()
        {
            Setup_Basic_Prerequisites();
            var currentCast = new SocietySnapshot();

            for (int i = 0; i < 10; i++)
                currentCast.AllCharacters.Add(new Character(i, "name" + i));

            foreach (Character c in currentCast.AllCharacters)
            {
                c.BaseMorality = Morality.Exploit;
                c.BaseSuspicion = SuspicionScale.Naive;

                for (int i = 0; i < 10; i++)
                    if (c.Id != i)
                        c.CreateRelationshipWith(currentCast.AllCharacters[i]);
            }

            Assert.IsTrue(theIncident.TryToPopulateIncident(currentCast));
        }

        [TestMethod]
        public void TryToPopulateIncident_WithNoPrerequisites_IsSuccessful()
        {
            var currentCast = new SocietySnapshot();

            for (int i = 0; i < 10; i++)
                currentCast.AllCharacters.Add(new Character(i, "name" + i));

            foreach (Character c in currentCast.AllCharacters)
            {
                for (int i = 0; i < 10; i++)
                    if (c.Id != i)
                        c.CreateRelationshipWith(currentCast.AllCharacters[i]);
            }

            Assert.IsTrue(theIncident.TryToPopulateIncident(currentCast));
        }

        [TestMethod]
        public void TryToPopulateIncident_SinglePrereqFails()
        {
            Setup_Basic_Prerequisites();
            var currentCast = new SocietySnapshot();

            for (int i = 0; i < 10; i++)
                currentCast.AllCharacters.Add(new Character(i, "name" + i));

            foreach (Character c in currentCast.AllCharacters)
            {
                c.BaseMorality = Morality.Exploit;
                c.BaseSuspicion = SuspicionScale.Guarded;

                for (int i = 0; i < 10; i++)
                    if (c.Id != i)
                        c.CreateRelationshipWith(currentCast.AllCharacters[i]);
            }

            Assert.IsFalse(theIncident.TryToPopulateIncident(currentCast));
        }

        [TestMethod]
        public void TryToPopulateIncident_BothPrereqsFail()
        {
            Setup_Basic_Prerequisites();
            var currentCast = new SocietySnapshot();

            for (int i = 0; i < 10; i++)
                currentCast.AllCharacters.Add(new Character(i, "name" + i));

            foreach (Character c in currentCast.AllCharacters)
            {
                c.BaseMorality = Morality.Forgive;
                c.BaseSuspicion = SuspicionScale.Paranoid;

                for (int i = 0; i < 10; i++)
                    if (c.Id != i)
                        c.CreateRelationshipWith(currentCast.AllCharacters[i]);
            }

            Assert.IsFalse(theIncident.TryToPopulateIncident(currentCast));
        }
        
        [TestMethod]
        public void RollDiceAndExecuteOneOutcome_IsSuccessful()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PopulateAllRoles_Randomly_IsSuccessful()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PopulateAllRoles_FollowingPrereqs_IsSuccessful()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetTextSummary_IsSuccessful()
        {
            throw new NotImplementedException();
        }
    }

}
