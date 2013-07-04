using System;
using System.Linq.Expressions;
using NInteract;
using Ninteract.Adapters;
using Ninteract.Engine;

namespace Ninteract
{
    internal class AssertionBuilder<TSut, TCollaborator> : ICallable<TSut, TCollaborator>,
                                                           IVerifiable<TCollaborator>
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
        
        public IVerifiable<TCollaborator> CallTo(Expression<Action<TSut>> sutCall)
        {
            _ninteractEngine.Stimulus = new Stimulus<TSut>(sutCall);
            return this;
        }

        public void ShouldTell(Expression<Action<TCollaborator>> action)
        {
            _ninteractEngine.Expectation = new TellExpectation<TSut, TCollaborator>(action);
            _ninteractEngine.Run();
        }

        public void ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function)
        {
            _ninteractEngine.Expectation = new AskExpectation<TSut, TCollaborator, TResult>(function);
            _ninteractEngine.Run();
        }

        public T Some<T>()
        {
            return _stimulusParameterPool.Produce<T>();
        }

        public T TheSame<T>()
        {
            return _stimulusParameterPool.Find<T>();
        }

        public T Some<T>(Expression<Predicate<T>> expectation)
        {
            return _expectedParameterFactory.Create<T>(expectation);
        }
    }
}
