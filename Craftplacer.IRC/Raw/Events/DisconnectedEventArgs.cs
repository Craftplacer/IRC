using System;
using System.Diagnostics.CodeAnalysis;

namespace Craftplacer.IRC.Raw.Events
{
    public class DisconnectedEventArgs : RawEventArgs
    {
        public DisconnectedEventArgs(RawIrcClient client, Exception exception) : base(client)
        {
            Exception = exception;
        }

        [MaybeNull]
        public Exception Exception { get; }
    }
}