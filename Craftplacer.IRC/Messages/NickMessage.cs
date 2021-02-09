using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Messages
{
    public class NickMessage : IrcProtocolMessage
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