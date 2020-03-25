using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace P.Web.Server
{

    public class PWebServer
    {
        private readonly TcpListener _listener;

        public PWebServer()
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 1337);
            _listener = new TcpListener(ipEndPoint);
        }
        public bool Start()
        {
            Console.WriteLine("STARTED");
            _listener.Start();
            Task.Run(HandleConnection);
            return true;
        }

        public Task HandleConnection()
        {
            // Buffer for reading data
            Byte[] bytes = new Byte[1024];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = _listener.AcceptTcpClient();
                Console.WriteLine("Connected!");

                HandleRequest(client, bytes);

                client.Close();
            }
        }

        private static void HandleRequest(TcpClient client, byte[] bytes)
        {
            string data;
            data = null;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                // Console.WriteLine("Received: {0}", data);

                byte[] msg = System.Text.Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\n\r\n");

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
            }

            var httpRequestMessage = HttpMessageParser.Parse(data);

            // Response
            {
                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }
        }

        public bool Stop()
        {
            Console.WriteLine("STOPPED");
            _listener.Stop();
            return true;
        }
    }
}
