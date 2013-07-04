using System;
using System.Collections.Generic;

namespace Ninteract.Engine
{
    public class ParameterPool
    {
        private readonly IParameterFactory _parameterFactory;
        private IDictionary<Type, object> _parameters = new Dictionary<Type, object>();

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
                throw new InvalidOperationException(string.Format("TheSame<{0}>() can't be used unless a parameter of the same type has previously been generated, e.g. with Some<{0}>().", typeof(T).ToString()));
            }
            return (T)recordedParameter;
        }
    }
}