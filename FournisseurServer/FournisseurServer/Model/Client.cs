using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FournisseurServer.Model
{
    class Client
    {
        private int id;
        private string login;
        private Socket workSocket;

        public Client(Socket workSocket)
        {
            this.id = 0;
            this.login = "undifined";
            this.workSocket = workSocket;
        }

        public int getId()
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public string getLogin()
        {
            return this.login;
        }

        public void setLogin(string login)
        {
            this.login = login;
        }

        public bool isConnected()
        {
            return this.id != 0;
        }

        public void send(string data)
        {
            data = data.Replace("<EOF>", "< EOF >");
            data += "<EOF>";

            byte[] byteData = Encoding.ASCII.GetBytes(data);
            this.workSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(this.sendCallback), this.workSocket);
        }

        private void sendCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;

            client.EndSend(ar);
        }
    }
}
