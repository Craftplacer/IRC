using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Messages;

namespace Craftplacer.IRC.Events
{
    public class MessageReceivedEventArgs : IrcEventArgs
    {
        public MessageReceivedEventArgs([NotNull] IrcClient client, [NotNull] IrcProtocolMessage message) : base(client)
        {
            Message = message;
        }
        
        public IrcProtocolMessage Message { get; }
    }
}