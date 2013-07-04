using System;
using System.Linq.Expressions;
using Ninteract.Engine.Exceptions;

namespace Ninteract.Engine
{
    public interface IExpectation<TCollaborator> where TCollaborator : class 
    {
        void VerifyAgainst(IFake<TCollaborator> fake);
    }

    public class TellExpectation<TSut, TCollaborator> : IExpectation<TCollaborator> where TCollaborator : class 
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

    public class AskExpectation<TSut, TCollaborator, TResult> : IExpectation<TCollaborator> where TCollaborator : class
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
}