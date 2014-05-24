using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server
{
    public class EmbeddedServer
    {
        private volatile static Thread serverThread;
        private volatile static Server server;
        public volatile static bool local = false;

        public static void Launch()
        {
            local = true;
            server = new Server(4242);
            serverThread = new Thread(server.Run);
            serverThread.Start();
        }

        public static void Stop()
        {
            Server.serverRunning = false;
            local = false;
            serverThread.Abort();
        }
    }
}
