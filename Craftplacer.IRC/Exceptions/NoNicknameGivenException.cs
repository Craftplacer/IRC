using Craftplacer.IRC.Raw.Messages;

using System.Diagnostics.CodeAnalysis;

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