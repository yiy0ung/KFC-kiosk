using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Kfc.Core
{
    public class TCPClient
    {
        string ip;
        int port;
        string id;
        string all = "@All";

        TcpClient tc;
        NetworkStream stream;
        Byte[] data;

        public TCPClient()
        {
            ip = "10.80.163.138";
            port = 80;
            id = "@2206";

            tc = new TcpClient(ip, port);
            stream = tc.GetStream();
        }

        public TCPClient(string ip, int port, string id)
        {
            this.ip = ip;
            this.port = port;
            this.id = id;

            tc = new TcpClient(ip, port);
            stream = tc.GetStream();
        }

        public void TCPLogin()
        {
            data = Encoding.UTF8.GetBytes(id);
            stream.Write(data, 0, data.Length);
        }

        public void TCPSend(string message)
        {
            string sendMessage = id + "#";
            sendMessage += message;

            TCPLogin();

            data = Encoding.UTF8.GetBytes(sendMessage);
            stream.Write(data, 0, data.Length);
        }

        public void TCPSendAll(string message)
        {
            string sendMessage = all + "#";
            sendMessage += message;

            TCPLogin();

            data = Encoding.UTF8.GetBytes(sendMessage);
            stream.Write(data, 0, data.Length);
        }
    }
}
