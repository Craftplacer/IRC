using Craftplacer.IRC.Entities;
using System.Diagnostics.CodeAnalysis;

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