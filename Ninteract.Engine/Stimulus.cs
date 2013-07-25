// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public class Stimulus<TSut> where TSut : class
    {
        protected LambdaExpression _sutCall;

        public Stimulus() { }

        public Stimulus(Expression<Action<TSut>> sutCall)
        {
            _sutCall = sutCall;
        }

        public object ApplyTo(TSut sut)
        {
            object result = _sutCall.Compile().DynamicInvoke(sut);
            return result;
        }

        public override string ToString()
        {
            return MethodFormatter.GetShortFormattedMethodCall( (Expression<Action<TSut>>) _sutCall);
        }
    }

    /// <summary>
    /// Needed for expectations on return value of stimulus
    /// </summary>
    public class Stimulus<TSut, TResult> : Stimulus<TSut> where TSut : class
    {
        public Stimulus(Expression<Func<TSut, TResult>> sutCall)
        {
            _sutCall = sutCall;
        }

        public override string ToString()
        {
            return MethodFormatter.GetFormattedMethodCallWithReturnType((Expression<Func<TSut, TResult>>)_sutCall);
        }
    }
}