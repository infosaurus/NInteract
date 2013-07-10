using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public class ReturnsAssumption<TCollaborator, TValue> where TCollaborator : class
    {
        private readonly Expression<Func<TCollaborator, TValue>> _subject;
        private readonly TValue _value;

        public ReturnsAssumption(Expression<Func<TCollaborator, TValue>> subject, TValue value)
        {
            _subject = subject;
            _value = value;
        }

        public void ApplyOn(IFake<TCollaborator> fake)
        {
            fake.SetupReturns(_subject, _value);
        }
    }
}