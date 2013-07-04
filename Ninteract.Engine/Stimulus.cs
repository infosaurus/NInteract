using System;
using System.Linq.Expressions;

namespace Ninteract.Engine
{
    public class Stimulus<TSut>
    {
        private readonly LambdaExpression _sutCall;

        public Stimulus(Expression<Action<TSut>> sutCall)
        {
            _sutCall = sutCall;
        }

        public void ApplyTo(TSut sut)
        {
            _sutCall.Compile().DynamicInvoke(sut);
        }
    }
}