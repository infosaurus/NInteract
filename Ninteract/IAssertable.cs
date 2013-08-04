// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract
{
    public interface IAssertable<TSut, TCollaborator> : IAssumable<TSut, TCollaborator>,
                                                        IVerifiable<TCollaborator> 
                                                        where TSut          : class
                                                        where TCollaborator : class
    {
        void ShouldReturn<TResult>(TResult value);
        void ShouldReturnSome<TResult>(Expression<Predicate<TResult>> expectation);
    }
}
