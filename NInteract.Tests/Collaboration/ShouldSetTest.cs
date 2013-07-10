using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class ShouldSetTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void ShouldSet_Positive()
        {
            A.CallTo(star => star.Hungry())
             .ShouldSet(assistant => assistant.IsHungry = false);
        }

        [Test]
        [ExpectedException(typeof(DidntSetException))]
        public void ShouldSet_Negative()
        {
            A.CallTo(star => star.Generous())
             .ShouldSet(assistant => assistant.Pen = Some<object>());
        }

        // TODO : correlated expected value (Moq It.Is<>(d => d == assistant.xyz) doesn't seem to work)
    }
}
