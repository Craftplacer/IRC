using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Craftplacer.IRC.Entities;
using Craftplacer.IRC.Events;
using Craftplacer.IRC.Raw.Events;
using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC
{
    [SuppressMessage("ReSharper", "PossiblyMistakenUseOfParamsMethod", Justification = "System.Diagnostics.Debug is a bitch")]
    public partial class IrcClient
    {
        private bool CheckForExpected(RawMessage raw)
        {
            var matchingKey = _expectedMessages.Keys.FirstOrDefault(p => p.Invoke(raw));
            if (matchingKey == null)
            {
                return false;
            }
            else
            {
                if (_expectedMessages.TryRemove(matchingKey, out var tcs))
                {
                    tcs.SetResult(raw);
                    return true;
                }
                else
                {
                    Debug.WriteLine("Failed to retrieve TCS for expected message");
                    return false;
                }
            }
        }

        private async void Raw_MessageReceived(object sender, RawMessageReceivedEventArgs e)
        {
            // Check if the received message is a server reply or a command.
            if (ushort.TryParse(e.Message.Command, out var replyNum))
            {
                var reply = (ServerReply)replyNum;
                HandleServerReply(reply, e.Message);
            }
            else
            {
                await HandleCommand(e.Message);
            }
        }

        /// <summary>
        /// Handles a message with a numeric server reply like <see cref="ServerReply.RPL_WELCOME"/>.
        /// </summary>
        private void HandleServerReply(ServerReply reply, RawMessage message)
        {
            switch (reply)
            {
                case ServerReply.RPL_WELCOME:
                {
                    Welcome?.Invoke(this, EventArgs.Empty);
                    break;
                }

                case ServerReply.RPL_MOTD:
                {
                    _motdBuffer?.AppendLine(message.Parameters[1]);
                    break;
                }

                case ServerReply.RPL_MOTDSTART:
                {
                    _motdBuffer ??= new StringBuilder();
                    _motdBuffer.AppendLine(message.Parameters[1]);
                    break;
                }

                case ServerReply.RPL_MOTDEND:
                {
                    Motd = _motdBuffer.ToString();
                    _motdBuffer.Clear();
                    break;
                }

                case ServerReply.RPL_LUSERCLIENT:
                case ServerReply.RPL_LUSEROP:
                case ServerReply.RPL_LUSERUNKNOWN:
                case ServerReply.RPL_LUSERCHANNELS:
                case ServerReply.RPL_LUSERME:
                case ServerReply.RPL_LOCALUSERS:
                case ServerReply.RPL_GLOBALUSERS:
                {
                    // Those reply contain strings intended for humans, thus can't be parsed.
                    break;
                }

                default:
                {
                    Debug.WriteLine("Unhandled IRC server reply: {1} ({0}) {2}", message.Command, reply, string.Join(' ', message.Parameters));
                    break;
                }
            }
        }

        /// <summary>
        /// Handles a message with a command like "PRIVMSG"
        /// </summary>
        private async Task HandleCommand(RawMessage message)
        {
            switch (message.Command)
            {
                case "CAP":
                {
                    var capCommand = message.Parameters[1];
                    switch (capCommand)
                    {
                        case "LS":
                        {
                            if (_negotiatingCapabilities)
                            {
                                break;
                            }

                            _serverCapabilities = message.Parameters[2].Split(' ').ToArray();
                            Debug.WriteLine("Offered capabilities: {0}", args: string.Join(", ", _serverCapabilities));

                            var supportedCapabilities = _serverCapabilities
                                    .Where(sc => _supportedCapabilities.Contains(sc))
                                    .ToArray();

                            if (supportedCapabilities.Any())
                            {
                                _negotiatingCapabilities = true;
                                await Raw.SendMessageAsync(new RawMessage("CAP", "REQ",
                                    string.Join(' ', supportedCapabilities)));
                            }
                            else
                            {
                                // We don't support any of the offered capabilities, we end here.
                                await Raw.SendMessageAsync(new RawMessage("CAP", "END"));
                                Debug.WriteLine("Negotiated no capabilities");
                            }

                            break;
                        }

                        case "ACK":
                        {
                            if (!_negotiatingCapabilities)
                            {
                                break;
                            }

                            _acknowledgedCapabilities = message.Parameters[2].Split(' ');
                            await Raw.SendMessageAsync(new RawMessage("CAP", "END"));

                            _negotiatingCapabilities = false;
                            Debug.WriteLine("Finished negotiating capabilities: {0}",
                                args: string.Join(", ", _acknowledgedCapabilities));

                            break;
                        }

                        default:
                        {
                            Debug.WriteLine("Unhandled capability command: {0}", args: capCommand);

                            break;
                        }
                    }

                    break;
                }

                case "PRIVMSG":
                {
                    if (!CheckForExpected(message))
                    {
                        var ircMessage = new IrcMessage(this, message);
                        var pme = new MessageReceivedEventArgs(this, ircMessage);
                        MessageReceived?.Invoke(this, pme);
                    }

                    break;
                }

                case "PING":
                {
                    await Raw.SendMessageAsync(new RawMessage("PONG", message.Parameters[0]));
                    break;
                }

                default:
                {
                    if (!CheckForExpected(message))
                    {
                        Debug.WriteLine("Unhandled IRC message: {0}", message);
                    }

                    break;
                }
            }
        }
    }
}