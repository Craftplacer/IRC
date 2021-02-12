using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Craftplacer.IRC.Entities;
using Craftplacer.IRC.Events;
using Craftplacer.IRC.Helpers;
using Craftplacer.IRC.Raw;
using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC
{
    /// <summary>
    /// A "managed" IRC client. It wraps around RawIrcClient and provides a more user-friendly interface for the modern IRC protocol.
    /// </summary>
    // TODO: Require setting nick and real name.
    public partial class IrcClient : IDisposable
    {
        private static readonly string[] _supportedCapabilities = new string[]
        {
            CapabilityConstants.MessageTags,
            CapabilityConstants.MessageIDs,
            CapabilityConstants.EchoMessage,
        };

        private readonly ConcurrentDictionary<Predicate<RawMessage>, TaskCompletionSource<RawMessage>> _expectedMessages;
        private string[] _acknowledgedCapabilities;
        private StringBuilder _motdBuffer;
        private bool _negotiatingCapabilities = false;
        private string _nickname;
        private string _password;
        private string _realName;
        private string[] _serverCapabilities;

        /// <summary>
        /// Sends a message and waits for an incoming message over a condition.
        /// </summary>
        private async Task<RawMessage> SendRequest(RawMessage sendingMessage, Predicate<RawMessage> predicate)
        {
            var tcs = new TaskCompletionSource<RawMessage>();

            if (!_expectedMessages.TryAdd(predicate, tcs))
            {
                throw new Exception("An expected message with the same condition already exists.");
            }

            await Raw.SendMessageAsync(sendingMessage);

            return await tcs.Task;
        }

        internal bool HasCapability(string capability)
        {
            return _serverCapabilities.Contains(capability.ToLowerInvariant());
        }

        public IrcClient()
        {
            _expectedMessages = new ConcurrentDictionary<Predicate<RawMessage>, TaskCompletionSource<RawMessage>>(2, 5);

            Raw = new RawIrcClient();
            Raw.MessageReceived += Raw_MessageReceived;
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// The server welcomed the client and is willing to operate.
        /// </summary>
        public event EventHandler Welcome;

        public bool Connected => Raw.Connected;

        /// <summary>
        /// The last "Message of the Day" sent by the server.
        /// </summary>
        public string Motd { get; private set; }

        /// <summary>
        /// The nickname of the client. Its value will be used when connecting to the server and can't be changed once connected.
        /// </summary>
        public string Nickname
        {
            get => _nickname;
            set
            {
                if (Connected)
                {
                    throw new InvalidOperationException($"You can't change your nickname while connected. Use {nameof(ChangeNicknameAsync)}() instead.");
                }

                _nickname = value;
            }
        }

        /// <summary>
        /// The password required to authenticate with the server. Its value will be used when connecting to the server and can't be changed once connected.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                if (Connected)
                {
                    throw new InvalidOperationException($"You can't change the password while connected.");
                }

                _password = value;
            }
        }

        public RawIrcClient Raw { get; }

        public string RealName
        {
            get => _realName;
            set
            {
                if (Connected)
                {
                    throw new InvalidOperationException($"You can't change your real name while connected.");
                }

                _realName = value;
            }
        }

        public async Task ChangeNicknameAsync(string nickname)
        {
            if (!Connected)
            {
                throw new NotConnectedException();
            }

            await Raw.SendMessageAsync(new RawMessage("NICK", nickname));
        }

        /// <summary>
        /// Connects to an IRC server.
        /// </summary>
        /// <param name="host">The hostname of the server you want to connect to.</param>
        /// <param name="port">The port of the server you want to connect to. If <see cref="null"/>, the default port appropriate for the <paramref name="ssl"/> parameter will be used.</param>
        /// <param name="ssl">Whether to connect with SSL underlying.</param>
        public async Task ConnectAsync(string host, int? port = null, bool ssl = false)
        {
            if (!port.HasValue)
            {
                port = ssl ? 6697 : 6667;
            }

            await Raw.ConnectAsync(host, port.Value, ssl);

            // https://modern.ircdocs.horse/#connection-registration
            await Raw.SendMessageAsync(new RawMessage("CAP", "LS", "302"));

            if (Password != null)
            {
                await Raw.SendMessageAsync(new RawMessage("PASS", parameters: Password));
            }

            await Raw.SendMessageAsync(new RawMessage("NICK", parameters: Nickname));
            // TODO: Use a separate property for username.
            await Raw.SendMessageAsync(new RawMessage("USER", Nickname, "*", "*", RealName));
        }

        public void Dispose()
        {
            ((IDisposable)Raw).Dispose();
        }

        /// <summary>
        /// Sends a message to the specified target.
        /// </summary>
        /// <param name="target">The channel or user where the message will be sent to. Use # to specify a channel.</param>
        /// <param name="message">The message to send</param>
        /// <returns>An <see cref="IrcMessage"/> if the server supports "echo-message" otherwise <see cref="null"/>.</returns>
        public async Task<IrcMessage> SendMessageAsync(string target, string message)
        {
            var privMsg = new RawMessage("PRIVMSG", target, message);

            if (HasCapability(CapabilityConstants.EchoMessage))
            {
                var arrivedMessage = await SendRequest(privMsg, (r) =>
                {
                    return r.Command == "PRIVMSG" && Utilities.ExtractHostmask(r.Source).Username == Nickname;
                });
                return new IrcMessage(this, arrivedMessage);
            }
            else
            {
                await Raw.SendMessageAsync(privMsg);
                return null;
            }
        }
    }
}