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
        public void IsOutcomeTotal100Percent_IsTrue()
        {
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(12));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(13));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(15));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(25));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(35));

            Assert.IsTrue(theIncident.IsOutcomeTotal100Percent());
        }

        [TestMethod]
        public void IsOutcomeTotal100Percent_IsFalse()
        {
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(12));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(13));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(15));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(25));
            theIncident.AllPossibleOutcomes.Add(new PossibleResult(34));

            Assert.IsFalse(theIncident.IsOutcomeTotal100Percent());
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

            Assert.IsTrue(theIncident.CanAllPrerequisitesBeMet(currentCast));
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

            Assert.IsFalse(theIncident.CanAllPrerequisitesBeMet(currentCast));
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

            Assert.IsFalse(theIncident.CanAllPrerequisitesBeMet(currentCast));
        }

    }

}
