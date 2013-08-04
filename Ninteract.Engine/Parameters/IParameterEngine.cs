// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public interface IParameterEngine
    {
        T Some<T>();
        T TheSame<T>();
        T Any<T>();
        T Some<T>(Expression<Predicate<T>> expectation);
    }
}