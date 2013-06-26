using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NInteract.Exceptions
{
    internal static class ExceptionMessageBuilder
    {
        internal static string GetDidntTellMessage<TSut, TCollaborator>(Expression<Action<TCollaborator>> tellAction)
            where TSut          : class
            where TCollaborator : class
        {
            var formattedMethodCall = GetFormattedMethodCall(tellAction);
            var message = string.Format("The {0} under test didn't tell its {1} collaborator : {2}.",
                                        typeof(TSut).Name,
                                        typeof(TCollaborator).Name,
                                        formattedMethodCall);
            return message;
        }

        public static string GetDidntAskMessage<TSut, TCollaborator, TResult>(Expression<Func<TCollaborator, TResult>> askFunction)
            where TSut          : class
            where TCollaborator : class
        {
            var formattedMethodCall = GetFormattedMethodCall(askFunction);
            var message = string.Format("The {0} under test didn't ask its {1} collaborator : {2}.",
                                        typeof(TSut).Name,
                                        typeof(TCollaborator).Name,
                                        formattedMethodCall);
            return message;
        }

        private static string GetFormattedMethodCall<TCollaborator>(Expression<Action<TCollaborator>> tellAction)
            where TCollaborator : class
        {
            var tellMethodCall = ((MethodCallExpression)tellAction.Body);
            return GetFormattedMethodCall(tellMethodCall);
        }
        private static string GetFormattedMethodCall<TCollaborator, TResult>(Expression<Func<TCollaborator, TResult>> askFunction)
            where TCollaborator : class
        {
            var tellMethodCall = ((MethodCallExpression)askFunction.Body);
            return GetFormattedMethodCall(tellMethodCall);
        }

        private static string GetFormattedMethodCall(MethodCallExpression methodCall)
        {
            var parameters = GetFormattedParameters(methodCall);
            var formattedMethodName = GetFormattedMethodName(methodCall);
            return string.Format("{0}({1})",
                                formattedMethodName,
                                string.Join(",", parameters));
        }

        private static string GetFormattedMethodName(MethodCallExpression methodCall)
        {
            string formattedMethodName;
            if (methodCall.Method.IsGenericMethod)
            {
                formattedMethodName = string.Format("{0}<{1}>",
                                                    methodCall.Method.Name,
                                                    string.Join(",", methodCall.Method.GetGenericArguments()
                                                                                      .Select(type => type.Name)));
            }
            else
            {
                formattedMethodName = methodCall.Method.Name;
            }
            return formattedMethodName;
        }

        private static IEnumerable<string> GetFormattedParameters(MethodCallExpression methodCall)
        {
            var parameters = new List<string>();
            foreach (Expression argument in methodCall.Arguments)
            {
                if (argument == null)
                {
                    parameters.Add("null");
                }
                else if (argument.NodeType == ExpressionType.Call)
                {
                    LambdaExpression lambda = Expression.Lambda(argument);
                    parameters.Add(GetFormattedMethodCall((MethodCallExpression) lambda.Body));
                }
                else
                {
                    parameters.Add(argument.ToString());
                }
            }
            return parameters;
        }
    }
}