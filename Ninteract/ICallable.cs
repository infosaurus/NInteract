using System;
using System.Linq.Expressions;

namespace Ninteract
{
    public interface ICallable<TSut, TCollaborator> where TSut          : class
                                                    where TCollaborator : class
    {
        IAssertable<TSut, TCollaborator> CallTo(Expression<Action<TSut>> expression);
    }
}