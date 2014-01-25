using NUnit.Framework;
using Ninteract.Tests.Collaboration;

namespace Ninteract.Tests.Contract
{
    public abstract class AssistantContract
    {
        [Test]
        public void GiveAppointment_NoAppointment()
        {
            var assistant = AssistantWithNoAppointment();
            var appointment = assistant.GiveNextAppointment();
            Assert.AreEqual(NoAppointment, appointment);
        }

        protected abstract IAssistant AssistantWithNoAppointment();

        public static object NoAppointment
        {
            get { return null; }
        }
    }

    [TestFixture]
    public class AssistantContractTest : AssistantContract
    {
        protected override IAssistant AssistantWithNoAppointment()
        {
            return new Assistant(new NoteBook());
        }
    }
}
