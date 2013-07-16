// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract
{
    public interface IAssumptionSubject<TSut, TCollaborator> where TSut           : class
                                                             where TCollaborator  : class
    {
        IAssertable<TSut, TCollaborator> Throws<TException>() where TException : Exception, new();
    }


    public interface IAskAssumptionSubject<TSut, TCollaborator, TValue> : IAssumptionSubject<TSut, TCollaborator> where TSut          : class
                                                                                                                  where TCollaborator : class
    {
        IAssertable<TSut, TCollaborator> Returns(TValue value);
    }


    public abstract class BaseAssumptionSubject<TSut, TCollaborator> : IAssumptionSubject<TSut, TCollaborator> where TSut          : class
                                                                                                               where TCollaborator : class
    {
        protected readonly AssertionBuilder<TSut, TCollaborator> _assertionBuilder;

        protected BaseAssumptionSubject(AssertionBuilder<TSut, TCollaborator> assertionBuilder)
        {
            _assertionBuilder = assertionBuilder;
        }

        public abstract IAssertable<TSut, TCollaborator> Throws<TException>() where TException : Exception, new();
    }


    public class AskAssumptionSubject<TSut, TCollaborator, TValue> : BaseAssumptionSubject<TSut, TCollaborator>,
                                                                     IAskAssumptionSubject<TSut, TCollaborator, TValue> 
                                                                                     where TSut          : class 
                                                                                     where TCollaborator : class
    {
        private readonly Expression<Func<TCollaborator, TValue>> _function;

        public Expression<Func<TCollaborator, TValue>> Subject
        {
            get { return _function; }
        }

        public AskAssumptionSubject(AssertionBuilder<TSut, TCollaborator> assertionBuilder, 
                                    Expression<Func<TCollaborator, TValue>> function)          : base(assertionBuilder)
        {
            _function = function;
        }

        public IAssertable<TSut, TCollaborator> Returns(TValue value)
        {
            _assertionBuilder.AssumeReturns(this, value);
            return _assertionBuilder;
        }

        public override IAssertable<TSut, TCollaborator> Throws<TException>()
        {
            _assertionBuilder.AssumeThrows<TException, TValue>(this);
            return _assertionBuilder;
        }
    }


    public class TellAssumptionSubject<TSut, TCollaborator> : BaseAssumptionSubject<TSut, TCollaborator> where TSut          : class
                                                                                                         where TCollaborator : class
    {
        private readonly Expression<Action<TCollaborator>> _action;
        public Expression<Action<TCollaborator>> Subject { get { return _action; } }

        public TellAssumptionSubject(AssertionBuilder<TSut, TCollaborator> assertionBuilder,
                                     Expression<Action<TCollaborator>> action)                  : base(assertionBuilder)
        {
            _action = action;
        }

        public override IAssertable<TSut, TCollaborator> Throws<TException>()
        {
            _assertionBuilder.AssumeThrows<TException>(this);
            return _assertionBuilder;
        }
    }


    public class SetAssumptionSubject<TSut, TCollaborator> : BaseAssumptionSubject<TSut, TCollaborator> where TSut          : class
                                                                                                        where TCollaborator : class
    {
        private readonly Action<TCollaborator> _action;
        public Action<TCollaborator> Subject { get { return _action; } }

        public SetAssumptionSubject(AssertionBuilder<TSut, TCollaborator> assertionBuilder,
                                     Action<TCollaborator> action) : base(assertionBuilder)
        {
            _action = action;
        }

        public override IAssertable<TSut, TCollaborator> Throws<TException>()
        {
            _assertionBuilder.AssumeThrows<TException>(this);
            return _assertionBuilder;
        }
    }
}