﻿using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC
{
    /// <summary>
    /// Base class allowing of de-/serialization of messages.
    /// </summary>
    public abstract class IrcProtocolMessage
    {
        public abstract RawMessage ProtocolMessage { get; }
    }
}