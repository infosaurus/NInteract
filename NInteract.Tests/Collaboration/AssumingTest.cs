using System;
using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class AssumingTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void AssumingReturns_Positive()
        {
            A.CallTo(star => star.SignAutographs(3))
             .Assuming(assistant => assistant.Pen).Returns(null)
             .ShouldTell(assistant => assistant.PrintAutographs(3));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingReturns_Negative()
        {
            A.CallTo(star => star.SignAutographs(3))
             .Assuming(assistant => assistant.Pen).Returns(new Object())
             .ShouldTell(assistant => assistant.PrintAutographs(3));
        }
    }
}
