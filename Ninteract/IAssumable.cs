using System;
using System.Linq.Expressions;

namespace Ninteract
{
    public interface IAssumable<TSut, TCollaborator> where TSut          : class
                                                     where TCollaborator : class
    {
        IAskAssumptionSubject<TSut, TCollaborator, TValue> Assuming<TValue>(Expression<Func<TCollaborator, TValue>> function);
        IAssumptionSubject<TSut, TCollaborator> Assuming(Expression<Action<TCollaborator>> action);
        IAssumptionSubject<TSut, TCollaborator> AssumingSet(Action<TCollaborator> func);
    }
}