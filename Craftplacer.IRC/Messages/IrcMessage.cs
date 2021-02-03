namespace Craftplacer.IRC.Messages
{
    /// <summary>
    /// Base class allowing of de-/serialization of messages. 
    /// </summary>
    public abstract class IrcMessage
    {
        public abstract RawMessage ProtocolMessage { get; }
    }
}