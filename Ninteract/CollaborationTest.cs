using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ninteract.Adapters;
using Ninteract.Engine;

namespace NInteract
{
    public class CollaborationTest<TSut, TCollaborator> where TSut          : class 
                                                        where TCollaborator : class
    {
        private IDictionary<Type, object> _recordedSutCallParameters = new Dictionary<Type, object>();
        private IParameterFactory _parameterFactory = new AutoFixtureParameterFactory();
        private ICollaboratorCallParameterBuilder _collaboratorCallParameterBuilder = new MoqCollaboratorCallParameterBuilder();

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
            if (_recordedSutCallParameters.TryGetValue(typeof(T), out existingValue))
            {
                return (T)existingValue;
            }
            var someValue = _parameterFactory.Create<T>();
            _recordedSutCallParameters.Add(typeof(T), someValue);
            return someValue;
        }

        public T Some<T>(Expression<Predicate<T>> predicate)
        {
            return _collaboratorCallParameterBuilder.Some<T>(predicate);
        }

        public T TheSame<T>()
        {
            object recordedParameter;
            if (!_recordedSutCallParameters.TryGetValue(typeof(T), out recordedParameter))
            {
                throw new InvalidOperationException(string.Format("TheSame<{0}>() can't be used unless a parameter of the same type has previously been generated, e.g. with Some<{0}>().", typeof(T).ToString()));
            }
            return (T)recordedParameter;
        }
    }
}
