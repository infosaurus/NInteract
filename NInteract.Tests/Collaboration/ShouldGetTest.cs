using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class ShouldGetTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void ShouldGet_Positive()
        {
            A.CallTo(star => star.Hungry())
             .ShouldGet(assistant => assistant.IsHungry);
        }

        [Test]
        [ExpectedException(typeof(DidntGetException))]
        public void ShouldGet_Negative()
        {
            A.CallTo(star => star.NeedsSomeFun())
             .ShouldGet(assistant => assistant.Pen);
        }
    }
}
