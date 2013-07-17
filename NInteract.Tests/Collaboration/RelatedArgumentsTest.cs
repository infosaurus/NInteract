using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class RelatedArgumentsTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void ShouldTell_RelatedArguments_Positive()
        {
            A.CallTo(star => star.SignAutographs(Some<int>()))
             .ShouldTell(assistant => assistant.PrintAutographs(TheSame<int>()));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ShouldTell_RelatedArguments_Negative()
        {
            A.CallTo(star => star.SignAutographs(Some<int>()))
             .ShouldTell(assistant => assistant.BuySandwiches(TheSame<int>()));
        }


        [Test]
        public void ShouldTell_MultipleRelatedArguments_Positive()
        {
            A.CallTo(star => star.FriendWantsCoffee(Some<Coffee>(), Some<int>(), Some<bool>()))
             .ShouldTell(assistant => assistant.MakeCoffee(TheSame<Coffee>(), TheSame<int>(), TheSame<bool>()));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ShouldTell_MultipleRelatedArguments_Negative()
        {
            A.CallTo(star => star.FriendWantsCoffee(Some<Coffee>(), Some<int>(), Some<bool>()))
             .ShouldTell(assistant => assistant.PrintAutographs(TheSame<int>()));
        }

        [Test]
        public void ShouldTell_RelatedArgumentsWithVariables_Positive()
        {
            A.CallTo(star => star.JustMet(Some<string>(), Some<int>()))
             .ShouldTell(assistant => assistant.AddAddressBookEntry(Some<ContactInfo>(c => c.Name == TheSame<string>() 
                                                                                        && c.PhoneNumber == TheSame<int>())));
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void ShouldTell_RelatedArgumentsWithVariables_Negative()
        {
            A.CallTo(star => star.JustMet(Some<string>(), Some<int>()))
             .ShouldTell(assistant => assistant.AddAddressBookEntry(Some<ContactInfo>(c => c.Name == string.Empty)));
        }

        [Test]
        public void Assuming_MultipleRelatedArguments_Positive()
        {
            A.CallTo(star => star.FriendWantsCoffee(Some<Coffee>(), Some<int>(), Some<bool>()))
             .Assuming(assistant => assistant.MakeCoffee(TheSame<Coffee>(), TheSame<int>(), TheSame<bool>())).Throws<OutOfSugarException>()
             .ShouldTell(assistant => assistant.BuySomeSugar());
        }

        [Test]
        [ExpectedException(typeof(DidntTellException))]
        public void Assuming_MultipleRelatedArguments_Negative()
        {
            A.CallTo(star => star.FriendWantsCoffee(Some<Coffee>(), Some<int>(), Some<bool>()))
             .Assuming(assistant => assistant.MakeCoffee(TheSame<Coffee>(), TheSame<int>(), TheSame<bool>())).Throws<OutOfSugarException>()
             .ShouldTell(assistant => assistant.PrintAutographs(TheSame<int>()));
        }
    }
}
