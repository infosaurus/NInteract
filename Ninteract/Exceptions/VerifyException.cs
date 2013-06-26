using System;

namespace NInteract
{
    public class VerifyException : Exception
    {
        public VerifyException(Exception innerException) : base(string.Empty, innerException)
        {

        }
    }
}