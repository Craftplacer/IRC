using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Messages
{
    public class TagMessage : IrcProtocolMessage
    {
        public override RawMessage ProtocolMessage => new RawMessage(
            "TAGMSG",
            ""
            );
    }
}