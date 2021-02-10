using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Entities;

namespace Craftplacer.IRC.Events
{
    public class UserPartedEventArgs : IrcEventArgs
    {
        public UserPartedEventArgs([NotNull] IrcClient client, IrcUser user) : base(client)
        {
            UserParted = user;
        }

        public IrcUser UserParted { get; }
    }
}