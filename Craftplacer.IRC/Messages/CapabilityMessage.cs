using System.Collections.Generic;
using System.Linq;

namespace Craftplacer.IRC.Messages
{
    public class CapabilityMessage : IrcMessage
    {
        public CapabilityMessage(string subCommand, params string[] parameters)
        {
            SubCommand = subCommand;
            Parameters = parameters.ToList();
        }

        public string SubCommand { get; set; }
        
        public List<string> Parameters { get; set; }

        public override RawMessage ProtocolMessage
        {
            get
            {
                return new RawMessage("CAP", new[] { SubCommand }.Concat(Parameters).ToArray());
            }
        }
    }
}