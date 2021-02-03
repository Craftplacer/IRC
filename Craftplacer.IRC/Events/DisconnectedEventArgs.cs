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
        
        public string Reason { get; }
        
        public Exception Exception { get; }
    }
}