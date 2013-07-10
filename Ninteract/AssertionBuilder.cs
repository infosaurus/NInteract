using System;
using System.Linq.Expressions;
using Ninteract.Adapters;
using Ninteract.Engine;

namespace Ninteract
{
    public class AssertionBuilder<TSut, TCollaborator> : ICallable    <TSut, TCollaborator>,
                                                         IAssertable   <TSut, TCollaborator>
                                                         where TSut          : class
                                                         where TCollaborator : class
    {
        private NinteractEngine<TSut, TCollaborator> _ninteractEngine;
        private ParameterPool _stimulusParameterPool;
        private IExpectedParameterFactory _expectedParameterFactory;

        public AssertionBuilder()
        {
            _ninteractEngine = new NinteractEngine<TSut, TCollaborator>(new AutoFixtureDependencyContainer<TSut, TCollaborator>(),
                                                                        new MoqFactory<TCollaborator>());
            _stimulusParameterPool = new ParameterPool(new AutoFixtureParameterFactory());
            _expectedParameterFactory = new MoqExpectedParameterFactory();
        }
        
        public IAssertable<TSut, TCollaborator> CallTo(Expression<Action<TSut>> sutCall)
        {
            _ninteractEngine.Stimulus = new Stimulus<TSut>(sutCall);
            return this;
        }

        public void ShouldTell(Expression<Action<TCollaborator>> action)
        {
            _ninteractEngine.Expect(new TellExpectation<TSut, TCollaborator>(action));
            _ninteractEngine.Run();
        }

        public IVerifiable<TCollaborator> ShouldTell(Expression<Action<TCollaborator>> action, IShouldChainer shouldChainer)
        {
            _ninteractEngine.Expect(new TellExpectation<TSut, TCollaborator>(action));

            return this;
        }

        public void ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function)
        {
            _ninteractEngine.Expect(new AskExpectation<TSut, TCollaborator, TResult>(function));
            _ninteractEngine.Run();
        }

        public IVerifiable<TCollaborator> ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function, IShouldChainer shouldChainer)
        {
            _ninteractEngine.Expect(new AskExpectation<TSut, TCollaborator, TResult>(function));

            return this;
        }

        public void ShouldGet<TResult>(Expression<Func<TCollaborator, TResult>> function)
        {
            _ninteractEngine.Expect(new GetExpectation<TSut, TCollaborator, TResult>(function));
            _ninteractEngine.Run();
        }

        public IVerifiable<TCollaborator> ShouldGet<TResult>(Expression<Func<TCollaborator, TResult>> function, IShouldChainer shouldChainer)
        {
            _ninteractEngine.Expect(new GetExpectation<TSut, TCollaborator, TResult>(function));
            return this;
        }

        public void ShouldSet(Action<TCollaborator> setAction)
        {
            _ninteractEngine.Expect(new SetExpectation<TSut, TCollaborator>(setAction));
            _ninteractEngine.Run();
        }

        public IVerifiable<TCollaborator> ShouldSet(Action<TCollaborator> setAction, IShouldChainer shouldChainer)
        {
            _ninteractEngine.Expect(new SetExpectation<TSut, TCollaborator>(setAction));
            return this;
        }

        public void ShouldThrow<TException>()
            where TException : Exception
        {
            _ninteractEngine.AddEncompassingExpectation(new ThrowExpectation<TException>());
            _ninteractEngine.Run();
        }

        public IAssumptionSubject<TSut, TCollaborator, TValue> Assuming<TValue>(Expression<Func<TCollaborator, TValue>> function)
        {
            return new AssumptionSubject<TSut, TCollaborator, TValue>(this, function);
        }

        public void AssumeReturns<TValue>(IAssumptionSubject<TSut, TCollaborator, TValue> assumptionSubject, TValue value)
        {
            _ninteractEngine.Assume(new ReturnsAssumption<TCollaborator, TValue>(assumptionSubject.Subject, value));
        }

        public IVerifiable<TCollaborator> And()
        {
            return this;
        }

        public T Some<T>()
        {
            return _stimulusParameterPool.Produce<T>();
        }

        public T TheSame<T>()
        {
            return _stimulusParameterPool.Find<T>();
        }

        public T Any<T>()
        {
            return _expectedParameterFactory.Create<T>();
        }

        public T Some<T>(Expression<Predicate<T>> expectation)
        {
            return _expectedParameterFactory.Create<T>(expectation);
        }
    }
}
