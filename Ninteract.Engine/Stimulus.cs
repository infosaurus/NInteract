// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public class Stimulus<TSut>
    {
        private readonly LambdaExpression _sutCall;

        public Stimulus(Expression<Action<TSut>> sutCall)
        {
            _sutCall = sutCall;
        }

        public void ApplyTo(TSut sut)
        {
            _sutCall.Compile().DynamicInvoke(sut);
        }
    }
}