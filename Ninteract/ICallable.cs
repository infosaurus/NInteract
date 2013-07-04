using System;
using System.Linq.Expressions;

namespace NInteract
{
    public interface ICallable<TSut, TCollaborator> where TSut          : class
                                                    where TCollaborator : class
    {
        IVerifiable<TCollaborator> CallTo(Expression<Action<TSut>> expression);
    }
}