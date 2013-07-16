// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Engine
{
    /// <summary>
    /// An expectation than wraps around the stimulus call and monitors it
    /// </summary>
    public interface IEncompassingExpectation
    {
        void Enqueue(IEncompassingExpectation next);

        void TriggerAndVerify<TSut>(Stimulus<TSut> stimulus, TSut sut)
            where TSut : class;
    }

    public abstract class EncompassingExpectation : IEncompassingExpectation
    {
        protected IEncompassingExpectation _next;

        public void Enqueue(IEncompassingExpectation next)
        {
            _next = next;
        }

        protected void TriggerStimulus<TSut>(Stimulus<TSut> stimulus, TSut sut)
            where TSut : class
        {
            if (_next == null)
                stimulus.ApplyTo(sut);
            else
                _next.TriggerAndVerify(stimulus, sut);
        }

        public abstract void TriggerAndVerify<TSut>(Stimulus<TSut> stimulus, TSut sut)
            where TSut : class;
    }

    public class ThrowExpectation<TException> : EncompassingExpectation where TException : Exception
    {
        public override void TriggerAndVerify<TSut>(Stimulus<TSut> stimulus, TSut sut)
        {
            var expectedExceptionWasThrown = false;
            try
            {
                TriggerStimulus(stimulus, sut);
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
        }
    }
}
