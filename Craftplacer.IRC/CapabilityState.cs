using System;

namespace Craftplacer.IRC
{
    [Flags]
    public enum CapabilityState
    {
        Disabled,

        OfferedByServer,

        OfferedByClient,

        /// <summary>
        /// This capability is supported by both the server and client, and has been enabled.
        /// </summary>
        Acknowledged,
    }
}