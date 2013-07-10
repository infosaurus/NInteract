using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class ShouldTellTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void ShouldTell_NoArguments_Positive()
        {
            A.CallTo(star => star.Thirsty())
             .ShouldTell(assistant => assistant.PrepareADrink());
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ShouldTell_NoArguments_Negative()
        {
            A.CallTo(star => star.Thirsty())
             .ShouldTell(assistant => assistant.CallACab());
        }


        [Test]
        public void ShouldTell_OneIntArgument_Positive()
        {
            A.CallTo(star => star.Hungry())
             .ShouldTell(assistant => assistant.BuySandwiches(3));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ShouldTell_OneIntArgument_Negative()
        {
            A.CallTo(star => star.Hungry())
             .ShouldTell(assistant => assistant.BuySandwiches(4));
        }


        [Test]
        public void ShouldTell_OneNullArgument_Positive()
        {
            A.CallTo(star => star.Tired())
             .ShouldTell(assistant => assistant.GetVegasPlaneSchedule(null));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ShouldTell_OneNullArgument_Negative()
        {
            A.CallTo(star => star.Hungry())
             .ShouldTell(assistant => assistant.GetVegasPlaneSchedule(null));
        }

        [Test]
        public void ShouldTell_MultipleArguments_Positive()
        {
            A.CallTo(star => star.WokeUp())
             .ShouldTell(assistant => assistant.MakeCoffee(Expresso.Instance, 2, false));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ShouldTell_MultipleArguments_Negative()
        {
            A.CallTo(star => star.WokeUp())
             .ShouldTell(assistant => assistant.MakeCoffee(Expresso.Instance, 2, true));
        }
    }
}
