using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Exceptions
{
    public class ErreoneusNicknameException : IrcException
    {
        public ErreoneusNicknameException(string message, RawMessage response, [MaybeNull] RawMessage request = null)
            : base(ServerReply.ERR_ERRONEUSNICKNAME, message ?? "The nickname contains invalid characters", response, request)
        {
        }
    }
}