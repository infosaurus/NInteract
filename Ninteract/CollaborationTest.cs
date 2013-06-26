using System;
using System.Collections.Generic;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace NInteract
{
    public class CollaborationTest<TSut, TCollaborator> where TSut          : class 
                                                        where TCollaborator : class
    {
        private IDictionary<Type, object> _expectedParametersByType = new Dictionary<Type, object>();
        private IFixture _parametersFactory = new Fixture().Customize(new AutoMoqCustomization());

        public ICaller<TSut, TCollaborator> A 
        { 
            get
            {
                return new Caller<TSut, TCollaborator>();
            } 
        }

        public T Some<T>()
        {
            object existingValue;
            if (_expectedParametersByType.TryGetValue(typeof(T), out existingValue))
            {
                return (T)existingValue;
            }
            var someValue = _parametersFactory.Create<T>();
            _expectedParametersByType.Add(typeof(T), someValue);
            return someValue;
        }

        public T TheSame<T>()
        {
            object recordedParameter;
            if (!_expectedParametersByType.TryGetValue(typeof(T), out recordedParameter))
            {
                throw new InvalidOperationException(string.Format("TheSame<{0}>() can't be used unless a parameter of the same type has previously been generated, e.g. with Some<{0}>().", typeof(T).ToString()));
            }
            return (T)recordedParameter;
        }
    }
}
