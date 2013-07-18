using NUnit.Framework;
using Ninteract.Tests.Collaboration;

namespace Ninteract.Tests.Contract
{
    public abstract class IAssistantContract
    {
        [Test]
        public void GiveAppointmentCanReturnNoAppointment()
        {
            var assistant = AssistantWithNoAppointment();
            var appointment = assistant.GiveNextAppointment();
            Assert.AreEqual(NoAppointment(), appointment);
        }

        protected abstract IAssistant AssistantWithNoAppointment();

        public static object NoAppointment()
        {
            return null;
        }
    }

    [TestFixture]
    public class AssistantContract : IAssistantContract
    {
        protected override IAssistant AssistantWithNoAppointment()
        {
            return Sut.Assuming(sut => sut.NextAppointment()).Returns(null);
        }
    }

    public class AssistantCollaborationTest : CollaborationTest<Assistant, INoteBook>
    {
        public void AssistantNextAppointmentCallsNoteBook()
        {
            A.CallTo(assistant => assistant.GiveNextAppointment())
             .ShouldAsk(noteBook => noteBook.NextAppointment());
        }
    }
}
