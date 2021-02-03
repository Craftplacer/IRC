using System;

namespace Craftplacer.IRC.Events.Raw
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