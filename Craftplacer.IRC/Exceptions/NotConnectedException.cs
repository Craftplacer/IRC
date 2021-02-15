using System;

namespace Craftplacer.IRC.Exceptions
{
    [Serializable]
    public class NotConnectedException : InvalidOperationException
    {
        public NotConnectedException() : base("You can't perform this operation unless you are connected.")
        {
        }
    }
}