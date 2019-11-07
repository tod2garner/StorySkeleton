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
        public void AreMinAndMaxMet_IsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AreMinAndMaxMet_IsFalse()
        {
            throw new NotImplementedException();
        }
    }
}
