using System;
using System.Linq.Expressions;
using Ninteract;

namespace NInteract
{
    /// <summary>
    /// Base class for NInteract collaboration tests. Provides syntax starters and shortcuts for expressing assertions
    /// </summary>
    /// <typeparam name="TSut"></typeparam>
    /// <typeparam name="TCollaborator"></typeparam>
    public class CollaborationTest<TSut, TCollaborator> where TSut          : class 
                                                        where TCollaborator : class
    {
        private AssertionBuilder<TSut, TCollaborator> _assertionBuilder;

        public ICallable<TSut, TCollaborator> A 
        { 
            get
            {
                _assertionBuilder = new AssertionBuilder<TSut, TCollaborator>();
                return _assertionBuilder;
            } 
        }

        public T Some<T>()
        {
            return _assertionBuilder.Some<T>();
        }

        public T Some<T>(Expression<Predicate<T>> predicate)
        {
            return _assertionBuilder.Some<T>(predicate);
        }

        public T TheSame<T>()
        {
            return _assertionBuilder.TheSame<T>();
        }
    }
}
