using System;
using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace NInteract.Tests.Collaboration
{
    public class ShouldAskTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void ShouldAsk_NoArguments_Positive()
        {
            A.CallTo(star => star.Bored())
             .ShouldAsk(assistant => assistant.GiveNextAppointment());
        }

        [Test]
        [ExpectedException(typeof(DidntAskException))]
        public void ShouldAsk_NoArguments_Negative()
        {
            A.CallTo(star => star.Bored())
             .ShouldAsk(assistant => assistant.GiveCreditCard());
        }


        [Test]
        public void ShouldAsk_OneIntArgument_Positive()
        {
            A.CallTo(star => star.BackAches())
             .ShouldAsk(assistant => assistant.GetMasseurNextAvailability(2));
        }

        [Test]
        [ExpectedException(typeof(DidntAskException))]
        public void ShouldAsk_OneIntArgument_Negative()
        {
            A.CallTo(star => star.Hungry())
             .ShouldAsk(assistant => assistant.GetMasseurNextAvailability(3));
        }


        [Test]
        public void ShouldAsk_OneNullArgument_Positive()
        {
            A.CallTo(star => star.NeedsSomeFun())
             .ShouldAsk(assistant => assistant.GetVegasPlaneSchedule(null));
        }

        [Test]
        [ExpectedException(typeof(DidntAskException))]
        public void ShouldAsk_OneNullArgument_Negative()
        {
            A.CallTo(star => star.Hungry())
             .ShouldAsk(assistant => assistant.GetVegasPlaneSchedule(null));
        }


        [Test]
        public void ShouldAsk_MultipleArguments_Positive()
        {
            A.CallTo(star => star.SignContract())
             .ShouldAsk(assistant => assistant.GiveTotalFee(1000m, false, VatInclusiveCalculationPolicy.Instance));
        }

        [Test]
        [ExpectedException(typeof(DidntAskException))]
        public void ShouldAsk_MultipleArguments_Negative()
        {
            A.CallTo(star => star.SignContract())
             .ShouldAsk(assistant => assistant.GiveTotalFee(1000m, false, null));
        }
    }
}
