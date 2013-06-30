using System;
using System.Linq.Expressions;
using Moq;
using Ninteract.Engine;

namespace Ninteract.Adapters
{
    public class MoqCollaboratorCallParameterBuilder : ICollaboratorCallParameterBuilder
    {
        public T Some<T>(Expression<Predicate<T>> predicate)
        {
            return It.Is<T>(predicate);
        }
    }
}