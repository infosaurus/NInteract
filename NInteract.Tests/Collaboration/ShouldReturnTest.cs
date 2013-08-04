using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Tests.Collaboration
{
    public class ShouldReturnTest : CollaborationTest<Star, IAssistant>
    {
        [Test]
        public void ShouldReturn_ValueType_Positive()
        {
            A.CallTo(star => star.SignContract())
             .Assuming(assistant => assistant.GiveTotalFee(Any<decimal>(), Any<bool>(), Any<CalculationPolicy>()))
             .Returns(Some<decimal>())
             .ShouldReturn(TheSame<decimal>());
        }

        [Test]
        [ExpectedException(typeof(DidntReturnException))]
        public void ShouldReturn_ValueType_Negative()
        {
            A.CallTo(star => star.SignContract())
             .Assuming(assistant => assistant.GiveTotalFee(Any<decimal>(), Any<bool>(), Any<CalculationPolicy>()))
             .Returns(Some<decimal>())
             .ShouldReturn(4m);
        }

        [Test]
        public void ShouldReturn_ReferenceType_Positive()
        {
            var autograph = new List<Autograph> { new Autograph() };
            A.CallTo(star => star.SignAutographs(1))
             .Assuming(assistant => assistant.PrintAutographs(Any<int>())).Returns(autograph)
             .ShouldReturn(autograph);
        }

        [Test]
        [ExpectedException(typeof(DidntReturnException))]
        public void ShouldReturn_ReferenceType_Negative()
        {
            var autograph = new List<Autograph> { new Autograph() };
            A.CallTo(star => star.SignAutographs(1))
             .Assuming(assistant => assistant.PrintAutographs(Any<int>())).Returns(null)
             .ShouldReturn(autograph);
        }

        [Test]
        [ExpectedException(typeof(InvalidAssertionTargetException))]
        public void ShouldReturn_IncorrectReturnType_ThrowsException()
        {
            A.CallTo(star => star.SignContract())
             .ShouldReturn(new Bill());
        }

        [Test]
        public void ShouldReturnSome_ReferenceType_Positive()
        {
            var autograph = new Autograph();
            A.CallTo(star => star.SignAutographs(1))
             .Assuming(assistant => assistant.PrintAutographs(Any<int>()))
                                                                          .Returns(new List<Autograph> { autograph })
             .ShouldReturnSome<IList<Autograph>>(list => list.Count == 1 && list.First() == autograph);
        }

        [Test]
        [ExpectedException(typeof(DidntReturnException))]
        public void ShouldReturnSome_ReferenceType_Negative()
        {
            var autograph = new Autograph();
            A.CallTo(star => star.SignAutographs(1))
             .Assuming(assistant => assistant.PrintAutographs(Any<int>()))
                                                                          .Returns(new List<Autograph> { autograph })
             .ShouldReturnSome<IList<Autograph>>(list => list.Count == 2 && list.First() == autograph);
        }

        [Test]
        public void ShouldReturnSome_ValueType_Positive()
        {
            A.CallTo(star => star.SignContract())
             .Assuming(assistant => assistant.GiveTotalFee(Any<decimal>(), Any<bool>(), Any<CalculationPolicy>()))
                                                                                                                  .Returns(5m)
             .ShouldReturnSome<decimal>(fee => fee == 5m);
        }

        [Test]
        [ExpectedException(typeof(DidntReturnException))]
        public void ShouldReturnSome_ValueType_Negative()
        {
            A.CallTo(star => star.SignContract())
             .Assuming(assistant => assistant.GiveTotalFee(Any<decimal>(), Any<bool>(), Any<CalculationPolicy>()))
                                                                                                                  .Returns(5m)
             .ShouldReturnSome<decimal>(fee => fee > 5m && fee < 6m);
        }
    }
}
