using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ninteract.Engine;
using Ninteract.Engine.Exceptions;

namespace NInteract
{
     public interface IVerifiable<TCollaborator> where TCollaborator : class
    {
        void ShouldTell(Expression<Action<TCollaborator>> action);
        void ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function);
        void Invoke();
    }
}