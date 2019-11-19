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
        TcpClient tc;
        NetworkStream stream;
        Byte[] data;

        string ip;
        int port;
        string id;
        string all = "@All";
        string lastLoginDate;
        string lastConnectDate;

        string LastLoginDate {
            get {
                return lastLoginDate;
            }
            set
            {
                lastLoginDate = value;
            }
        }
        string LastConnectDate
        {
            get
            {
                return lastConnectDate;
            }
            set
            {
                lastConnectDate = value;
            }
        }

        public TCPClient()
        {
            // default
            this.ip = "10.80.163.138";
            this.port = 80;
            this.id = "@All";
        }

        public string ConnectTCPServer(string ip, int port)
        {
            this.ip = ip;
            this.port = port;

            try {
                tc = new TcpClient(this.ip, this.port);
                stream = tc.GetStream();

                return "OK";
            } catch(Exception err) {
                return "서버 접속 실패";
            }
        }

        public string TCPLogin(string id)
        {
            this.id = id;

            if (tc.Connected == false)
            {
                string errMessage = "연결이 끊켰습니다. 다시 시도해주세요";
                return errMessage;
            }

            try {
                data = Encoding.UTF8.GetBytes(this.id);
                stream.Write(data, 0, data.Length);

                this.LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                return "OK";
            } catch(Exception err) {
                return err.Message;
            }
        }

        public string TCPSend(string message)
        {
            if (tc.Connected == false)
            {
                string errMessage = "연결이 끊켰습니다. 다시 시도해주세요";
                return errMessage;
            }

            try {
                string sendMessage = id + "#";
                sendMessage += message;

                data = Encoding.UTF8.GetBytes(sendMessage);
                stream.Write(data, 0, data.Length);

                return "OK";
            } catch(Exception err) {
                return err.Message;
            }
        }

        public string TCPSendAll(string message)
        {
            if (tc.Connected == false)
            {
                string errMessage = "연결이 끊켰습니다. 다시 시도해주세요";
                return errMessage;
            }

            try {
                string sendMessage = all + "#";
                sendMessage += message;

                data = Encoding.UTF8.GetBytes(sendMessage);
                stream.Write(data, 0, data.Length);

                return "OK";
            } catch(Exception err) {
                return err.Message;
            }
        }
    }
}
