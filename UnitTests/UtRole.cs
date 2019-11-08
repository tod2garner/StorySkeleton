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
            theRole = new Role();

            Assert.IsNotNull(theRole.Participants);
            Assert.IsNull(theRole.MinCount);
            Assert.IsNull(theRole.MaxCount);
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
    }
}
