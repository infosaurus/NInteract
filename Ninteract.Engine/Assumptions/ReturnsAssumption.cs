// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public class ReturnsAssumption<TCollaborator, TValue> where TCollaborator : class
    {
        private readonly Expression<Func<TCollaborator, TValue>> _subject;
        private readonly TValue _value;

        public ReturnsAssumption(Expression<Func<TCollaborator, TValue>> subject, TValue value)
        {
            _subject = subject;
            _value = value;
        }

        public void ApplyOn(IFake<TCollaborator> fake)
        {
            fake.SetupReturns(_subject, _value);
        }
    }
}