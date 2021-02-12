using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Entities
{
    public class IrcMessage : IrcEntity
    {
        public IrcMessage(IrcClient client, RawMessage raw) : base(client)
        {
            var target = raw.Parameters[0];
            if (target[0] == '#')
            {
                Channel = new IrcChannel()
                {
                    Name = target
                };
            }

            User = new IrcUser(client, raw.Source);

            Body = raw.Parameters[1];

            if (raw.Tags.TryGetValue("msgid", out var id))
            {
                Id = id;
            }
        }

        public string Body { get; set; }
        public IrcChannel Channel { get; }

        /// <summary>
        /// The Id of this message.
        /// </summary>
        /// <remarks>
        /// This property only gets set on servers supporting the "message-ids" IRCv3 capability.
        /// </remarks>
        [MaybeNull]
        public string Id { get; set; }

        public IrcUser User { get; }

        /// <summary>
        /// Replies to this message.
        /// </summary>
        public async Task ReplyAsync()
        {
            await Client.SendMessageAsync(Channel.Name, Body);
        }
    }
}