using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Messages
{
    public class PassMessage : IrcMessage
    {
        public PassMessage(string password)
        {
            Password = password;
        }

        public string Password { get; }
        
        public override RawMessage ProtocolMessage => new RawMessage("PASS", Password);
    }
}