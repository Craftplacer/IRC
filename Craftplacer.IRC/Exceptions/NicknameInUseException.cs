using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Messages;


namespace Craftplacer.IRC.Exceptions
{
    public class NicknameInUseException : IrcException
    {
        public NicknameInUseException(string message, RawMessage response, [MaybeNull] RawMessage request = null)
            : base(ServerReply.ERR_NICKNAMEINUSE, message ?? "Nickname is already in use", response, request)
        {
        }
    }
}