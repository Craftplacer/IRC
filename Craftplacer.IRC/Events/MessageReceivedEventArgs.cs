﻿using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Entities;

namespace Craftplacer.IRC.Events
{
    public class MessageReceivedEventArgs : IrcEventArgs
    {
        public MessageReceivedEventArgs([NotNull] IrcClient client, [NotNull] IrcMessage message) : base(client)
        {
            Message = message;
        }

        public IrcMessage Message { get; }
    }
}