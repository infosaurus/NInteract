// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public interface IFake<T> where T : class 
    {
        T Illusion { get; }

        void Verify(Expression<Action<T>> expression);
        void Verify<TResult>(Expression<Func<T, TResult>> expression);
        void VerifyGet<TResult>(Expression<Func<T, TResult>> getExpression);
        void VerifySet(Action<T> setAction);
        void SetupReturns<TResult>(Expression<Func<T, TResult>> function, TResult result);
        void SetupThrows<TException>(Expression<Action<T>> action) where TException : Exception, new();
        void SetupThrows<TException, TResult>(Expression<Func<T, TResult>> function) where TException : Exception, new();
        void SetupThrows<TException>(Action<T> setAction) where TException : Exception, new();
    }
}
