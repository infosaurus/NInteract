using System;
using System.Linq.Expressions;
using Moq;
using Ninteract.Engine;

namespace Ninteract.Adapters
{
    public class MoqExpectedParameterFactory : IExpectedParameterFactory
    {
        public T Create<T>(Expression<Predicate<T>> predicate)
        {
            return It.Is<T>(predicate);
        }

        public T Create<T>()
        {
            return It.IsAny<T>();
        }
    }
}