using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Exceptions
{
    public class IrcException
    {
        public IrcException(ServerReply reply, string v, RawMessage response, RawMessage request)
        {
            Reply = reply;
            Message = v;
            Response = response;
            Request = request;
        }

        public ServerReply Reply { get; }
        public string Message { get; }
        public RawMessage Response { get; }
        public RawMessage Request { get; }
    }
}