using System;
using System.Linq.Expressions;

namespace NInteract
{
     public interface IVerifiable<TCollaborator> where TCollaborator : class
    {
        void ShouldTell(Expression<Action<TCollaborator>> action);
        void ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function);
    }
}