using FournisseurServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FournisseurServer
{
    class Server
    {
        private IPAddress address;
        private IPEndPoint iep;
        private Socket sock;
        private ManualResetEvent allDone;
        public static string separatorCommandString = "_::_";
        private static string[] separatorCommand = new string[] { "_::_" };
        private string[] separatorSocket = new string[] { "<EOF>" };

        public Server(string ipaddress, int port, int nbClient)
        {
            this.allDone = new ManualResetEvent(true);
            this.address = IPAddress.Parse(ipaddress);
            this.iep = new IPEndPoint(this.address, port);

            this.sock = new Socket(this.iep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.sock.Bind(this.iep);
            this.sock.Listen(nbClient);

            try
            {
                while (true)
                {
                    allDone.Reset();

                    Console.WriteLine("Listen for a client ...");
                    this.sock.BeginAccept(this.AcceptCallback, sock);

                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.ToString());
            }

            Console.WriteLine("Closing the listener");

        }

        /**
         * Accept connexion from client and start a readCallback (async receiving data)
         */
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket sock = (Socket)ar.AsyncState;
            Socket handler = sock.EndAccept(ar);

            Console.WriteLine("Client connected");
            this.allDone.Set();

            Client client = new Client(handler);

            StateObject state = new StateObject(client);
            state.workSocket = handler;

            handler.BeginReceive(state.buffer, 0, StateObject.Buffersize, 0, new AsyncCallback(readCallback), state);
        }

        /**
         * Read socket from client and call commands to use it
         */
        public void readCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            try
            {
                int read = handler.EndReceive(ar);

                if (read > 0)
                {
                    string test = Encoding.ASCII.GetString(state.buffer, 0, read);
                    state.sb.Append(test);

                    string content = state.sb.ToString();

                    string[] commands = content.Split(separatorSocket, StringSplitOptions.None);

                    if (commands.Length > 1)
                    {
                        for (int i = 0; i < commands.Length - 1; i++)
                        {
                            UseCommand(commands[i], state.getClient());
                        }

                        state.sb = new StringBuilder(commands[commands.Length - 1]);
                    }


                    handler.BeginReceive(state.buffer, 0, StateObject.Buffersize, 0, new AsyncCallback(readCallback), state);
                }
                else
                {
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                handler.Close();
                Console.WriteLine("Error when read client's packet");
            }

        }

        /**
         * Traite les commandes
         */
        private void UseCommand(string command, Client client)
        {
            string[] data = command.Split(separatorCommand, StringSplitOptions.None);

            Command cmd = new Command(client);
            Type type = cmd.GetType();

            MethodInfo theCommand = type.GetMethod(data[0]);

            object[] arguments = { data };

            theCommand.Invoke(cmd, arguments);
        }
    }
}
