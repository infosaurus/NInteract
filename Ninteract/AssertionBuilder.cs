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

        public IAskAssumptionSubject<TSut, TCollaborator, TValue> Assuming<TValue>(Expression<Func<TCollaborator, TValue>> function)
        {
            return new AskAssumptionSubject<TSut, TCollaborator, TValue>(this, function);
        }

        public IAssumptionSubject<TSut, TCollaborator> Assuming(Expression<Action<TCollaborator>> action)
        {
            return new TellAssumptionSubject<TSut, TCollaborator>(this, action);
        }

        public IAssumptionSubject<TSut, TCollaborator> AssumingSet(Action<TCollaborator> action)
        {
            return new SetAssumptionSubject<TSut, TCollaborator>(this, action);
        }

        public void AssumeReturns<TValue>(IAskAssumptionSubject<TSut, TCollaborator, TValue> askAssumptionSubject, TValue value)
        {
            _ninteractEngine.Assume(new ReturnsAssumption<TCollaborator, TValue>(askAssumptionSubject.Subject, value));
        }

        public void AssumeThrows<TException>(ITellAssumptionSubject<TSut, TCollaborator> assumptionSubject) 
            where TException : Exception, new()
        {
            _ninteractEngine.Assume(new ActionThrowsAssumption<TCollaborator, TException>(assumptionSubject.Subject));
        }

        public void AssumeThrows<TException, TValue>(IAskAssumptionSubject<TSut, TCollaborator, TValue> assumptionSubject) 
            where TException : Exception, new()
        {
            _ninteractEngine.Assume(new FunctionThrowsAssumption<TCollaborator, TException, TValue>(assumptionSubject.Subject));
        }

        public void AssumeThrows<TException>(ISetAssumptionSubject<TSut, TCollaborator> assumptionSubject)
            where TException : Exception, new()
        {
            _ninteractEngine.Assume(new SetActionThrowsAssumption<TCollaborator, TException>(assumptionSubject.Subject));
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
