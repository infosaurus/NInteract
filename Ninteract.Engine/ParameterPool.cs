// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Collections.Generic;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Engine
{
    public class ParameterPool
    {
        private readonly IParameterFactory          _parameterFactory;
        private readonly IDictionary<Type, object>  _parameters = new Dictionary<Type, object>();

        public ParameterPool(IParameterFactory parameterFactory)
        {
            _parameterFactory = parameterFactory;
        }

        public T Produce<T>()
        {
            object existingParameter;
            if (_parameters.TryGetValue(typeof(T), out existingParameter))
            {
                return (T)existingParameter;
            }
            var someValue = _parameterFactory.Create<T>();
            _parameters.Add(typeof(T), someValue);
            return someValue;
        }

        public T Find<T>()
        {
            object recordedParameter;
            if (!_parameters.TryGetValue(typeof(T), out recordedParameter))
            {
                throw new ParameterNotFoundException(typeof(T).Name);
            }
            return (T)recordedParameter;
        }
    }
}