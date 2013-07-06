using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public interface IFake<T> where T : class 
    {
        T Illusion { get; }

        void Verify(Expression<Action<T>> expression);
        void Verify<TResult>(Expression<Func<T, TResult>> expression);
    }
}
