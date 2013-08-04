// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;
using Ninteract.Engine;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Adapters
{
    public class AutoMoqParameterEngine : IParameterEngine
    {
        private readonly ParameterPool               _stimulusParameterPool;
        private readonly IExpectedParameterFactory   _expectedParameterFactory;

        public AutoMoqParameterEngine()
        {
            _stimulusParameterPool      = new ParameterPool(new AutoFixtureParameterFactory());
            _expectedParameterFactory   = new MoqExpectedParameterFactory();
        }

        public T Some<T>()
        {
            return FindOrProduce<T>();
        }

        public T TheSame<T>()
        {
            // Some and TheSame are only syntactically different and sequential,
            // in reality they can happen in any order at runtime
            // (eg, Assumptions containing TheSame are evaluated before Calls containing Some)

            return Some<T>();
        }

        private T FindOrProduce<T>()
        {
            try
            {
                return _stimulusParameterPool.Find<T>();
            }
            catch (ParameterNotFoundException)
            {
                return _stimulusParameterPool.Produce<T>();
            }
        }

        public T Any<T>()
        {
            return _expectedParameterFactory.Create<T>();
        }

        public T Some<T>(Expression<Predicate<T>> expectation)
        {
            return _expectedParameterFactory.Create<T>(expectation);
        }
    }
}