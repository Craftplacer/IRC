using System.Collections.Generic;

namespace Craftplacer.IRC.Messages
{
    public class RawMessage
    {
        public RawMessage(string command, params string[] parameters)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new System.ArgumentException($"'{nameof(command)}' cannot be null or whitespace", nameof(command));
            
            this.Command = command;
            this.Parameters = parameters;
        }

        public string Command { get; }
        public IEnumerable<string> Parameters { get; }
    }
}
