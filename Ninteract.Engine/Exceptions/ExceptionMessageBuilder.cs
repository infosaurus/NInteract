// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract.Engine.Exceptions
{
    internal static class ExceptionMessageBuilder
    {
        internal static string CreateDidntTellMessage<TSut, TCollaborator>(Expression<Action<TCollaborator>> tellAction)
            where TSut          : class
            where TCollaborator : class
        {
            var formattedMethodCall = MethodFormatter.GetShortFormattedMethodCall(tellAction);
            var message = string.Format("The {0} under test didn't tell its {1} collaborator : {2}.",
                                        typeof(TSut).Name,
                                        typeof(TCollaborator).Name,
                                        formattedMethodCall);
            return message;
        }

        public static string CreateDidntAskMessage<TSut, TCollaborator, TResult>(Expression<Func<TCollaborator, TResult>> askFunction)
            where TSut          : class
            where TCollaborator : class
        {
            var formattedMethodCall = MethodFormatter.GetShortFormattedMethodCall(askFunction);
            var message = string.Format("The {0} under test didn't ask its {1} collaborator : {2}.",
                                        typeof(TSut).Name,
                                        typeof(TCollaborator).Name,
                                        formattedMethodCall);
            return message;
        }

        public static string CreateDidntGetMessage<TSut, TCollaborator, TResult>(Expression<Func<TCollaborator, TResult>> getFunction)
            where TSut          : class
            where TCollaborator : class
        {
            var formattedMethodCall = MethodFormatter.GetFormattedPropertyCall(getFunction);
            var message = string.Format("The {0} under test didn't get its {1} collaborator's {2}.",
                                        typeof(TSut).Name,
                                        typeof(TCollaborator).Name,
                                        formattedMethodCall);
            return message;
        }

        public static string CreateDidntSetMessage<TSut, TCollaborator>(Action<TCollaborator> setAction)
            where TSut : class
            where TCollaborator : class
        {
            var message = string.Format("The {0} under test didn't set one of its {1} collaborator's properties.",
                                        typeof(TSut).Name,
                                        typeof(TCollaborator).Name);
            return message;
        }

        public static string CreateDidntThrowMessage<TSut>(Type exceptionType)
        {
            return string.Format("The {0} under test didn't throw a(n) {1}.",
                                 typeof (TSut).Name,
                                 exceptionType.Name);
        }

        public static string CreateDidntReturnMessage<TSut>(object result)
        {
            return string.Format("The {0} under test didn't return {1}", typeof (TSut).Name, result);
        }
    }
}