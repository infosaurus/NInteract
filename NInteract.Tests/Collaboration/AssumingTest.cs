using System;
using NUnit.Framework;
using Ninteract.Engine.Exceptions;
using Ninteract.Tests.Contract;

namespace Ninteract.Tests.Collaboration
{
    public class AssumingTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void AssumingReturns_Property_Positive()
        {
            A.CallTo(star => star.SignAutographs(3))
             .Assuming(assistant => assistant.Pen).Returns(null)
             .ShouldTell(assistant => assistant.PrintAutographs(3));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingReturns_Property_Negative()
        {
            A.CallTo(star => star.SignAutographs(3))
             .Assuming(assistant => assistant.Pen).Returns(new Object())
             .ShouldTell(assistant => assistant.PrintAutographs(3));
        }

        [Test]
        public void AssumingReturns_Method_Positive()
        {
            A.CallTo(star => star.Bored())
             .Assuming(assistant => assistant.GiveNextAppointment()).Returns(IAssistantContract.NoAppointment())
             .ShouldTell(assistant => assistant.GetMasseurNextAvailability(Any<int>()));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingReturns_Method_Negative()
        {
            A.CallTo(star => star.Bored())
             .Assuming(assistant => assistant.GiveNextAppointment()).Returns(null)
             .ShouldTell(assistant => assistant.BuySandwiches(3));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingReturns_UnrelatedMethod_Negative()
        {
            A.CallTo(star => star.Bored())
             .Assuming(assistant => assistant.GetVegasPlaneSchedule(Any<object>())).Returns(null)
             .ShouldTell(assistant => assistant.BuySandwiches(3));
        }

        [Test]
        public void AssumingThrows_Action_Positive()
        {
            A.CallTo(star => star.PayBill(Some<Bill>()))
             .Assuming(assistant => assistant.PayBill(Any<Bill>())).Throws<OutOfCashException>()
             .ShouldTell(assistant => assistant.WithdrawCash(500));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingThrows_Action_Negative()
        {
            A.CallTo(star => star.PayBill(Some<Bill>()))
             .Assuming(assistant => assistant.PayBill(Any<Bill>())).Throws<OutOfCashException>()
             .ShouldTell(assistant => assistant.MakeCoffee(Any<Coffee>(), Any<int>(), Any<bool>()));
        }

        [Test]
        public void AssumingThrows_Function_Positive()
        {
            A.CallTo(star => star.Hungry())
             .Assuming(assistant => assistant.BuySandwiches(Any<int>())).Throws<OutOfCashException>()
             .ShouldTell(assistant => assistant.WithdrawCash(50));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingThrows_Function_Negative()
        {
            A.CallTo(star => star.Hungry())
             .Assuming(assistant => assistant.BuySandwiches(Any<int>())).Throws<OutOfCashException>()
             .ShouldTell(assistant => assistant.PrepareADrink());
        }

        [Test]
        public void AssumingThrows_Getter_Positive()
        {
            A.CallTo(star => star.SignAutographs(Some<int>()))
             .Assuming(assistant => assistant.Pen).Throws<OutOfInkException>()
             .ShouldTell(assistant => assistant.BuyPen());
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingThrows_Getter_Negative()
        {
            A.CallTo(star => star.SignAutographs(Some<int>()))
             .Assuming(assistant => assistant.Pen).Throws<OutOfInkException>()
             .ShouldTell(assistant => assistant.CallACab());
        }

        [Test]
        public void AssumingThrows_Setter_Positive()
        {
            A.CallTo(star => star.Hungry())
             .AssumingSet(assistant => assistant.IsHungry = false).Throws<StillHungryException>()
             .ShouldTell(assistant => assistant.BuySandwiches(1));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingThrows_Setter_Negative()
        {
            A.CallTo(star => star.Hungry())
             .AssumingSet(assistant => assistant.IsHungry = false).Throws<StillHungryException>()
             .ShouldTell(assistant => assistant.BuySandwiches(6));
        }
    }
}
