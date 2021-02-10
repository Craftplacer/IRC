using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading.Tasks;

using Craftplacer.IRC.Raw.Events;
using Craftplacer.IRC.Raw.Messages;

namespace Craftplacer.IRC.Raw
{
    // TODO: Look into async disposing
    public class RawIrcClient : IDisposable
    {
        public RawIrcClient()
        {
            tcp = new TcpClient();
        }

        private readonly TcpClient tcp;
        private Stream stream;
        private StreamWriter writer;
        private StreamReader reader;
        public event EventHandler<RawMessageReceivedEventArgs> MessageReceived;

        public async Task ConnectAsync(string host, int port, bool ssl = false)
        {
            await tcp.ConnectAsync(host, port);

            stream = tcp.GetStream();

            // Wrap SSL around NetworkStream if the user wants an encrypted connection.
            if (ssl)
            {
                stream = new SslStream(stream);
            }

            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);

            _ = Task.Run(ListenAsync);
        }

        private async Task ListenAsync()
        {
            while (tcp.Connected)
            {
                var line = await reader.ReadLineAsync();

                if (line == null)
                {
                    break;
                }

                var message = RawMessage.Parse(line);

                MessageReceived?.Invoke(this, new RawMessageReceivedEventArgs(this, message));
            }
        }

        public async Task SendMessageAsync(RawMessage message)
        {
            var line = message.ToString();
            await writer.WriteLineAsync(line);
            await writer.FlushAsync();
        }

        public void Dispose()
        {
            writer?.Dispose();
            reader?.Dispose();
            stream?.Dispose();
            tcp.Dispose();
        }
    }
}