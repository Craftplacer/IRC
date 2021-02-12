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
        private readonly TcpClient _tcp;

        private StreamReader _reader;

        private Stream _stream;

        private StreamWriter _writer;

        private async Task ListenAsync()
        {
            while (_tcp.Connected)
            {
                var line = await _reader.ReadLineAsync();

                if (line == null)
                {
                    break;
                }

                var message = RawMessage.Parse(line);

                MessageReceived?.Invoke(this, new RawMessageReceivedEventArgs(this, message));
            }
        }

        public RawIrcClient()
        {
            _tcp = new TcpClient();
        }

        public event EventHandler<RawMessageReceivedEventArgs> MessageReceived;

        public bool Connected => _tcp.Connected;

        public async Task ConnectAsync(string host, int port, bool ssl = false)
        {
            await _tcp.ConnectAsync(host, port);

            _stream = _tcp.GetStream();

            // Wrap SSL around NetworkStream if the user wants an encrypted connection.
            if (ssl)
            {
                _stream = new SslStream(_stream);
            }

            _reader = new StreamReader(_stream);
            _writer = new StreamWriter(_stream);

            _ = Task.Run(ListenAsync);
        }

        public void Dispose()
        {
            _writer?.Dispose();
            _reader?.Dispose();
            _stream?.Dispose();
            _tcp.Dispose();
        }

        public async Task SendMessageAsync(RawMessage message)
        {
            var line = message.ToString();
            await _writer.WriteLineAsync(line);
            await _writer.FlushAsync();
        }
    }
}