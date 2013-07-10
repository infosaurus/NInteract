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
             .ShouldTell(assistant => assistant.CallACab(),
                And)
             .ShouldTell(assistant => assistant.PrepareADrink());
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ChainedShouldTells_Second_Negative()
        {
            A.CallTo(star => star.Thirsty())
             .ShouldTell(assistant => assistant.PrepareADrink(),
                And)
             .ShouldTell(assistant => assistant.CallACab());
        }

        [Test]
        public void ChainedShouldTells_Positive()
        {
            A.CallTo(star => star.Thirsty())
             .ShouldTell(assistant => assistant.PrepareADrink(),
                And)
             .ShouldTell(assistant => assistant.ServeDrink());
        }

        [Test]
        [ExpectedException(typeof(DidntAskException))]
        public void ChainedShoulds_Tell_Ask_Negative()
        {
            A.CallTo(star => star.Thirsty())
             .ShouldTell(assistant => assistant.PrepareADrink(),
                And)
             .ShouldAsk(assistant => assistant.GiveCreditCard());
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ChainedShoulds_Ask_Tell_Negative()
        {
            A.CallTo(star => star.BackAches())
             .ShouldAsk(assistant => assistant.GetMasseurNextAvailability(2),
                And)
             .ShouldTell(assistant => assistant.PrepareADrink());
        }

        [Test]
        public void ChainedShoulds_Ask_Tell_Positive()
        {
            A.CallTo(star => star.NeedsSomeFun())
             .ShouldAsk(assistant => assistant.GetVegasPlaneSchedule(null),
                And)
             .ShouldTell(assistant => assistant.BookPlaneToVegas());
        }

        [Test]
        public void ChainedShoulds_Get_Tell_Positive()
        {
            A.CallTo(star => star.Hungry())
             .ShouldGet(assistant => assistant.IsHungry, 
                And)
             .ShouldTell(assistant => assistant.BuySandwiches(3));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ChainedShoulds_Get_Tell_Negative()
        {
            A.CallTo(star => star.Hungry())
             .ShouldGet(assistant => assistant.IsHungry,
                And)
             .ShouldTell(assistant => assistant.PrintAutographs(Some<int>()));
        }

        [Test]
        public void ChainedShoulds_Set_Tell_Positive()
        {
            A.CallTo(star => star.Hungry())
             .ShouldSet(assistant => assistant.IsHungry = false,
                        And)
             .ShouldTell(assistant => assistant.BuySandwiches(Any<int>()));
        }

        [Test]
        [ExpectedException(typeof(DidntAskException))]
        public void ChainedShoulds_Set_Ask_Negative()
        {
            A.CallTo(star => star.Hungry())
             .ShouldSet(assistant => assistant.IsHungry = false,
                        And)
             .ShouldAsk(assistant => assistant.GetMasseurNextAvailability(Any<int>()));
        }
    }
}
