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
    public class UtPrerequisite_CrossRole
    {
        Mocks.Mock_Prereq_DirectionalEthics_Max maxEthics;

        [TestInitialize]
        public void TestInitialize()
        {
            EthicsScale givenMax = EthicsScale.Exploit;
            Role role1 = new Role("r1");
            Role role2 = new Role("r2");
            maxEthics = new Mocks.Mock_Prereq_DirectionalEthics_Max(givenMax, role1, role2);

            Assert.AreEqual(maxEthics.GetBenchmark_AtoB(), givenMax);
            Assert.IsNotNull(maxEthics.GetRoleA());
            Assert.IsNotNull(maxEthics.GetRoleB());
        }

        [TestMethod]
        public void PassesBenchmark_IsTrue() //givenValue <= max in prereq 
        {
            var givenValue = EthicsScale.Exploit;
            Assert.IsTrue(maxEthics.PublicPassesBenchmark(givenValue));

            givenValue = EthicsScale.Beat;
            Assert.IsTrue(maxEthics.PublicPassesBenchmark(givenValue));
        }

        [TestMethod]
        public void PassesBenchmark_IsFalse() //givenValue not <= max in prereq 
        {
            var givenValue = EthicsScale.Embrace;
            Assert.IsFalse(maxEthics.PublicPassesBenchmark(givenValue));
        }

        [TestMethod]
        public void HasDirectionalRelationThatPassesBenchmark_IsTrue()
        {
            var one = new Character(1, "one");
            one.BaseMorality = Morality.Exploit;
            var two = new Character(2, "two");
            one.CreateRelationshipWith(two);
            var theRelation = one.AllRelations.First();

            Assert.IsTrue(theRelation.Ethics <= maxEthics.GetBenchmark_AtoB());
            Assert.IsTrue(maxEthics.PublicHasDirectionalRelationThatPassesBenchmark(one, two));
        }

        [TestMethod]
        public void HasDirectionalRelationThatPassesBenchmark_WithNoRelation_IsFalse()
        {
            var one = new Character(1, "one");
            var two = new Character(2, "two");

            Assert.IsFalse(maxEthics.PublicHasDirectionalRelationThatPassesBenchmark(one, two));
        }

        [TestMethod]
        public void HasDirectionalRelationThatPassesBenchmark_BeyondLimit_IsFalse()
        {
            var one = new Character(1, "one");
            one.BaseMorality = Morality.Forgive;
            var two = new Character(2, "two");
            one.CreateRelationshipWith(two);
            var theRelation = one.AllRelations.First();

            Assert.IsTrue(theRelation.Ethics > maxEthics.GetBenchmark_AtoB());
            Assert.IsFalse(maxEthics.PublicHasDirectionalRelationThatPassesBenchmark(one, two));
        }

        [TestMethod]
        public void AreRoleMinMaxCountsMet_IsTrue()
        {
            var theRole1 = maxEthics.GetRoleA();
            theRole1.MaxCount = 4;
            theRole1.MinCount = 2;
            theRole1.Participants.Add(new Character(1, "c1"));
            theRole1.Participants.Add(new Character(2, "c2"));
            theRole1.Participants.Add(new Character(3, "c3"));

            var theRole2 = maxEthics.GetRoleB();
            theRole2.MaxCount = 1;
            theRole2.MinCount = 1;
            theRole2.Participants.Add(new Character(4, "c4"));

            Assert.IsTrue(maxEthics.PublicAreRoleMinMaxCountsMet());
        }

        [TestMethod]
        public void AreRoleMinMaxCountsMet_IsFalse()
        {
            var theRole1 = maxEthics.GetRoleA();
            theRole1.MaxCount = 1;
            theRole1.MinCount = 3;

            Assert.IsFalse(maxEthics.PublicAreRoleMinMaxCountsMet());
        }

        [TestMethod]
        public void IsMetByCurrentParticipants_IsTrue()
        {
            var one = new Character(1, "one");
            var two = new Character(2, "two");
            var theRole1 = maxEthics.GetRoleA();
            var theRole2 = maxEthics.GetRoleB();
            theRole1.Participants.Add(one);
            theRole2.Participants.Add(two);

            one.BaseMorality = Morality.Exploit;
            one.CreateRelationshipWith(two);
            var theRelation = one.AllRelations.First();

            Assert.IsTrue(theRelation.Ethics <= maxEthics.GetBenchmark_AtoB());
            Assert.IsTrue(maxEthics.IsMetByCurrentParticipants());
        }

        [TestMethod]
        public void IsMetByCurrentParticipants_IsFalse()
        {
            var one = new Character(1, "one");
            var two = new Character(2, "two");
            var theRole1 = maxEthics.GetRoleA();
            var theRole2 = maxEthics.GetRoleB();
            theRole1.Participants.Add(one);
            theRole2.Participants.Add(two);

            one.BaseMorality = Morality.Forgive;
            one.CreateRelationshipWith(two);
            var theRelation = one.AllRelations.First();

            Assert.IsTrue(theRelation.Ethics > maxEthics.GetBenchmark_AtoB());
            Assert.IsFalse(maxEthics.IsMetByCurrentParticipants());
        }

        [TestMethod]
        public void TryToFulfillFromScratch_Succeeds()
        {
            var role1 = maxEthics.GetRoleA();
            var role2 = maxEthics.GetRoleB();
            role1.MinCount = 1;
            role2.MinCount = 1;

            var currentCast = new SocietySnapshot();

            for (int i = 0; i < 5; i++)
                currentCast.AllCharacters.Add(new Character(i, "name" + i));

            foreach (Character c in currentCast.AllCharacters)
            {
                c.BaseMorality = Morality.Exploit;

                for (int i = 0; i < 5; i++)
                    if (c.Id != i)
                        c.CreateRelationshipWith(currentCast.AllCharacters[i]);
            }

            Assert.IsTrue(maxEthics.TryToFulfillFromScratch(currentCast, null));
            Assert.IsTrue(role1.Participants.Count > 0);
            Assert.IsTrue(role2.Participants.Count > 0);
        }

        [TestMethod]
        public void TryToFulfillFromScratch_Fails()
        {
            var role1 = maxEthics.GetRoleA();
            var role2 = maxEthics.GetRoleB();
            role1.MinCount = 1;
            role2.MinCount = 1;

            var currentCast = new SocietySnapshot();

            for (int i = 0; i < 5; i++)
                currentCast.AllCharacters.Add(new Character(i, "name" + i));

            foreach (Character c in currentCast.AllCharacters)
            {
                c.BaseMorality = Morality.Forgive;

                for (int i = 0; i < 5; i++)
                    if (c.Id != i)
                        c.CreateRelationshipWith(currentCast.AllCharacters[i]);
            }

            Assert.IsFalse(maxEthics.TryToFulfillFromScratch(currentCast, null));
        }
    }
}
