using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public class ActionThrowsAssumption<TCollaborator, TException> where TCollaborator : class 
                                                                   where TException    : Exception, new()
    {
        private readonly Expression<Action<TCollaborator>> _subject;

        public ActionThrowsAssumption(Expression<Action<TCollaborator>> subject)
        {
            _subject = subject;
        }

        public void ApplyOn(IFake<TCollaborator> fake)
        {
            fake.SetupThrows<TException>(_subject);
        }
    }

    public class FunctionThrowsAssumption<TCollaborator, TException, TResult> where TCollaborator : class
                                                                              where TException    : Exception, new()
    {
        private readonly Expression<Func<TCollaborator, TResult>> _subject;

        public FunctionThrowsAssumption(Expression<Func<TCollaborator, TResult>> subject)
        {
            _subject = subject;
        }

        public void ApplyOn(IFake<TCollaborator> fake)
        {
            fake.SetupThrows<TException, TResult>(_subject);
        }
    }

    public class SetActionThrowsAssumption<TCollaborator, TException> where TCollaborator : class
                                                                      where TException    : Exception, new()
    {
        private readonly Action<TCollaborator> _subject;

        public SetActionThrowsAssumption(Action<TCollaborator> subject)
        {
            _subject = subject;
        }

        public void ApplyOn(IFake<TCollaborator> fake)
        {
            fake.SetupThrows<TException>(_subject);
        }
    }
}