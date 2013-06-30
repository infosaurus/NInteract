using System;

namespace Ninteract.Engine.Exceptions
{
    public class VerifyException : Exception
    {
        public VerifyException(Exception innerException) : base(string.Empty, innerException)
        {

        }
    }
}