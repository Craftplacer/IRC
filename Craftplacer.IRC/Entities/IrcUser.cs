using System;
using System.Threading.Tasks;
using Craftplacer.IRC.Helpers;

namespace Craftplacer.IRC.Entities
{
    public class IrcUser : IrcEntity
    {
        public IrcUser(IrcClient client, string hostmask) : base(client)
        {
            (Nickname, Username, Host) = Utilities.ExtractHostmask(hostmask);
        }
        
        public string Host { get; }

        public string Nickname { get; }
        
        public string Username { get; }

        public async Task SendMessageAsync(string message)
        {
            await Client.SendMessageAsync(Username, message);
        }
    }
}