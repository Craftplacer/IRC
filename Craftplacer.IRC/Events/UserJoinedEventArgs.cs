using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Entities;

namespace Craftplacer.IRC.Events
{
    public class UserJoinedEventArgs : IrcEventArgs
    {
        public UserJoinedEventArgs([NotNull] IrcClient client, IrcUser user, IrcChannel channel) : base(client)
        {
            UserJoined = user;
            Channel = channel;
        }

        public IrcUser UserJoined { get; }

        public IrcChannel Channel { get; }
    }
}