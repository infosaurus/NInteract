using System;
using System.Linq.Expressions;

namespace NInteract
{
     public interface IVerifiable<TCollaborator> where TCollaborator : class
    {
         IChainable<TCollaborator> ShouldTell(Expression<Action<TCollaborator>> action);
         IChainable<TCollaborator> ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function);
    }
}