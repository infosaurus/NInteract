using System;
using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class ShouldThrowTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void ShouldThrow_Positive()
        {
            A.CallTo(star => star.BackAches())
             .Assuming(assistant => assistant.GetMasseurNextAvailability(Any<int>())).Returns(DateTime.Now.AddDays(3))
             .ShouldThrow<OutOfControlException>();
        }

        [Test]
        [ExpectedException(typeof(DidntThrowException))]
        public void ShouldThrow_Negative()
        {
            A.CallTo(star => star.BackAches())
             .Assuming(assistant => assistant.GetMasseurNextAvailability(Any<int>())).Returns(DateTime.Now.AddDays(1))
             .ShouldThrow<OutOfControlException>();
        }
    }
}
