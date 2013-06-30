using System;

namespace Ninteract.Engine.Exceptions
{
    public class DidntTellException : Exception
    {
        public DidntTellException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class DidntAskException : Exception
    {
        public DidntAskException(string message, Exception innerException) : base(message, innerException)
        { 
        }
    }
}