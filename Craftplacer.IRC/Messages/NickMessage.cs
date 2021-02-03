using System.Diagnostics.CodeAnalysis;

namespace Craftplacer.IRC.Messages
{
    public class NickMessage : IrcMessage
    {
        public NickMessage([NotNull] string nickname)
        {
            Nickname = nickname;
        }

        [NotNull]
        public string Nickname { get; set; }
        
        public override RawMessage ProtocolMessage => new RawMessage("NICK", Nickname);
    }
}