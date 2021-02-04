using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Raw.Events
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