using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Craftplacer.IRC.Raw.Events;
using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC
{
    public partial class IrcClient
    {
        private async void Raw_MessageReceived(object sender, RawMessageReceivedEventArgs e)
        {
            // Check if the received message is a server reply or a command.
            if (ushort.TryParse(e.Message.Command, out var replyNum))
            {
                var reply = (ServerReply)replyNum;
                switch (reply)
                {
                    case ServerReply.RPL_WELCOME:
                    {
                        Welcome?.Invoke(this, EventArgs.Empty);
                        break;
                    }

                    case ServerReply.RPL_MOTD:
                    {
                        if (_motdBuffer != null)
                        {
                            _motdBuffer.AppendLine(e.Message.Parameters[0]);
                        }
                        break;
                    }

                    case ServerReply.RPL_MOTDSTART:
                    {
                        if (_motdBuffer == null)
                        {
                            _motdBuffer = new StringBuilder();
                        }

                        _motdBuffer.AppendLine(e.Message.Parameters[0]);
                        break;
                    }

                    case ServerReply.RPL_MOTDEND:
                    {
                        Motd = _motdBuffer.ToString();
                        _motdBuffer.Clear();
                        break;
                    }

                    default:
                    {
                        Debug.WriteLine("Unhandled IRC server reply: {1} ({0}) {2}", e.Message.Command, reply, string.Join(' ', e.Message.Parameters));
                        break;
                    }
                }
            }
            else
            {
                switch (e.Message.Command)
                {
                    case "CAP":
                    {
                        var capCommand = e.Message.Parameters[1];
                        switch (capCommand)
                        {
                            case "LS":
                            {
                                if (_negotiatingCapabilities)
                                {
                                    break;
                                }

                                _serverCapabilities = e.Message.Parameters[2].Split(' ');
                                Debug.WriteLine("Offered capabilities: {0}", args: string.Join(", ", _serverCapabilities));

                                var supportedCapabilities = _serverCapabilities.Where(sc => _supportedCapabilities.Contains(sc));

                                if (supportedCapabilities.Any())
                                {
                                    _negotiatingCapabilities = true;
                                    await Raw.SendMessageAsync(new RawMessage("CAP", "REQ", string.Join(' ', supportedCapabilities)));
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

                                _acknowledgedCapabilities = e.Message.Parameters[2].Split(' ');
                                await Raw.SendMessageAsync(new RawMessage("CAP", "END"));

                                _negotiatingCapabilities = false;
                                Debug.WriteLine("Finished negotiating capabilities: {0}", args: string.Join(", ", _acknowledgedCapabilities));

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

                    case "PING":
                        e.Client.SendMessageAsync(new RawMessage("PONG", e.Message.Parameters[0]));
                        break;

                    default:
                    {
                        Debug.WriteLine("Unhandled IRC message: {0}", e.Message);
                        break;
                    }
                }
            }
        }
    }
}