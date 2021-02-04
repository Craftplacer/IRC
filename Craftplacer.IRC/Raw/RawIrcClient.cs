using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Craftplacer.IRC.Raw
{
    public class RawIrcClient
    {
        public RawIrcClient()
        {
            tcp = new TcpClient();
        }

        private readonly TcpClient tcp;
        private Stream stream;



        public async Task ConnectAsync(string host, int port = 6697, bool ssl = false)
        {
            await tcp.ConnectAsync(host, port);

            stream = tcp.GetStream();

            // Wrap SSL around NetworkStream if user wants an encrypted connection.
            if (ssl)
                stream = new SslStream(stream);
        }


    }
}
