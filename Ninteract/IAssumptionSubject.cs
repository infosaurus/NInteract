using System;
using System.Linq.Expressions;

namespace Ninteract
{
    public interface IAssumptionSubject<TSut, TCollaborator, TValue> where TSut          : class
                                                                     where TCollaborator : class
    {
        IVerifiable<TCollaborator> Returns(TValue value);

        Expression<Func<TCollaborator, TValue>> Subject { get; }
    }

    public class AssumptionSubject<TSut, TCollaborator, TValue> : IAssumptionSubject<TSut, TCollaborator, TValue> where TSut          : class 
                                                                                                                  where TCollaborator : class
    {
        private readonly AssertionBuilder<TSut, TCollaborator> _assertionBuilder;
        private readonly Expression<Func<TCollaborator, TValue>> _func;

        public Expression<Func<TCollaborator, TValue>> Subject
        {
            get { return _func; }
        }

        public AssumptionSubject(AssertionBuilder<TSut, TCollaborator> assertionBuilder, Expression<Func<TCollaborator, TValue>> func)
        {
            _assertionBuilder = assertionBuilder;
            _func = func;
        }

        public IVerifiable<TCollaborator> Returns(TValue value)
        {
            _assertionBuilder.AssumeReturns(this, value);
            return _assertionBuilder;
        }
    }
}