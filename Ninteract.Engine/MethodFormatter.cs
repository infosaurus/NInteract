// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    internal static class MethodFormatter
    {
        public static string GetShortFormattedMethodCall<T>(Expression<Action<T>> tellAction) 
            where T : class
        {
            var tellMethodCall = ((MethodCallExpression)tellAction.Body);
            return GetShortFormattedMethodCall(tellMethodCall);
        }

        public static string GetShortFormattedMethodCall<T, TResult>(Expression<Func<T, TResult>> askFunction)
            where T : class
        {
            var askMethodCall = ((MethodCallExpression)askFunction.Body);
            return GetShortFormattedMethodCall(askMethodCall);
        }

        public static string GetShortFormattedMethodCall<T>(Expression<Predicate<T>> predicate)
        {
            return predicate.Body.ToString();
        }

        public static string GetFormattedMethodCallWithReturnType<T, TResult>(Expression<Func<T, TResult>> askFunction)
            where T : class
        {
            var askMethodCall = ((MethodCallExpression)askFunction.Body);
            return GetFormattedMethodCallWithReturnType(askMethodCall);
        }

        public static string GetFormattedPropertyCall<T, TResult>(Expression<Func<T, TResult>> propertyCall)
            where T : class
        {
            var propertyAccess = ((MemberExpression)propertyCall.Body);
            return propertyAccess.Member.Name;
        }

        private static string GetShortFormattedMethodCall(MethodCallExpression methodCall)
        {
            var parameters = GetFormattedParameters(methodCall);
            var formattedMethodName = GetFormattedMethodName(methodCall);
            return string.Format("{0}({1})",
                                 formattedMethodName,
                                 String.Join(",", parameters));
        }

        private static string GetFormattedMethodCallWithReturnType(MethodCallExpression methodCall)
        {
            return string.Format("{0} {1}", methodCall.Method.ReturnType.Name, GetShortFormattedMethodCall(methodCall));
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
                    parameters.Add(GetShortFormattedMethodCall((MethodCallExpression) lambda.Body));
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