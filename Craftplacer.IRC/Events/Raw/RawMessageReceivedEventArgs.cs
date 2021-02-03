using Craftplacer.IRC.Messages;

namespace Craftplacer.IRC.Events.Raw
{
    public class RawMessageReceivedEventArgs : RawEventArgs
    {
        public RawMessageReceivedEventArgs(RawIrcClient client, RawMessage message) : base(client)
        {
            Message = message;
        }
        
        public RawMessage Message { get; }
    }
}