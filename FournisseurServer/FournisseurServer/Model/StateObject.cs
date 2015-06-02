using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FournisseurServer.Model
{
    class StateObject
    {
        public Socket workSocket = null;
        public const int Buffersize = 1024;
        public byte[] buffer = new byte[Buffersize];
        public StringBuilder sb = new StringBuilder();
        private Client client;

        public StateObject(Client client)
        {
            this.client = client;
        }

        public Client getClient()
        {
            return this.client;
        }
    }
}
