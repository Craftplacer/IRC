using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Messages;


namespace Craftplacer.IRC.Exceptions
{
    public class NicknameCollisionException : IrcException
    {
        public NicknameCollisionException(string message, RawMessage response, [MaybeNull] RawMessage request = null)
            : base(ServerReply.ERR_NICKCOLLISION, message ?? "Nickname collision", response, request)
        {
        }
    }
}