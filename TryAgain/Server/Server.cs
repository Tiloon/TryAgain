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
using System.IO;


namespace Server
{
    class Server
    {
        public volatile static bool serverRunning = true;
        public volatile static string[,] map;
        public volatile static string mapAdress;
        public const int TICKGAP = 20;
        List<Client> clients;
        Socket socket;

        List<String> igIDs = new List<string>();
        Dictionary<String, GameObject> goblist = new Dictionary<String, GameObject>();


        public static void LoadMap(String path)
        {
            mapAdress = path;
            String json;
            byte[] myDataBuffer;
            WebClient myWebClient = new WebClient();
            try
            {
                myDataBuffer = myWebClient.DownloadData(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            json = Encoding.ASCII.GetString(myDataBuffer);
            map = JsonConvert.DeserializeObject<string[,]>(json);
        }

        public static void LoadNPCList(String path)
        {
            String json;
            byte[] myDataBuffer;
            WebClient myWebClient = new WebClient();
            try
            {
                myDataBuffer = myWebClient.DownloadData(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            json = Encoding.ASCII.GetString(myDataBuffer);
            Shared.Gob[] npcList = JsonConvert.DeserializeObject<Shared.Gob[]>(json);
        }

        public Server(int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            clients = new List<Client>();
            socket.Listen(35);
            Console.WriteLine("Connected on port " + ((IPEndPoint)socket.LocalEndPoint).Port);
        }

        public void Run()
        {
            Thread clientsThread = new Thread(new ThreadStart(AcceptClient));
            clientsThread.Start();

            Thread clientsChat = new Thread(new ThreadStart(Loop));
            clientsChat.Start();

            clientsThread.Join();
            clientsChat.Join();
        }

        public void AcceptClient()
        {
            while (serverRunning)
            {
                Socket client = socket.Accept();
                // Use the socket client to do whatever you want to do
                IPEndPoint remote = (IPEndPoint)client.RemoteEndPoint;
                Client myClient = new Client("Anon", remote.Address.ToString(), remote.Port, client);
                myClient.SetName();
                myClient.Send("Welcome");

                clients.Add(myClient);
                //clients.Add(myClient);
                Console.WriteLine("Connexion from " + myClient.name);
                myClient.Send("map:" + mapAdress);
            }
        }

        public void Loop()
        {
            DateTime lastTick = DateTime.Now;
            while (true)
            {
                if (!serverRunning)
                    return;

                if (clients.Count == 0)
                    continue;

                if (Math.Abs(lastTick.Millisecond - DateTime.Now.Millisecond) > TICKGAP) // Tick
                {
                    foreach (String id in igIDs)
                    {
                        if (goblist[id].IsToUpdate())
                        {
                            goblist[id].Update();
                        }
                    }
                    lastTick = DateTime.Now;
                }

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
                                try
                                {
                                    goblist.Remove(client.name);
                                    igIDs.Remove(client.name);
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("YOU SHALL NOT DO THIS");
                                }

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

                        if (message.StartsWith("msg:\\"))
                        {
                            message = message.Remove(0, 5);
                            if (message == "add")
                            {
                                if (!goblist.ContainsKey("monster00"))
                                {
                                    Console.WriteLine("monstre spawned");
                                    GameObject el = new GameObject();
                                    el.name = "monster00";
                                    el.ID = "monster00";
                                    el.spr = "Mbio1";
                                    el.type = "Monster";
                                    el.x = 3.0f;
                                    el.X = Convert.ToBase64String(BitConverter.GetBytes(el.x));
                                    el.y = 3.0f;
                                    el.Y = Convert.ToBase64String(BitConverter.GetBytes(el.y));
                                    goblist.Add("monster00", el);
                                    igIDs.Add("monster00");
                                }
                            }
                            else if (message == "report")
                            {
                                Console.WriteLine("REPORTED");
                                client.Send("msg:TU VEUX UNE MEDAILLE ?");
                            }
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
                            try
                            {
                                if (goblist.ContainsKey(message))
                                {
                                    if (goblist[message].type == "Player")
                                    {
                                        if (clients.Exists(z => z.name == goblist[message].name))
                                        {
                                            int idx = clients.FindIndex(z => z.name == goblist[message].name);
                                            clients[idx].Send("kick:You are dead : TryAgain");
                                            clients.Remove(clients[idx]);
                                        }
                                    }
                                    goblist.Remove(message);
                                    igIDs.Remove(message);

                                }
                            }
                            catch (Exception)
                            { }

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


                            try
                            {
                                goblist[client.name].SetView(view);
                                List<GameObject> gobjects = new List<GameObject>();
                                foreach (String item in igIDs)
                                {
                                    //Console.WriteLine("bit");
                                    if ((client.name != item) && (goblist[client.name].GetView().Intersects(new Rectangle((int)goblist[item].x, (int)goblist[item].y, 1, 1))))
                                    {
                                        goblist[item].ToUpdate(true);
                                        goblist[item].NewTarget(goblist[client.name]);
                                        gobjects.Add(goblist[item]);
                                        client.Send("add:" + JsonConvert.SerializeObject(goblist[item]));
                                    }
                                }
                            }
                            catch (Exception)
                            { }
                            /*
                            if (gobjects.Count > 0)
                            {
                                client.Send("msg:COUCOU");
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
