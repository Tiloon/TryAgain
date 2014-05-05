using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net.Sockets;
using System.Net;


namespace Server
{
    class Server
    {
        LinkedList<Client> clients;
        Socket socket;

        List<String> igIDs = new List<string>();
        Dictionary<String, GameObject> goblist = new Dictionary<String, GameObject>();


        public Server(int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            clients = new LinkedList<Client>();
            socket.Listen(35);
            Console.WriteLine("Connected on port " + ((IPEndPoint)socket.LocalEndPoint).Port);
        }

        public void Run()
        {
            Thread clientsThread = new Thread(new ThreadStart(AcceptClient));
            clientsThread.Start();

            Thread clientsChat = new Thread(new ThreadStart(Chat));
            clientsChat.Start();

            clientsThread.Join();
            clientsChat.Join();
        }

        public void AcceptClient()
        {
            while (true)
            {
                Socket client = socket.Accept();
                // Use the socket client to do whatever you want to do
                IPEndPoint remote = (IPEndPoint)client.RemoteEndPoint;
                Client myClient = new Client("Jojo", remote.Address.ToString(), remote.Port, client);
                myClient.SetName();
                myClient.Send("Welcome");

                clients.AddLast(myClient);
                Console.WriteLine("Connexion from " + myClient.name);
            }
        }

        public void Chat()
        {

            DateTime lastTick = DateTime.Now;
            while (true)
            {
                if (clients.Count == 0)
                    continue;
                /*
                if (Math.Abs(lastTick.Millisecond - DateTime.Now.Millisecond) > 200)
                {
                    foreach (Client sclient in clients)
                        if(sclient.online)
                            sclient.Send("?getpos");
                    lastTick = DateTime.Now;
                }*/

                for (int i = 0; i < clients.Count; i++)
                {
                    Client client = clients.ElementAt(i);

                    if (client.sock.Poll(1, SelectMode.SelectRead))
                    {

                        string message = client.Receive();
                        
                        if (message == null)
                        {
                            Console.WriteLine("Client " + client.name + " disconnected");
                            if (client.online)
                            {
                                goblist.Remove(client.name);
                                igIDs.Remove(client.name);
                            }
                                
                            clients.Remove(client);
                            continue;
                        }

                        Console.WriteLine(message);
                        if (message == "Ping")
                        {
                            client.Send("Pong");
                            continue;
                        }

                        if (message == "login")
                        {
                            if (goblist.ContainsKey(client.name))
                            {
                                client.Send("kick:Id already in use");
                                clients.Remove(client);
                            }
                            else
                            {
                                GameObject el = new GameObject();
                                el.name = client.name;
                                el.ID = client.name;
                                goblist.Add(client.name, el);
                                igIDs.Add(client.name);
                                client.online = true;
                                client.Send("logged:");
                            }

                            continue;
                        }

                        if (message.StartsWith("msg:"))
                        {
                            message = message.Remove(0, 4);
                            Console.WriteLine(client.name + " : " + message);

                            foreach (Client sclient in clients)
                                sclient.Send("msg:" + client.name + ": " + message);
                            continue;
                        }

                        if (message.StartsWith("pos:"))
                        {

                            goblist[client.name].X = message.Substring(4, 8);
                            goblist[client.name].Y = message.Substring(13, 8);
                            Console.WriteLine(client.name + "pos : \n{\n    x : " + System.BitConverter.ToSingle(Convert.FromBase64String(goblist[client.name].X), 0) +
                                                                     "\n    y : " + System.BitConverter.ToSingle(Convert.FromBase64String(goblist[client.name].Y), 0) + "\n}");
                            continue;
                        }
                    }
                }
            }
        }
    }
}
