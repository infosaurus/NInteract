using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public interface IExpectedParameterFactory
    {
        T Create<T>(Expression<Predicate<T>> predicate);
        T Create<T>();
    }
}