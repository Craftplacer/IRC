using System.Collections.Generic;
using System.Linq;

using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Messages
{
    public class CapabilityMessage : IrcProtocolMessage
    {
        public CapabilityMessage(string subCommand, params string[] parameters)
        {
            SubCommand = subCommand;
            Parameters = parameters.ToList();
        }

        public List<string> Parameters { get; set; }
        public override RawMessage ProtocolMessage => new RawMessage("CAP", new[] { SubCommand }.Concat(Parameters).ToArray());
        public string SubCommand { get; set; }
    }
}