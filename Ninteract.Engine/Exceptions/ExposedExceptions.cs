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

    public class DidntGetException : Exception
    {
        public DidntGetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class DidntSetException : Exception
    {
        public DidntSetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }    
    
    public class DidntThrowException : Exception
    {
        public DidntThrowException(string message)
            : base(message, null)
        {
        }
    }
}