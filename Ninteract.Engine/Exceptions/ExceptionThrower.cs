// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract.Engine.Exceptions
{
    public static class ExceptionThrower
    {
        public static void ThrowDidntTell<TSut, TCollaborator>(Expression<Action<TCollaborator>> tellAction, 
                                                               VerifyException originalException)
            where TSut          : class
            where TCollaborator : class
        {
            var message = ExceptionMessageBuilder.CreateDidntTellMessage<TSut, TCollaborator>(tellAction);
            throw new DidntTellException(message, originalException);
        }

        public static void ThrowDidntAsk<TSut, TCollaborator, TResult>(Expression<Func<TCollaborator, TResult>> askFunction, 
                                                                       VerifyException originalException)
            where TSut          : class
            where TCollaborator : class
        {
            var message = ExceptionMessageBuilder.CreateDidntAskMessage<TSut, TCollaborator, TResult>(askFunction);
            throw new DidntAskException(message, originalException);
        }

        public static void ThrowDidntGet<TSut, TCollaborator, TResult>(Expression<Func<TCollaborator, TResult>> getFunction,
                                                                        VerifyException originalException)
            where TSut          : class
            where TCollaborator : class
        {
            var message = ExceptionMessageBuilder.CreateDidntGetMessage<TSut, TCollaborator, TResult>(getFunction);
            throw new DidntGetException(message, originalException);
        }

        public static void ThrowDidntSet<TSut, TCollaborator>(Action<TCollaborator> setAction,
                                                              VerifyException originalException)
            where TSut          : class
            where TCollaborator : class
        {
            var message = ExceptionMessageBuilder.CreateDidntSetMessage<TSut, TCollaborator>(setAction);
            throw new DidntSetException(message, originalException);
        }

        public static void ThrowDidntThrow<TSut>(Type exceptionType)
        {
            var message = ExceptionMessageBuilder.CreateDidntThrowMessage<TSut>(exceptionType);
            throw new DidntThrowException(message);
        }

        public static void ThrowDidntReturn<TSut, TResult>(TResult result)
        {
            var message = ExceptionMessageBuilder.CreateDidntReturnMessage<TSut>(result);
            throw new DidntReturnException(message);
        }
    }
}
