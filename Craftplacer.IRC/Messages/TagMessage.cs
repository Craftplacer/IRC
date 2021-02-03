namespace Craftplacer.IRC.Messages
{
    public class TagMessage : IrcMessage
    {
        public override RawMessage ProtocolMessage => new RawMessage(
            "TAGMSG",
            ""
            );
    }
}