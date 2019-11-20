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
    public class UtRole
    {
        Role theRole;

        [TestInitialize]
        public void TestInitialize()
        {
            theRole = new Role("test");

            Assert.AreEqual("test", theRole.RoleName);
            Assert.IsNotNull(theRole.Participants);
            Assert.IsNull(theRole.MinCount);
            Assert.IsNull(theRole.MaxCount);
        }

        [TestMethod]
        public void CopyRole_NoParticipants()
        {
            theRole.MinCount = 2;
            theRole.MaxCount = 8;
            theRole.Participants.Add(new Character(0, "one"));
            theRole.Participants.Add(new Character(1, "two"));
            var theCopy = theRole.Copy(false);

            Assert.AreEqual(theRole.RoleName, theCopy.RoleName);
            Assert.AreEqual(theRole.MinCount, theCopy.MinCount);
            Assert.AreEqual(theRole.MaxCount, theCopy.MaxCount);
            Assert.AreNotEqual(theRole.Participants.Count, theCopy.Participants.Count);
        }

        [TestMethod]
        public void CopyRole_WithParticipants()
        {
            theRole.MinCount = 12;
            theRole.MaxCount = null;
            theRole.Participants.Add(new Character(0, "one"));
            var theCopy = theRole.Copy(true);

            Assert.AreEqual(theRole.RoleName, theCopy.RoleName);
            Assert.AreEqual(theRole.MinCount, theCopy.MinCount);
            Assert.AreEqual(theRole.MaxCount, theCopy.MaxCount);
            Assert.AreEqual(theRole.Participants.Count, theCopy.Participants.Count);

            //Participant - not same object, but same properties
            Assert.AreEqual(theRole.Participants.First().Name, theCopy.Participants.First().Name);
            Assert.AreEqual(theRole.Participants.First().Id, theCopy.Participants.First().Id);
            Assert.AreNotEqual(theRole.Participants.First(), theCopy.Participants.First());
        }

        [TestMethod]
        public void AreMinAndMaxMet_NullLimits_ZeroParticipants_IsTrue()
        {
            Assert.IsTrue(theRole.AreMinAndMaxMet());
        }


        [TestMethod]
        public void AreMinAndMaxMet_NullLimits_SomeParticipants_IsTrue()
        {
            theRole.Participants.Add(new Character(0, "one"));
            theRole.Participants.Add(new Character(1, "two"));
            Assert.IsTrue(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AreMinAndMaxMet_MinOf1_IsTrue()
        {
            theRole.MinCount = 1;
            theRole.Participants.Add(new Character(0, "one"));

            Assert.IsTrue(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AreMinAndMaxMet_MinOf2_IsTrue()
        {
            theRole.MinCount = 2;
            theRole.Participants.Add(new Character(0, "one"));
            theRole.Participants.Add(new Character(1, "two"));
            theRole.Participants.Add(new Character(2, "three"));
            theRole.Participants.Add(new Character(3, "four"));

            Assert.IsTrue(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AreMinAndMaxMet_MaxOf1_IsTrue()
        {
            theRole.MaxCount = 1;
            Assert.IsTrue(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AreMinAndMaxMet_MaxOf2_IsTrue()
        {
            theRole.MaxCount = 2;
            theRole.Participants.Add(new Character(0, "one"));

            Assert.IsTrue(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AreMinAndMaxMet_MaxOf3_IsTrue()
        {
            theRole.MaxCount = 3;
            theRole.Participants.Add(new Character(0, "one"));
            theRole.Participants.Add(new Character(1, "two"));
            theRole.Participants.Add(new Character(2, "three"));

            Assert.IsTrue(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AreMinAndMaxMet_MinOf1_IsFalse()
        {
            theRole.MinCount = 1;
            Assert.IsFalse(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AreMinAndMaxMet_MinOf3_IsFalse()
        {
            theRole.MinCount = 3;
            theRole.Participants.Add(new Character(0, "one"));
            theRole.Participants.Add(new Character(1, "two"));

            Assert.IsFalse(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AreMinAndMaxMet_MaxOf0_IsFalse()
        {
            theRole.MaxCount = 0;
            theRole.Participants.Add(new Character(0, "one"));

            Assert.IsFalse(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AreMinAndMaxMet_MaxOf3_IsFalse()
        {
            theRole.MaxCount = 3;
            theRole.Participants.Add(new Character(0, "one"));
            theRole.Participants.Add(new Character(1, "two"));
            theRole.Participants.Add(new Character(2, "three"));
            theRole.Participants.Add(new Character(3, "four"));

            Assert.IsFalse(theRole.AreMinAndMaxMet());
        }

        [TestMethod]
        public void AddParticipantsRandomly_WhenMaxIsBelowMin_ResultIsFalse()
        {
            var theRole = new Role("r");
            theRole.MaxCount = 1;
            theRole.MinCount = 3;
            var theList = new List<Character>();

            var result = theRole.AddParticipantsRandomly(theList, null);

            Assert.IsFalse(result);
            Assert.IsTrue(theRole.Participants.Count <= 0);
        }

        [TestMethod]
        public void AddParticipantsRandomly_WhenMaxIsZero_NoneAreAdded()
        {
            var theRole = new Role("r");
            theRole.MaxCount = 0;
            var theList = new List<Character>();

            var result = theRole.AddParticipantsRandomly(theList, null);

            Assert.IsTrue(result);
            Assert.IsTrue(theRole.Participants.Count <= 0);
        }

        [TestMethod]
        public void AddParticipantsRandomly_WhenMaxIsNull_DefaultMaxApplies()
        {
            var defaultMax = Role.DEFAULT_ROLE_MAX_COUNT;
            var givenMin = 3;
            var theRole = new Role("r");
            theRole.MinCount = givenMin;

            var theList = new List<Character>(defaultMax * 2);
            for (int i = 0; i < defaultMax * 2; i++)
                theList.Add(new Character(i, "name" + i));

            var result = theRole.AddParticipantsRandomly(theList, null);

            Assert.IsTrue(result);
            Assert.IsTrue(theRole.Participants.Count >= givenMin);
            Assert.IsTrue(theRole.Participants.Count <= defaultMax);
        }

        [TestMethod]
        public void AddParticipantsRandomly_WhenMinIsNull_NoError()
        {
            var givenMax = 5;
            var theRole = new Role("r");
            theRole.MaxCount = givenMax;

            Assert.IsNull(theRole.MinCount);

            var theList = new List<Character>(givenMax * 2);
            for (int i = 0; i < givenMax * 2; i++)
                theList.Add(new Character(i, "name" + i));

            var result = theRole.AddParticipantsRandomly(theList, null);

            Assert.IsTrue(result);
            Assert.IsTrue(theRole.Participants.Count <= givenMax);
        }

        [TestMethod]
        public void AddParticipantsRandomly_ResultIsWithinMinMaxRange()
        {
            var givenMax = 5;
            var givenMin = 3;
            var theRole = new Role("r");
            theRole.MaxCount = givenMax;
            theRole.MinCount = givenMin;

            var theList = new List<Character>(givenMax * 2);
            for (int i = 0; i < givenMax * 2; i++)
                theList.Add(new Character(i, "name" + i));

            var result = theRole.AddParticipantsRandomly(theList, null);

            Assert.IsTrue(result);
            Assert.IsTrue(theRole.Participants.Count >= givenMin);
            Assert.IsTrue(theRole.Participants.Count <= givenMax);
        }

        [TestMethod]
        public void AddParticipants_RetestingAfterEach_IsSuccessful()
        {
            throw new NotImplementedException();
        }
    }
}
