using System;
using System.Linq.Expressions;

namespace NInteract.Exceptions
{
    internal static class ExceptionThrower
    {
        public static void ThrowDidntTell<TSut, TCollaborator>(Expression<Action<TCollaborator>> tellAction, 
                                                               VerifyException originalException)
            where TSut          : class
            where TCollaborator : class
        {
            var message = ExceptionMessageBuilder.GetDidntTellMessage<TSut, TCollaborator>(tellAction);
            throw new DidntTellException(message, originalException);
        }

        public static void ThrowDidntAsk<TSut, TCollaborator, TResult>(Expression<Func<TCollaborator, TResult>> askFunction, 
                                                                       VerifyException originalException)
            where TSut          : class
            where TCollaborator : class
        {
            var message = ExceptionMessageBuilder.GetDidntAskMessage<TSut, TCollaborator, TResult>(askFunction);
            throw new DidntAskException(message, originalException);
        }
    }
}
