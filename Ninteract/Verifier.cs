using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ninteract.Engine;
using Ninteract.Engine.Exceptions;

namespace NInteract
{
     public interface IVerifier<TCollaborator> where TCollaborator : class
    {
        void ShouldTell(Expression<Action<TCollaborator>> action);
        void ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function);
    }

    public class Verifier<TSut, TCollaborator> : IVerifier<TCollaborator> where TSut : class
                                                                                where TCollaborator : class
    {
        private readonly IInvoker<TSut, TCollaborator> _invoker;
        private readonly IList<IFake<TCollaborator>> _fakeCollaborators;

        public Verifier(IInvoker<TSut, TCollaborator> invoker, IList<IFake<TCollaborator>> fakeCollaborators)
        {
            _invoker = invoker;
            _fakeCollaborators = fakeCollaborators;
        }

        public void ShouldTell(Expression<Action<TCollaborator>> action)
        {
            _invoker.Invoke();
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
            _invoker.Invoke();
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