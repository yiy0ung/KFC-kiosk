using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Kfc.Core
{
    public class SocketErrorArgs : EventArgs
    {
        public string errMessage;
    }

    public class Client
    {
        string host;
        int port;
        string id;
        string all = "@All";
        Socket socket = null;

        string sendData = "";

        byte[] sendBuff = new byte[1024];
        byte[] receiveBuff = new byte[1024];

        public delegate void ConnectHandler(object sender, EventArgs args);
        public delegate void SocketErrorHandler(object sender, SocketErrorArgs args);
        public delegate void LoginHandler(object sender, EventArgs args);

        public event ConnectHandler OnConnect;
        public event SocketErrorHandler OnSocketError;
        public event LoginHandler OnLogin;

        public string lastLoginDate;
        public string lastConnectDate;

        public Client()
        {
            // default
            this.host = "10.80.163.138";
            this.port = 80;
            this.id = "@All";
        }

        public void Connect()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                var ep = new IPEndPoint(IPAddress.Parse(this.host), this.port);

                Console.WriteLine("서버 연결 시작");
                socket.BeginConnect(ep, ConnectCallback, null);
            }
            catch (Exception ex)
            {
                SocketErrorArgs args = new SocketErrorArgs();
                args.errMessage = ex.Message;

                if (OnSocketError != null)
                    OnSocketError(this, args);
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                socket.EndConnect(ar);
                Console.WriteLine("서버 연결 완료");

                socket.BeginReceive(receiveBuff, 0, receiveBuff.Length, SocketFlags.None, ReceiveCallback, null);

                if(OnConnect != null)
                    OnConnect(this, null); 
            }
            catch (Exception ex)
            {
                SocketErrorArgs args = new SocketErrorArgs();
                args.errMessage = ex.Message;

                if (OnSocketError != null)
                    OnSocketError(this, args);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int result = socket.EndReceive(ar);

                if (result <= 0)
                {
                    // 서버종료 됨
                    Console.WriteLine("서버가 종료 됨");

                    SocketErrorArgs args = new SocketErrorArgs();
                    args.errMessage = "서버로 부터 접속이 끊겼습니다.";

                    if (OnSocketError != null)
                        OnSocketError(this, args);

                    return;
                }

                // 다시 수신 대기
                socket.BeginReceive(receiveBuff, 0, receiveBuff.Length, SocketFlags.None, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                SocketErrorArgs args = new SocketErrorArgs();
                args.errMessage = ex.Message;

                if (OnSocketError != null)
                    OnSocketError(this, args);
            }
        }

        public void LogIn(string id)
        {
            try
            {
                Console.WriteLine("로그인 시도");

                this.id = id;
                sendData = id;
                sendBuff = Encoding.UTF8.GetBytes(sendData);
                socket.BeginSend(sendBuff, 0, sendBuff.Length, SocketFlags.None, LoginCallback, null);
            }
            catch (Exception ex)
            {
                SocketErrorArgs args = new SocketErrorArgs();
                args.errMessage = ex.Message;

                if (OnSocketError != null)                
                    OnSocketError(this, args);
            }
        }

        public void Send(string message)
        {
            try
            {
                sendData = id + "#" + message;
                sendBuff = Encoding.UTF8.GetBytes(sendData);
                socket.BeginSend(sendBuff, 0, sendBuff.Length, SocketFlags.None, SendCallback, null);
            }
            catch (Exception ex)
            {
                SocketErrorArgs args = new SocketErrorArgs();
                args.errMessage = ex.Message;

                if (OnSocketError != null)
                    OnSocketError(this, args);
            }
        }

        public void SendAll(string message)
        {
            try
            {
                sendData = all + "#" + message;
                sendBuff = Encoding.UTF8.GetBytes(sendData);
                socket.BeginSend(sendBuff, 0, sendBuff.Length, SocketFlags.None, SendCallback, null);
            }
            catch (Exception ex)
            {
                SocketErrorArgs args = new SocketErrorArgs();
                args.errMessage = ex.Message;

                if (OnSocketError != null)
                    OnSocketError(this, args);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                socket.EndSend(ar);
                Console.WriteLine("전송 완료");
            }
            catch (Exception ex)
            {
                SocketErrorArgs args = new SocketErrorArgs();
                args.errMessage = ex.Message;

                if (OnSocketError != null)
                    OnSocketError(this, args);
            }
        }

        private void LoginCallback(IAsyncResult ar)
        {
            try
            {
                socket.EndSend(ar);
                Console.WriteLine("로그인 중");

                if (OnLogin != null)
                    OnLogin(this, null);
            }
            catch (Exception ex)
            {
                SocketErrorArgs args = new SocketErrorArgs();
                args.errMessage = ex.Message;

                if (OnSocketError != null)
                    OnSocketError(this, args);
            }
        }
    }
}