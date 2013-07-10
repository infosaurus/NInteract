﻿using System;
using System.Linq.Expressions;
using Moq;
using Ninteract.Engine;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Adapters
{
    public class MoqFake<T> : IFake<T> where T : class
    {
        private Mock<T> _moqFake;

        public T Illusion { get { return _moqFake.Object; } }

        public MoqFake()
        {
            _moqFake = new Mock<T>();
        }

        public void SetupReturns<TResult>(Expression<Func<T, TResult>> function, TResult result)
        {
            _moqFake.Setup(function).Returns(result);
        }

        public void Verify(Expression<Action<T>> expression)
        {
            try
            {
                _moqFake.Verify(expression);
            }
            catch (Moq.MockException moqException)
            {
                throw new VerifyException(moqException);
            }
        }

        public void Verify<TResult>(Expression<Func<T, TResult>> expression)
        {
            try
            {
                _moqFake.Verify(expression);
            }
            catch (Moq.MockException moqException)
            {
                throw new VerifyException(moqException);
            }
        }

        public void VerifyGet<TResult>(Expression<Func<T, TResult>> expression)
        {
            try
            {
                _moqFake.VerifyGet(expression);
            }
            catch (Moq.MockException moqException)
            {
                throw new VerifyException(moqException);
            }
        }

        public void VerifySet(Action<T> setAction)
        {
            try
            {
                _moqFake.VerifySet(setAction);
            }
            catch (Moq.MockException moqException)
            {
                throw new VerifyException(moqException);
            }
        }
    }
}