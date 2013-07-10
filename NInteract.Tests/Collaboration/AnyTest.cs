using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class AnyTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void ShouldTell_Any_Positive()
        {
            A.CallTo(star => star.SignAutographs(Some<int>()))
             .ShouldTell(assistant => assistant.PrintAutographs(Any<int>()));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ShouldTell_Any_Negative()
        {
            A.CallTo(star => star.SignAutographs(Some<int>()))
             .ShouldTell(assistant => assistant.GetVegasPlaneSchedule(Any<object>()));
        }
    }
}
