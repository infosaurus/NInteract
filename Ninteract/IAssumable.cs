using System;
using System.Linq.Expressions;

namespace Ninteract
{
    public interface IAssumable<TSut, TCollaborator> where TSut          : class
                                                     where TCollaborator : class
    {
        IAssumptionSubject<TSut, TCollaborator, TValue> Assuming<TValue>(Expression<Func<TCollaborator, TValue>> function);
    }
}