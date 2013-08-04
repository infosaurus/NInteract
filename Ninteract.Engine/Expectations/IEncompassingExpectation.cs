// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Engine
{
    /// <summary>
    /// An expectation than wraps around the stimulus call and monitors it
    /// </summary>
    public interface IEncompassingExpectation
    {
        void Enqueue(IEncompassingExpectation next);

        object TriggerAndVerify<TSut>(Stimulus<TSut> stimulus, TSut sut)
            where TSut : class;
    }

    public abstract class EncompassingExpectation : IEncompassingExpectation
    {
        protected IEncompassingExpectation _next;

        public void Enqueue(IEncompassingExpectation next)
        {
            _next = next;
        }

        protected object TriggerStimulus<TSut>(Stimulus<TSut> stimulus, TSut sut)
            where TSut : class
        {
            if (_next == null)
                return stimulus.ApplyTo(sut);
            else
                return _next.TriggerAndVerify(stimulus, sut);
        }

        public abstract object TriggerAndVerify<TSut>(Stimulus<TSut> stimulus, TSut sut)
            where TSut : class;
    }

    public class ThrowExpectation<TException> : EncompassingExpectation where TException : Exception
    {
        public override object TriggerAndVerify<TSut>(Stimulus<TSut> stimulus, TSut sut)
        {
            var expectedExceptionWasThrown = false;
            object result = null;
            try
            {
                result = TriggerStimulus(stimulus, sut);
            }
            catch (Exception exception)
            {
                // Original exception is embedded in dynamic invocation exception
                if (exception.InnerException is TException)
                    expectedExceptionWasThrown = true;

                // Silence expected exception so that test doesn't fail
            }

            if (!expectedExceptionWasThrown)
                ExceptionThrower.ThrowDidntThrow<TSut>(typeof(TException));

            return result;
        }
    }

    public class ReturnsValueExpectation<TResult> : EncompassingExpectation
    {
        private readonly TResult _expectedReturnValue;

        public ReturnsValueExpectation(TResult expectedReturnValue)
        {
            _expectedReturnValue = expectedReturnValue;
        }

        public override object TriggerAndVerify<TSut>(Stimulus<TSut> stimulus, TSut sut)
        {
            var value = TriggerStimulus(stimulus, sut);
            if (value == null)
            {
                if (_expectedReturnValue != null)
                    ExceptionThrower.ThrowDidntReturn<TSut, TResult>(_expectedReturnValue);
            }
            else
            {

                TResult result;
                try
                {
                    result = (TResult) value;
                }
                catch (Exception)
                {
                    throw new InvalidAssertionTargetException(
                        string.Format("Expected value type {0} doesn't match actual method return type : {1}.",
                                      typeof (TResult).Name,
                                      stimulus.ToString()));
                }

                if (!result.Equals(_expectedReturnValue))
                {
                    ExceptionThrower.ThrowDidntReturn<TSut, TResult>(_expectedReturnValue);
                }
            }
            return value;
        }
    }

    public class ReturnsPredicateExpectation<TResult> : EncompassingExpectation
    {
        private readonly Expression<Predicate<TResult>> _returnValueExpectation;

        public ReturnsPredicateExpectation(Expression<Predicate<TResult>> returnValueExpectation)
        {
            _returnValueExpectation = returnValueExpectation;
        }

        public override object TriggerAndVerify<TSut>(Stimulus<TSut> stimulus, TSut sut)
        {
            var value = TriggerStimulus(stimulus, sut);
                        TResult result;
            try
            {
                result = (TResult)value;
            }
            catch (Exception)
            {
                throw new InvalidAssertionTargetException(
                    string.Format("Expected value type {0} doesn't match actual method return type : {1}.",
                                  typeof(TResult).Name,
                                  stimulus.ToString()));
            }

            if (!_returnValueExpectation.Compile().Invoke(result))
            {
                ExceptionThrower.ThrowDidntReturn<TSut, TResult>(_returnValueExpectation);
            }
            
            return value;
        }
    }
}
