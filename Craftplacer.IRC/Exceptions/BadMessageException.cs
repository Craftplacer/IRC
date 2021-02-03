using System;

namespace Craftplacer.IRC.Exceptions
{
    public class BadMessageException : Exception
    {
        public BadMessageException(string cause) : base("The message is badly formatted: " + cause)
        {
        }
    }
}