using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public interface ICollaboratorCallParameterBuilder
    {
        T Some<T>(Expression<Predicate<T>> predicate);
    }
}