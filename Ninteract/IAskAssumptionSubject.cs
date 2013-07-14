using System;
using System.Linq.Expressions;

namespace Ninteract
{
    public interface IAssumptionSubject<TSut, TCollaborator> where TSut : class
                                                             where TCollaborator : class
    {
        IVerifiable<TCollaborator> Throws<TException>() where TException : Exception, new();
    }

    public interface IAskAssumptionSubject<TSut, TCollaborator, TValue> : IAssumptionSubject<TSut, TCollaborator> where TSut          : class
                                                                                                                  where TCollaborator : class
    {
        IVerifiable<TCollaborator> Returns(TValue value);

        Expression<Func<TCollaborator, TValue>> Subject { get; }
    }

    public interface ITellAssumptionSubject<TSut, TCollaborator> : IAssumptionSubject<TSut, TCollaborator> where TSut          : class
                                                                                                           where TCollaborator : class
    {
        Expression<Action<TCollaborator>> Subject { get; }
    }

    public interface ISetAssumptionSubject<TSut, TCollaborator> : IAssumptionSubject<TSut, TCollaborator> where TSut : class
                                                                                                          where TCollaborator : class
    {
        Action<TCollaborator> Subject { get; }
    }

    public abstract class BaseAssumptionSubject<TSut, TCollaborator> : IAssumptionSubject<TSut, TCollaborator> where TSut : class
                                                                                                               where TCollaborator : class
    {
        protected readonly AssertionBuilder<TSut, TCollaborator> _assertionBuilder;

        protected BaseAssumptionSubject(AssertionBuilder<TSut, TCollaborator> assertionBuilder)
        {
            _assertionBuilder = assertionBuilder;
        }

        public abstract IVerifiable<TCollaborator> Throws<TException>() where TException : Exception, new();
    }

    public class AskAssumptionSubject<TSut, TCollaborator, TValue> : BaseAssumptionSubject<TSut, TCollaborator>,
                                                                     IAskAssumptionSubject<TSut, TCollaborator, TValue> where TSut          : class 
                                                                                                                        where TCollaborator : class
    {
        private readonly Expression<Func<TCollaborator, TValue>> _function;

        public Expression<Func<TCollaborator, TValue>> Subject
        {
            get { return _function; }
        }

        public AskAssumptionSubject(AssertionBuilder<TSut, TCollaborator> assertionBuilder, 
                                    Expression<Func<TCollaborator, TValue>> function)          : base(assertionBuilder)
        {
            _function = function;
        }

        public IVerifiable<TCollaborator> Returns(TValue value)
        {
            _assertionBuilder.AssumeReturns(this, value);
            return _assertionBuilder;
        }

        public override IVerifiable<TCollaborator> Throws<TException>()
        {
            _assertionBuilder.AssumeThrows<TException, TValue>(this);
            return _assertionBuilder;
        }
    }

    public class TellAssumptionSubject<TSut, TCollaborator> : BaseAssumptionSubject<TSut, TCollaborator>,
                                                              ITellAssumptionSubject<TSut, TCollaborator> where TSut : class
                                                                                                          where TCollaborator : class
    {
        private readonly Expression<Action<TCollaborator>> _action;
        public Expression<Action<TCollaborator>> Subject { get { return _action; } }

        public TellAssumptionSubject(AssertionBuilder<TSut, TCollaborator> assertionBuilder,
                                     Expression<Action<TCollaborator>> action)
            : base(assertionBuilder)
        {
            _action = action;
        }

        public override IVerifiable<TCollaborator> Throws<TException>()
        {
            _assertionBuilder.AssumeThrows<TException>(this);
            return _assertionBuilder;
        }
    }

    public class SetAssumptionSubject<TSut, TCollaborator> : BaseAssumptionSubject<TSut, TCollaborator>,
                                                             ISetAssumptionSubject<TSut, TCollaborator> where TSut          : class
                                                                                                        where TCollaborator : class
    {
        private readonly Action<TCollaborator> _action;
        public Action<TCollaborator> Subject { get { return _action; } }

        public SetAssumptionSubject(AssertionBuilder<TSut, TCollaborator> assertionBuilder,
                                     Action<TCollaborator> action) : base(assertionBuilder)
        {
            _action = action;
        }

        public override IVerifiable<TCollaborator> Throws<TException>()
        {
            _assertionBuilder.AssumeThrows<TException>(this);
            return _assertionBuilder;
        }
    }
}