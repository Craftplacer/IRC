using System;

namespace Craftplacer.IRC.Events
{
    public class DisconnectedEventArgs : IrcEventArgs
    {
        public DisconnectedEventArgs(string reason, IrcClient client) : base(client)
        {
            Reason = reason;
        }

        public DisconnectedEventArgs(Exception exception, IrcClient client) : base(client)
        {
            Reason = (Exception = exception)?.Message;
        }

        public Exception Exception { get; }
        public string Reason { get; }
    }
}