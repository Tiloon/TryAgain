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
        Random rand = new Random();
        int idmob = 0;

        public volatile static List<String> igIDs = new List<string>();
        public volatile static Dictionary<String, GameObject> goblist = new Dictionary<String, GameObject>();

        private void AddMob(float x, float y)
        {
            idmob++;
            int type = rand.Next(0, 100);
            if (!goblist.ContainsKey("monster0" + idmob.ToString()))
            {
                string id = "monster0" + idmob.ToString();
                GameObject el = new GameObject();
                if (type <= 50)
                {
                    el.SetLp(10);
                    el.name = "ghost";
                    el.ID = id;
                    el.spr = "Mghost";
                    el.type = "Monster";
                    el.x = x;
                    el.X = Convert.ToBase64String(BitConverter.GetBytes(el.x));
                    el.y = y;
                    el.Y = Convert.ToBase64String(BitConverter.GetBytes(el.y));
                    el.speed += ((float)rand.Next(-6, 6)) / 200;
                }
                else if (type <= 60)
                {
                    el.SetLp(40);
                    el.name = "bio";
                    el.ID = id;
                    el.spr = "Mbio1";
                    el.type = "Monster";
                    el.x = x;
                    el.X = Convert.ToBase64String(BitConverter.GetBytes(el.x));
                    el.y = y;
                    el.Y = Convert.ToBase64String(BitConverter.GetBytes(el.y));
                    el.speed += ((float)rand.Next(0, 14)) / 180;
                }
                else if ((map[(int)x, (int)y] == "Tfire") || (map[(int)x, (int)y] == "Tpierrenoi"))
                {
                    el.SetLp(180);
                    el.name = "dragon";
                    el.ID = id;
                    el.spr = "Mdragon";
                    el.type = "Monster";
                    el.x = x;
                    el.X = Convert.ToBase64String(BitConverter.GetBytes(el.x));
                    el.y = y;
                    el.Y = Convert.ToBase64String(BitConverter.GetBytes(el.y));
                    el.speed += ((float)rand.Next(-2, 12)) / 180;
                }
                else
                    return;
                goblist.Add(id, el);
                igIDs.Add(id);
                Console.WriteLine("Npc spawned : " + el.name + "pos : (" + el.x.ToString() + ";" + el.y.ToString() + ")");
            }
        }

        public static void LoadMapFromWeb(String path)
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
            Console.WriteLine("Map loaded (size : (" + map.GetLength(0) + ":" + map.GetLength(1) + ")");
        }

        public static void LoadMap(String path)
        {
            mapAdress = null;
            String json;
            StreamReader sr = new StreamReader(path);
            json = sr.ReadToEnd();
            map = JsonConvert.DeserializeObject<string[,]>(json);
            Console.WriteLine("Map loaded (size : (" + map.GetLength(0) + ":" + map.GetLength(1) + ")");
        }

        public void LoadNPCList(String path)
        {
            String json;
            StreamReader sr = new StreamReader(path);
            json = sr.ReadToEnd();
            Shared.Gob[] npcList = JsonConvert.DeserializeObject<Shared.Gob[]>(json);
            foreach (var npc in npcList)
            {
                GameObject e = new GameObject();
                e.X = npc.X;
                e.Y = npc.Y;
                e.x = Shared.Converter.StringToFloat(e.X);
                e.y = Shared.Converter.StringToFloat(e.Y);
                e.SetStats(npc.stats);
                e.SetScript(npc.script);
                e.name = npc.commonName;
                e.ID = npc.name;
                e.spr = npc.spr;
                e.type = npc.type;

                goblist.Add(e.ID, e);
                igIDs.Add(e.ID);
                Console.WriteLine("Loaded : " + npc.name + "(poistion : (" + e.x.ToString() + ":" + e.y.ToString() + ")");
            }
        }

        public Server(int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            clients = new List<Client>();
            socket.Listen(35);
            Console.WriteLine("Connected on port " + ((IPEndPoint)socket.LocalEndPoint).Port);

            this.LoadNPCList("npcs.json");
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
                if (mapAdress != null)
                    myClient.Send("map:" + mapAdress);
            }
        }

        public void Remove(string id)
        {
            foreach (Client sclient in clients)
                sclient.Send("rm:" + id);
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
                    for (int i = 0; i < igIDs.Count; i++)
                    {
                        if (goblist[igIDs[i]].IsToUpdate())
                        {
                            goblist[igIDs[i]].Update();
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
                                    el.spr = "Mghost";
                                    el.type = "Monster";
                                    el.x = 3.0f;
                                    el.X = Convert.ToBase64String(BitConverter.GetBytes(el.x));
                                    el.y = 3.0f;
                                    el.Y = Convert.ToBase64String(BitConverter.GetBytes(el.y));
                                    goblist.Add("monster00", el);
                                    igIDs.Add("monster00");
                                }
                            }
                            else if (message == "additem")
                            {
                                if (!goblist.ContainsKey("item00"))
                                {
                                    Console.WriteLine("item added");
                                    GameObject el = new GameObject();
                                    el.name = "item00";
                                    el.ID = "item00";
                                    el.spr = "Mbio1";
                                    el.type = "Item";
                                    el.x = 3.0f;
                                    el.X = Convert.ToBase64String(BitConverter.GetBytes(el.x));
                                    el.y = 3.0f;
                                    el.Y = Convert.ToBase64String(BitConverter.GetBytes(el.y));
                                    goblist.Add("item00", el);
                                    igIDs.Add("item00");
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

                        if (message.StartsWith("dam:"))
                        {
                            message = message.Remove(0, 4);
                            string id = message.Substring(0, message.IndexOf('&'));
                            int damages = Convert.ToInt32(message.Substring(message.IndexOf('&') + 1));
                            Tuple<string, int> data = new Tuple<string,int>(id, damages);
                            Console.Write(data.Item1 + " got damaged : " + data.Item2 + "damages.");
                            try
                            {
                                if (goblist.ContainsKey(data.Item1))
                                {
                                    goblist[data.Item1].TakeDamages(data.Item2);
                                }
                            }
                            catch (Exception)
                            { }

                            continue;
                        }

                        if (message.StartsWith("view:"))
                        {

                            message = message.Remove(0, 5);
                            //Console.WriteLine(message);
                            //message = message.Remove(0, 1).Remove(message.Length - 2);
                            SRectangle rect = JsonConvert.DeserializeObject<SRectangle>(message);
                            Rectangle view = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);


                            for (int x = 0; x < rect.Width; x++)
                            {
                                if (rand.Next(0, 8000) < 1)
                                    AddMob(rect.X + x, rect.Y - 2);

                                if (rand.Next(0, 8000) < 1)
                                    AddMob(rect.X + x + 1, rect.Y + rect.Height + 1);
                            }

                            for (int y = 0; y < rect.Height; y++)
                            {
                                if (rand.Next(0, 8000) < 1)
                                    AddMob(rect.X - 2, rect.Y + y);

                                if (rand.Next(0, 8000) < 1)
                                    AddMob(rect.X + rect.Width + 1, rect.Y + y + 1);
                            }

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
