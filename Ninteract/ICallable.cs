// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract
{
    public interface ICallable<TSut, TCollaborator> where TSut          : class
                                                    where TCollaborator : class
    {
        IAssertable<TSut, TCollaborator> CallTo(Expression<Action<TSut>> expression);
        IAssertable<TSut, TCollaborator> CallTo<TResult>(Expression<Func<TSut, TResult>> expression);
    }
}