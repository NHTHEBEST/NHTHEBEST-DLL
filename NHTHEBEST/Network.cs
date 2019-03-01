using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NHTHEBEST
{
    namespace Network
    {
        public class NetCat
        {
            private NetworkStream stream;
            private StreamWriter streamw;
            private StreamReader streamr;
            private TcpClient myclient = new TcpClient();
            private TcpListener myserver;
            private Thread server;

            public void Connect(string host, int port)
            {
                myclient.Connect(host, port);
                stream = myclient.GetStream();
                streamw = new StreamWriter(stream);
                streamr = new StreamReader(stream);
            }
            public void Listen(IPAddress host, int port)
            {
                myserver = new TcpListener(host, port);
                myserver.Start();
                server = new Thread(lserver);
                server.Priority = ThreadPriority.BelowNormal;
                server.Start();
                Thread.Sleep(100);
            }
            private void lserver()
            {
                while (true)
                {
                    myclient = myserver.AcceptTcpClient();
                    stream = myclient.GetStream();
                    streamw = new StreamWriter(stream);
                    streamr = new StreamReader(stream);
                }
            }
            public void Dispose() // Noncompliant
            {
                try
                {
                    server.Abort();
                }
                catch { }
            }
            public void Send(string text)
            {
                streamw.WriteLine(text);
                streamw.Flush();
            }

            private void Sendbyte(byte b)
            {
                streamw.Write(b);
                streamw.Flush();
            }
            public void SendBytes(byte[] data)
            {
                foreach (var item in data)
                {
                    Sendbyte(item);
                }
            }
            public string ReceiveLine()
            {
                return streamr.ReadLine();
            }
            private byte ReceiveByte()
            {
                return (byte)streamr.Read();
            }
            public byte[] ReceiveBytes(int bytes)
            {
                List<byte> buff = new List<byte>();
                for (int i = 0; i <= bytes; i++)
                {
                    buff.Add(ReceiveByte());
                }
                return buff.ToArray();
            }
        }
    }
}
