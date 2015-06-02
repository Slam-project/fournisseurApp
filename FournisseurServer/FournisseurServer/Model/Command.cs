using FournisseurServer.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FournisseurServer.Model
{
    class Command
    {
        private Client client;

        public Command(Client client)
        {
            this.client = client;
        }

        public void Connect(string[] param)
        {
            AuthDatabase.getDatabase().getUser(this.client, param[1], param[2]);

            if (this.client.isConnected())
            {
                Console.WriteLine("Client " + this.client.getId() + "(" + this.client.getLogin() + ") connected.");
                this.client.send("Connect" + Server.separatorCommandString + "Connected");
                
            }
            else
            {
                this.client.send("Disconnect" + Server.separatorCommandString + "login and/or password incorect");
            }
        }
    }
}
