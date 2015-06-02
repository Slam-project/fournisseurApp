using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FournisseurServer.Model.Database
{
    class AuthDatabase
    {
        private static AuthDatabase db;
        private MySqlConnection connection;

        // make it in config file !
        private static string ipAddress = "127.0.0.1";
        private static string database = "auth";
        private static string login = "root";
        private static string password = "";

        private AuthDatabase()
        {
            string connectionString = "SERVER=" + ipAddress + ";DATABASE=" + database + ";UID=" + login + ";PASSWORD=" + password;
            this.connection = new MySqlConnection(connectionString);
        }

        public static AuthDatabase getDatabase()
        {
            if (db == null)
            {
                db = new AuthDatabase();
            }

            return db;
        }

        public void getUser(Client client, string login, string password)
        {
            try
            {
                this.connection.Open();

                MySqlCommand cmd = this.connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM account WHERE login = @login AND password = @password";
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    client.setId(reader.GetInt32(0));
                    client.setLogin(reader.GetString(1));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR MYSQL : " + e.ToString());
            }

            this.connection.Close();
        }
    }
}
