using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("SERVER V1");
                //Server.LoadMap("http://tryagain.pimzero.com/map.json");
                //http://127.0.0.1/map.json
                Server.LoadMap("http://127.0.0.1/map.json");
                Server server = new Server(4242);
                server.Run();
            }
            catch (Exception)
            {
            }
        }
    }
}
