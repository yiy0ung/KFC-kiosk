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

            //try {

            //    tc = new TcpClient(ip, port);
            //    stream = tc.GetStream();

            //} catch(Exception err) {

            //    return;
            //}
        }

        public TCPClient(string ip, int port, string id)
        {
            this.ip = ip;
            this.port = port;
            this.id = id;

            //try {

            //    tc = new TcpClient(ip, port);
            //    stream = tc.GetStream();

            //} catch (Exception err) {

            //    return;
            //}
        }

        public string ConnectTCPServer()
        {
            try {

                tc = new TcpClient(ip, port);
                stream = tc.GetStream();

                return "OK";

            } catch(Exception err) {

                throw new Exception("서버 접속 실패");
            }
        }

        public string TCPLogin()
        {
            try {

                try {
                    ConnectTCPServer();
                } catch(Exception err) {
                    throw new Exception(err.Message);
                }

                data = Encoding.UTF8.GetBytes(id);
                stream.Write(data, 0, data.Length);

                return "OK";

            } catch(Exception err) {

                throw new Exception("로그인 실패");
            }
        }

        public string TCPSend(string message)
        {
            try {

                string sendMessage = id + "#";
                sendMessage += message;

                TCPLogin();

                data = Encoding.UTF8.GetBytes(sendMessage);
                stream.Write(data, 0, data.Length);

                tc.Close();

                return "OK";

            } catch(Exception err) {

                return err.Message;
            }
        }

        public string TCPSendAll(string message)
        {
            try {

                string sendMessage = all + "#";
                sendMessage += message;

                TCPLogin();

                data = Encoding.UTF8.GetBytes(sendMessage);
                stream.Write(data, 0, data.Length);

                tc.Close();

                return "OK";

            } catch(Exception err) {

                return err.Message;
            }
        }
    }
}
