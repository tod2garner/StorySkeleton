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
            theIncident = new Incident();

            Assert.IsNotNull(theIncident.AllPossibleOutcomes);
            Assert.IsNotNull(theIncident.MyPrerequisites);
            Assert.IsNotNull(theIncident.AllParticipants);
        }

        private void Setup_Basic_Prerequisites()
        {
            var role1 = new Role();
            role1.MinCount = 2;
            var role2 = new Role();
            role2.MinCount = 1;
            role2.MaxCount = 3;

            theIncident.AllParticipants.Add(role1);
            theIncident.AllParticipants.Add(role2);
            theIncident.MyPrerequisites.Add(new DirectionalEthics_Max(role1, role2, EthicsScale.Exploit));
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
        public void CanAllPrerequisitesBeMet_IsTrue()
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

            Assert.IsTrue(theIncident.TryToFulfillAllPrerequisites(currentCast));
        }

        [TestMethod]
        public void CanAllPrerequisitesBeMet_WithNoPrerequisites_IsTrue()
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

            Assert.IsTrue(theIncident.TryToFulfillAllPrerequisites(currentCast));
        }

        [TestMethod]
        public void CanAllPrerequisitesBeMet_IsFalse_SinglePrereqFails()
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

            Assert.IsFalse(theIncident.TryToFulfillAllPrerequisites(currentCast));
        }

        [TestMethod]
        public void CanAllPrerequisitesBeMet_IsFalse_BothFail()
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

            Assert.IsFalse(theIncident.TryToFulfillAllPrerequisites(currentCast));
        }

        [TestMethod]
        public void AddParticipantsRandomly_WhenMaxIsZero_NoneAreAdded()
        {
            var theRole = new Role();
            theRole.MaxCount = 0;
            var theList = new List<Character>();

            AIncident.AddParticipantsRandomly(theRole, theList);

            Assert.IsTrue(theRole.Participants.Count <= 0);
        }

        [TestMethod]
        public void AddParticipantsRandomly_WhenMaxIsNull_DefaultMaxApplies()
        {
            var defaultMax = Role.DEFAULT_ROLE_MAX_COUNT;
            var givenMin = 3;
            var theRole = new Role();
            theRole.MinCount = givenMin;

            var theList = new List<Character>(defaultMax * 2);
            for (int i = 0; i < defaultMax * 2; i++)
                theList.Add(new Character(i, "name" + i));

            AIncident.AddParticipantsRandomly(theRole, theList);

            Assert.IsTrue(theRole.Participants.Count >= givenMin);
            Assert.IsTrue(theRole.Participants.Count <= defaultMax);
        }

        [TestMethod]
        public void AddParticipantsRandomly_WhenMinIsNull_NoError()
        {
            var givenMax = 5;
            var theRole = new Role();
            theRole.MaxCount = givenMax;

            Assert.IsNull(theRole.MinCount);

            var theList = new List<Character>(givenMax * 2);
            for (int i = 0; i < givenMax * 2; i++)
                theList.Add(new Character(i, "name" + i));

            AIncident.AddParticipantsRandomly(theRole, theList);

            Assert.IsTrue(theRole.Participants.Count <= givenMax);
        }

        [TestMethod]
        public void AddParticipantsRandomly_ResultIsWithinMinMaxRange()
        {
            var givenMax = 5;
            var givenMin = 3;
            var theRole = new Role();
            theRole.MaxCount = givenMax;
            theRole.MinCount = givenMin;

            var theList = new List<Character>(givenMax * 2);
            for (int i = 0; i < givenMax * 2; i++)
                theList.Add(new Character(i, "name" + i));

            AIncident.AddParticipantsRandomly(theRole, theList);

            Assert.IsTrue(theRole.Participants.Count >= givenMin);
            Assert.IsTrue(theRole.Participants.Count <= givenMax);
        }

        [TestMethod]
        public void RollDiceAndExecuteOneOutcome_IsSuccessful()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PopulateAllRolesRandomly_IsSuccessful()
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
