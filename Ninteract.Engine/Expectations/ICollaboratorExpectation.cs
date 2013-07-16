// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Engine
{
    public interface ICollaboratorExpectation<TCollaborator> where TCollaborator : class 
    {
        void VerifyAgainst(IFake<TCollaborator> fake);
    }

    public class TellExpectation<TSut, TCollaborator> : ICollaboratorExpectation<TCollaborator> where TCollaborator : class 
                                                                                    where TSut          : class
    {
        private readonly Expression<Action<TCollaborator>> _tellExpression;

        public TellExpectation(Expression<Action<TCollaborator>> tellExpression)
        {
            _tellExpression = tellExpression;
        }

        public void VerifyAgainst(IFake<TCollaborator> fake)
        {
            try
            {
                fake.Verify(_tellExpression);
            }
            catch (VerifyException exception)
            {
                ExceptionThrower.ThrowDidntTell<TSut, TCollaborator>(_tellExpression, exception);
            }
        }
    }

    public class AskExpectation<TSut, TCollaborator, TResult> : ICollaboratorExpectation<TCollaborator> where TCollaborator : class
                                                                                            where TSut          : class
    {
        private readonly Expression<Func<TCollaborator, TResult>> _askExpression;

        public AskExpectation(Expression<Func<TCollaborator, TResult>> askExpression)
        {
            _askExpression = askExpression;
        }

        public void VerifyAgainst(IFake<TCollaborator> fake)
        {
            try
            {
                fake.Verify(_askExpression);
            }
            catch (VerifyException exception)
            {
                ExceptionThrower.ThrowDidntAsk<TSut, TCollaborator, TResult>(_askExpression, exception);
            }
        }
    }

    public class GetExpectation<TSut, TCollaborator, TResult> : ICollaboratorExpectation<TCollaborator> where TCollaborator : class
                                                                                            where TSut          : class
    {
        private readonly Expression<Func<TCollaborator, TResult>> _getExpression;

        public GetExpectation(Expression<Func<TCollaborator, TResult>> getExpression)
        {
            _getExpression = getExpression;
        }

        public void VerifyAgainst(IFake<TCollaborator> fake)
        {
            try
            {
                fake.VerifyGet(_getExpression);
            }
            catch (VerifyException exception)
            {
                ExceptionThrower.ThrowDidntGet<TSut, TCollaborator, TResult>(_getExpression, exception);
            }
        }
    }

    public class SetExpectation<TSut, TCollaborator> : ICollaboratorExpectation<TCollaborator>
        where TCollaborator : class
        where TSut : class
    {
        private readonly Action<TCollaborator> _setAction;

        public SetExpectation(Action<TCollaborator> setAction)
        {
            _setAction = setAction;
        }

        public void VerifyAgainst(IFake<TCollaborator> fake)
        {
            try
            {
                fake.VerifySet(_setAction);
            }
            catch (VerifyException exception)
            {
                ExceptionThrower.ThrowDidntSet<TSut, TCollaborator>(_setAction, exception);
            }
        }
    }
}