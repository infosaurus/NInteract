// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

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