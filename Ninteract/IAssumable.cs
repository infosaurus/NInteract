// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

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