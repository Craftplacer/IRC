using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Exceptions
{
    public class NoNicknameGivenException : IrcException
    {
        public NoNicknameGivenException(string message, RawMessage response, [MaybeNull] RawMessage request = null)
            : base(ServerReply.ERR_NONICKNAMEGIVEN, message ?? "", response, request)
        {
        }
    }
}