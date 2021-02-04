using System;

namespace Craftplacer.IRC.Raw.Events
{
    public class RawEventArgs : EventArgs
    {
        public RawEventArgs(RawIrcClient client)
        {
            Client = client;
        }

        public RawIrcClient Client { get; }
    }
}