﻿using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Messages
{
    public class UserMessage : IrcProtocolMessage
    {
        public UserMessage(string username, string realName)
        {
            Username = username;
            RealName = realName;
        }

        public string Username { get; }
        public string RealName { get; }

        public override RawMessage ProtocolMessage => new RawMessage("USER", Username, "0", "*", RealName);
    }
}