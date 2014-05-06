using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;


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

                                foreach (Client sclient in clients)
                                {
                                    sclient.Send("msg:" + client.name + " logged out.");
                                    sclient.Send("rm:" + client.name);
                                }
                            }
                                
                            clients.Remove(client);
                            continue;
                        }

                        //Console.WriteLine(message);
                        if (message == "Ping")
                        {
                            client.Send("Pong");
                            continue;
                        }

                        if (message.StartsWith("login:"))
                        {
                            Console.WriteLine(client.name + " Logged");

                            if (goblist.ContainsKey(client.name))
                            {
                                client.Send("kick:ID already in use");
                                clients.Remove(client);
                            }
                            else
                            {
                                GameObject el = new GameObject();
                                el.name = client.name;
                                el.ID = client.name;
                                el.spr = message.Substring(6);
                                el.type = "Player";
                                goblist.Add(client.name, el);
                                igIDs.Add(client.name);
                                client.online = true;
                                client.Send("logged:");
                                foreach (Client sclient in clients)
                                    sclient.Send("msg:" + client.name + " logged in.");
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

                        if (message.StartsWith("rm:"))
                        {
                            message = message.Remove(0, 3);
                            Console.WriteLine(message + " is removed");
                            if (goblist.ContainsKey(message))
                            {
                                goblist.Remove(message);
                                igIDs.Remove(message);
                            }
                            foreach (Client sclient in clients)
                                sclient.Send("rm:" + message);
                            continue;
                        }

                        if (message.StartsWith("view:"))
                        {
                            message = message.Remove(0, 5);
                            //Console.WriteLine(message);
                            //message = message.Remove(0, 1).Remove(message.Length - 2);
                            SRectangle rect = JsonConvert.DeserializeObject<SRectangle>(message);
                            Rectangle view = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                            goblist[client.name].SetView(view);
                            List<GameObject> gobjects = new List<GameObject>();
                            foreach (String item in igIDs)
                            {
                                //Console.WriteLine("bit");
                                if ((client.name != item) && (goblist[client.name].GetView().Intersects(new Rectangle((int)goblist[item].x, (int)goblist[item].y, 1, 1))))
                                {
                                    gobjects.Add(goblist[item]);
                                    client.Send("add:" + JsonConvert.SerializeObject(goblist[item]));
                                }
                            }
                            /*
                            if (gobjects.Count > 0)
                            {
                                client.Send("msg:COUCOUPD");
                                client.Send("gobs:" + JsonConvert.SerializeObject(gobjects));
                                Console.WriteLine(JsonConvert.SerializeObject(gobjects));
                            }*/
                            continue;
                        }

                        if (message.StartsWith("pos:"))
                        {
                            if (goblist.ContainsKey(client.name))
                            {

                                goblist[client.name].X = message.Substring(4, 8);
                                goblist[client.name].x = System.BitConverter.ToSingle(Convert.FromBase64String(goblist[client.name].X), 0);
                                goblist[client.name].Y = message.Substring(13, 8);
                                goblist[client.name].y = System.BitConverter.ToSingle(Convert.FromBase64String(goblist[client.name].Y), 0);
                            }
                            else
                            {
                                GameObject gob = new GameObject();
                                gob.X = message.Substring(4, 8);
                                gob.x = System.BitConverter.ToSingle(Convert.FromBase64String(gob.X), 0);
                                gob.Y = message.Substring(13, 8);
                                gob.y = System.BitConverter.ToSingle(Convert.FromBase64String(gob.Y), 0);
                                gob.name = client.name;
                                goblist.Add(client.name, gob);
                            }
                            //Console.WriteLine(client.name + "pos : \n{\n    x : " + System.BitConverter.ToSingle(Convert.FromBase64String(goblist[client.name].X), 0) +
                            //                                         "\n    y : " + System.BitConverter.ToSingle(Convert.FromBase64String(goblist[client.name].Y), 0) + "\n}");
                            continue;
                        }
                    }
                }
            }
        }
    }
}
