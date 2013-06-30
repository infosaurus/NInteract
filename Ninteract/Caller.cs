using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ninteract.Adapters;
using Ninteract.Engine;

namespace NInteract
{
    public interface ICaller<TSut, TCollaborator> where TSut          : class
                                                  where TCollaborator : class
    {
        IVerifier<TCollaborator> CallTo(Expression<Action<TSut>> expression);
    }

    public interface IInvoker<TSut, TCollaborator> where TSut          : class 
                                                   where TCollaborator : class
    {
        void Invoke();
    }

    public class Caller<TSut, TCollaborator> : ICaller<TSut, TCollaborator>,
                                               IInvoker<TSut, TCollaborator> where TSut          : class 
                                                                             where TCollaborator : class
    {
        private TSut _sut;
        private IDependencyContainer<TSut, TCollaborator> _dependencyContainer    = new AutoFixtureDependencyContainer<TSut, TCollaborator>();
        private IFakeFactory<TCollaborator> _fakeCollaboratorFactory        = new MoqFactory<TCollaborator>(); 
        private Queue<LambdaExpression> _invocationQueue                    = new Queue<LambdaExpression>();
        private IList<IFake<TCollaborator>> _fakeCollaborators              = new List<IFake<TCollaborator>>();

        public Caller()
        {
            _dependencyContainer.RegisterCollaborator(() =>
            {
                var collaborator = _fakeCollaboratorFactory.Create();
                _fakeCollaborators.Add(collaborator);
                return collaborator.Placeholder;
            });
            _sut = _dependencyContainer.CreateSut();
        }

        public IVerifier<TCollaborator> CallTo(Expression<Action<TSut>> expression)
        {
            _invocationQueue.Enqueue(expression);
            return new Verifier<TSut, TCollaborator>(this, _fakeCollaborators);
        }

        public void Invoke()
        {
            foreach (var expression in _invocationQueue)
            {
                expression.Compile().DynamicInvoke(_sut);
            }
        }
    }
}