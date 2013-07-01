using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NInteract;
using Ninteract.Adapters;
using Ninteract.Engine;
using Ninteract.Engine.Exceptions;

namespace Ninteract
{
    internal class AssertionBuilder<TSut, TCollaborator> : ICallable<TSut, TCollaborator>,
                                                           IVerifiable<TCollaborator>
                                                           where TSut          : class
                                                           where TCollaborator : class
    {
        // ICallable
        private TSut _sut;
        private IDependencyContainer<TSut, TCollaborator> _dependencyContainer = new AutoFixtureDependencyContainer<TSut, TCollaborator>();
        private IFakeFactory<TCollaborator> _fakeCollaboratorFactory = new MoqFactory<TCollaborator>();
        private Queue<LambdaExpression> _invocationQueue = new Queue<LambdaExpression>();
        private IList<IFake<TCollaborator>> _fakeCollaborators = new List<IFake<TCollaborator>>();

        public AssertionBuilder()
        {
            _dependencyContainer.RegisterCollaborator(() =>
            {
                var collaborator = _fakeCollaboratorFactory.Create();
                _fakeCollaborators.Add(collaborator);
                return collaborator.Placeholder;
            });
            _sut = _dependencyContainer.CreateSut();
        }
        
        // IVerifiable

        public IVerifiable<TCollaborator> CallTo(Expression<Action<TSut>> expression)
        {
            _invocationQueue.Enqueue(expression);
            return this;
        }

        
        public void Invoke()
        {
            foreach (var expression in _invocationQueue)
            {
                expression.Compile().DynamicInvoke(_sut);
            }
        }

        // IVerifiable

        public void ShouldTell(Expression<Action<TCollaborator>> action)
        {
            Invoke();
            var mock = GetCollaboratorMockOrThrow();
            try
            {
                mock.Verify(action);
            }
            catch (VerifyException exception)
            {
                ExceptionThrower.ThrowDidntTell<TSut, TCollaborator>(action, exception);
            }
        }

        public void ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function)
        {
            Invoke();
            var mock = GetCollaboratorMockOrThrow();
            try
            {
                mock.Verify(function);
            }
            catch (VerifyException exception)
            {
                ExceptionThrower.ThrowDidntAsk<TSut, TCollaborator, TResult>(function, exception);
            }
        }

        private IFake<TCollaborator> GetCollaboratorMockOrThrow(Func<IFake<TCollaborator>, bool> mockSelector = null)
        {
            IFake<TCollaborator> mock;
            var noCollaboratorSelectorPrecisionMessage = string.Empty;
            if (mockSelector == null)
            {
                if (_fakeCollaborators.Count > 1)
                    throw new InvalidOperationException(
                        string.Format(
                            "Type {0} has more than 1 injectable collaborator of type {1}. Use a selector to choose one of them.",
                            typeof(TSut).FullName, typeof(TCollaborator).FullName));
                mockSelector = anyMock => true; // there's only one anyway
            }
            else
            {
                noCollaboratorSelectorPrecisionMessage = "matching selector";
            }
            mock = _fakeCollaborators.FirstOrDefault(mockSelector);
            if (mock == null)
            {
                throw new InvalidOperationException(string.Format("Type {0} has no injectable collaborator of type {1} {2}.", typeof(TSut).FullName, typeof(TCollaborator).FullName, noCollaboratorSelectorPrecisionMessage));
            }
            return mock;
        }
    }
}
