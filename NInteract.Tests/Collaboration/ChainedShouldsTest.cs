using NInteract;
using NInteract.Tests.Collaboration;
using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class ChainedShouldsTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ChainedShouldTells_First_Negative()
        {
            A.CallTo(star => star.Thirsty())
             .ShouldTell(assistant => assistant.CallACab())
             .And()
             .ShouldTell(assistant => assistant.PrepareADrink());
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ChainedShouldTells_Second_Negative()
        {
            A.CallTo(star => star.Thirsty())
             .ShouldTell(assistant => assistant.PrepareADrink())
             .And()
             .ShouldTell(assistant => assistant.CallACab());
        }

        [Test]
        public void ChainedShouldTells_Positive()
        {
            A.CallTo(star => star.Thirsty())
             .ShouldTell(assistant => assistant.PrepareADrink())
             .And()
             .ShouldTell(assistant => assistant.ServeDrink());
        }

        [Test]
        [ExpectedException(typeof(DidntAskException))]
        public void ChainedShoulds_Tell_Ask_Negative()
        {
            A.CallTo(star => star.Thirsty())
             .ShouldTell(assistant => assistant.PrepareADrink())
             .And()
             .ShouldAsk(assistant => assistant.GiveCreditCard());
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ChainedShoulds_Ask_Tell_Negative()
        {
            A.CallTo(star => star.BackAches())
             .ShouldAsk(assistant => assistant.GetMasseurNextAvailability(2))
             .And()
             .ShouldTell(assistant => assistant.PrepareADrink());
        }

        [Test]
        public void ChainedShoulds_Ask_Tell_Positive()
        {
            A.CallTo(star => star.NeedsSomeFun())
             .ShouldAsk(assistant => assistant.GetVegasPlaneSchedule(null))
             .And()
             .ShouldTell(assistant => assistant.BookPlaneToVegas());
        }
    }
}
