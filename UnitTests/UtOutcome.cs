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
    public class UtOutcome
    {
        Outcome_ChangeTrust theOutcome;

        [TestInitialize]
        public void TestInitialize()
        {
            int givenMagnitude = 1;
            var role1 = new Role();
            var role2 = new Role();
            theOutcome = new Outcome_ChangeTrust(1, role1, role2);

            Assert.AreEqual(givenMagnitude, theOutcome.Magnitude);
            Assert.IsNotNull(theOutcome.BeingChanged);
            Assert.AreEqual(role1, theOutcome.BeingChanged);
            Assert.IsNotNull(theOutcome.Towards);
        }

        [TestMethod]
        public void WhenExecuted_ThenTrustIsChanged()
        {
            throw new NotImplementedException();
        }
        
    }
}
