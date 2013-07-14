using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class ChainedAssumingsTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void AssumingReturns_Chained_Positive()
        {
            A.CallTo(star => star.Bored())

             .Assuming(assistant => assistant.GiveNextAppointment()).Returns(null)
             .Assuming(assistant => assistant.GetMasseurNextAvailability(Any<int>())).Returns(null)

             .ShouldTell(assistant => assistant.PrepareADrink());
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingReturns_Chained_Negative()
        {
            A.CallTo(star => star.Bored())

             .Assuming(assistant => assistant.GiveNextAppointment()).Returns(null)
             .Assuming(assistant => assistant.GetMasseurNextAvailability(Any<int>())).Returns(null)

             .ShouldTell(assistant => assistant.PayBill(Any<Bill>()));
        }

        [Test]
        public void AssumingThrows_Chained_Positive()
        {
            A.CallTo(star => star.PayBill(Some<Bill>()))

             .Assuming(assistant => assistant.PayBill(Any<Bill>())).Throws<OutOfCashException>()
             .Assuming(assistant => assistant.WithdrawCash(Any<int>())).Throws<BankruptException>()

             .ShouldTell(assistant => assistant.WithdrawCash(500));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void AssumingThrows_Chained_Negative()
        {
            A.CallTo(star => star.PayBill(Some<Bill>()))

             .Assuming(assistant => assistant.PayBill(Any<Bill>())).Throws<OutOfCashException>()
             .Assuming(assistant => assistant.WithdrawCash(Any<int>())).Throws<BankruptException>()

             .ShouldTell(assistant => assistant.PrepareADrink());
        }
    }
}
