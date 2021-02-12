using System;
using System.Diagnostics.CodeAnalysis;

namespace Craftplacer.IRC.Events
{
    public class IrcEventArgs : EventArgs
    {
        public IrcEventArgs([NotNull] IrcClient client)
        {
            Client = client;
        }

        public IrcClient Client { get; }
    }
}