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
    public class UtPrerequisite_OneRole
    {
        Mocks.Mock_Prereq_MutualTrust_Min minMutualTrust;

        [TestInitialize]
        public void TestInitialize()
        {
            EthicsScale givenMin = EthicsScale.Coexist;
            Role theRole = new Role("r");
            minMutualTrust = new Mocks.Mock_Prereq_MutualTrust_Min(givenMin, theRole);

            Assert.AreEqual(minMutualTrust.BenchmarkTrust, givenMin);
            Assert.IsNotNull(minMutualTrust.Role);
        }

        [TestMethod]
        public void Copy_OneRolePrereq()
        {
            var altRole1 = new Role("alt1");
            var altRole2 = new Role("r");
            var replacementList = new List<Role>(2) { altRole1, altRole2 };

            var theCopy = minMutualTrust.Copy(replacementList) as MutualTrust_Min;

            Assert.AreEqual(minMutualTrust.BenchmarkTrust, theCopy.BenchmarkTrust);
            Assert.AreNotEqual(minMutualTrust.Role, theCopy.Role);
            Assert.AreEqual(altRole2, theCopy.Role);
        }

        [TestMethod]
        public void PassesBenchmark_IsTrue() //givenValue >= min in prereq 
        {
            var givenValue = EthicsScale.Coexist;
            Assert.IsTrue(minMutualTrust.PublicPassesBenchmark(givenValue));

            givenValue = EthicsScale.Befriend;
            Assert.IsTrue(minMutualTrust.PublicPassesBenchmark(givenValue));
        }

        [TestMethod]
        public void PassesBenchmark_IsFalse() //givenValue not >= min in prereq 
        {
            var givenValue = EthicsScale.Exploit;
            Assert.IsFalse(minMutualTrust.PublicPassesBenchmark(givenValue));
        }

        [TestMethod]
        public void HaveMutualTrustThatPassesBenchmark_IsTrue()
        {
            var one = new Character(1, "one");
            one.BaseSuspicion = SuspicionScale.Relaxed;
            var two = new Character(2, "two");
            two.BaseSuspicion = SuspicionScale.Relaxed;

            one.CreateRelationshipWith(two, null);
            var theRelation1 = one.AllRelations.First();
            two.CreateRelationshipWith(one, null);
            var theRelation2 = two.AllRelations.First();

            Assert.IsTrue(theRelation1.Trust >= minMutualTrust.BenchmarkTrust);
            Assert.IsTrue(theRelation2.Trust >= minMutualTrust.BenchmarkTrust);
            Assert.IsTrue(minMutualTrust.PublicHaveMutualTrustThatPassesBenchmark(one, two));
        }

        [TestMethod]
        public void HaveMutualTrustThatPassesBenchmark_BeyondLimit_IsFalse()
        {
            var one = new Character(1, "one");
            one.BaseSuspicion = SuspicionScale.Paranoid;
            var two = new Character(2, "two");
            two.BaseSuspicion = SuspicionScale.Paranoid;

            one.CreateRelationshipWith(two, null);
            var theRelation1 = one.AllRelations.First();
            two.CreateRelationshipWith(one, null);
            var theRelation2 = two.AllRelations.First();

            Assert.IsTrue(theRelation1.Trust < minMutualTrust.BenchmarkTrust);
            Assert.IsTrue(theRelation2.Trust < minMutualTrust.BenchmarkTrust);
            Assert.IsFalse(minMutualTrust.PublicHaveMutualTrustThatPassesBenchmark(one, two));
        }

        [TestMethod]
        public void HaveMutualTrustThatPassesBenchmark_NoRelation_IsFalse()
        {
            var one = new Character(1, "one");
            var two = new Character(2, "two");

            Assert.IsFalse(minMutualTrust.PublicHaveMutualTrustThatPassesBenchmark(one, two));
        }

        [TestMethod]
        public void HaveMutualTrustThatPassesBenchmark_NotMutual_IsFalse()
        {
            var one = new Character(1, "one");
            one.BaseSuspicion = SuspicionScale.Relaxed;
            var two = new Character(2, "two");
            two.BaseSuspicion = SuspicionScale.Paranoid;

            one.CreateRelationshipWith(two, null);
            var theRelation1 = one.AllRelations.First();
            two.CreateRelationshipWith(one, null);
            var theRelation2 = two.AllRelations.First();

            Assert.IsTrue(theRelation1.Trust >= minMutualTrust.BenchmarkTrust);
            Assert.IsTrue(theRelation2.Trust < minMutualTrust.BenchmarkTrust);
            Assert.IsFalse(minMutualTrust.PublicHaveMutualTrustThatPassesBenchmark(one, two));
        }

        [TestMethod]
        public void AreRoleMinMaxCountsMet_WithNoMinOrMax_IsTrue()
        {
            Assert.IsTrue(minMutualTrust.PublicAreRoleMinMaxCountsMet());
        }

        [TestMethod]
        public void AreRoleMinMaxCountsMet_IsTrue()
        {
            var theRole = minMutualTrust.Role;
            theRole.MaxCount = 4;
            theRole.MinCount = 2;
            theRole.Participants.Add(new Character(1, "c1"));
            theRole.Participants.Add(new Character(2, "c2"));
            theRole.Participants.Add(new Character(3, "c3"));

            Assert.IsTrue(minMutualTrust.PublicAreRoleMinMaxCountsMet());
        }

        [TestMethod]
        public void AreRoleMinMaxCountsMet_WhenBeyonMax_IsFalse()
        {
            var theRole = minMutualTrust.Role;
            theRole.MaxCount = 2;
            theRole.MinCount = 1;
            theRole.Participants.Add(new Character(1, "c1"));
            theRole.Participants.Add(new Character(2, "c2"));
            theRole.Participants.Add(new Character(3, "c3"));

            Assert.IsFalse(minMutualTrust.PublicAreRoleMinMaxCountsMet());
        }

        [TestMethod]
        public void AreRoleMinMaxCountsMet_WhenBelowMin_IsFalse()
        {
            var theRole = minMutualTrust.Role;
            theRole.MaxCount = 4;
            theRole.MinCount = 2;
            theRole.Participants.Add(new Character(1, "c1"));

            Assert.IsFalse(minMutualTrust.PublicAreRoleMinMaxCountsMet());
        }

        [TestMethod]
        public void AreRoleMinMaxCountsMet_IsFalse()
        {
            var theRole = minMutualTrust.Role;
            theRole.MaxCount = 1;
            theRole.MinCount = 3;

            Assert.IsFalse(minMutualTrust.PublicAreRoleMinMaxCountsMet());
        }

        [TestMethod]
        public void IsMetByCurrentParticipants_IsTrue()
        {
            var one = new Character(1, "one");
            var two = new Character(2, "two");
            var theRole = minMutualTrust.Role;
            theRole.Participants.Add(one);
            theRole.Participants.Add(two);

            one.BaseSuspicion = SuspicionScale.Relaxed;
            two.BaseSuspicion = SuspicionScale.Relaxed;
            one.CreateRelationshipWith(two, null);
            two.CreateRelationshipWith(one, null);
            var theRelation1 = one.AllRelations.First();
            var theRelation2 = two.AllRelations.First();

            Assert.IsTrue(theRelation1.Trust >= minMutualTrust.BenchmarkTrust);
            Assert.IsTrue(theRelation2.Trust >= minMutualTrust.BenchmarkTrust);
            Assert.IsTrue(minMutualTrust.IsMetByCurrentParticipants());
        }

        [TestMethod]
        public void IsMetByCurrentParticipants_IsFalse()
        {
            var one = new Character(1, "one");
            var two = new Character(2, "two");
            var theRole = minMutualTrust.Role;
            theRole.Participants.Add(one);
            theRole.Participants.Add(two);

            one.BaseSuspicion = SuspicionScale.Paranoid;
            two.BaseSuspicion = SuspicionScale.Relaxed;
            one.CreateRelationshipWith(two, null);
            two.CreateRelationshipWith(one, null);
            var theRelation1 = one.AllRelations.First();
            var theRelation2 = two.AllRelations.First();

            Assert.IsTrue(theRelation1.Trust < minMutualTrust.BenchmarkTrust);
            Assert.IsTrue(theRelation2.Trust >= minMutualTrust.BenchmarkTrust);
            Assert.IsFalse(minMutualTrust.IsMetByCurrentParticipants());
        }
        
    }
}
