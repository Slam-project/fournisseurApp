using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FournisseurServer
{
    class Program
    {
        public static bool isRunning;

        static void Main(string[] args)
        {
            isRunning = true;
            Server authServer = new Server("192.168.1.74", 27015, 20);

            while (isRunning)
            {

            }
        }
    }
}
