﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TryAgain.GameElements.misc;
using Newtonsoft.Json;
using TryAgain.Datas;
using System.Net;
using TryAgain.GameStates;
using TryAgain.Characters;
using TryAgain.GameElements.Characters;
using Server;
using TryAgain.GameElements.Map___environnement;

namespace TryAgain.Online
{

    public struct ProfileDefinition
    {
        public String avatar;
        public String name;
        public String server;

    }

    class Connection
    {
        public static Thread clientThread;
        public volatile static String UserID = "IUSer";
        public volatile static String avatar = "Ttony";
        public volatile static String host = "127.0.0.1";
        public volatile static int port = 4242;
        static volatile Socket server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static volatile StreamWriter servWriter;
        static volatile StreamReader servReader;
        static volatile bool online;
        static volatile bool update = true;
        static volatile String ping = "0";


        public static bool Updated { get { return update; } }
        public bool Connected { get { return server_socket.Connected; } }

        public static void Init(String profile)
        {
            try
            {
                ProfileDefinition p = JsonConvert.DeserializeObject<ProfileDefinition>(Initializer.ReadTextFile(@"User\" + profile));
                UserID = p.name;
                avatar = p.avatar;
                host = p.server;
            }
            catch (Exception)
            { }
        }

        public static void Connect()
        {
            try
            {
                server_socket.Connect(host, port);
                online = true;
            }
            catch (SocketException)
            {
                online = false;
                /*Server.EmbeddedServer.Launch();
                try
                {
                    server_socket.Connect("127.0.0.1", 4242);
                    online = true;
                }
                catch (SocketException)
                {
                    online = false;
                }*/
            }

            if (online)
            {
                servWriter = new StreamWriter(new NetworkStream(server_socket));
                servReader = new StreamReader(new NetworkStream(server_socket));

                servWriter.WriteLine(UserID);
                servWriter.Flush();
                string confirm = servReader.ReadLine();
                if (confirm.CompareTo("Welcome") == 0)
                {
                    Connection.clientThread = new Thread(ClientThread);
                    clientThread.Start();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("v");
                    online = false;
                    Close();
                    server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }

            }
        }

        private static void Close()
        {
            if (Server.EmbeddedServer.local)
                Server.EmbeddedServer.Stop();
            if (online)
            {
                servWriter.Flush();
                servWriter.Close();
                servReader.Close();
                server_socket.Close();
            }
        }

        public static void Stop()
        {
            if (Server.EmbeddedServer.local)
                Server.EmbeddedServer.Stop();
            if (online)
            {
                clientThread.Abort();
                Close();
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            if (server_socket.Connected && online)
                sb.DrawString(Textures.UIfont, "Online (" + ping + ") as " + UserID, new Vector2(0, 0), Color.Green);
            else
                sb.DrawString(Textures.UIfont, "Offline", new Vector2(0, 0), Color.Red);
        }

        public static void SendMessage(String str)
        {
            if (online)
            {
                servWriter.WriteLine("msg:" + str);
                servWriter.Flush();
            }
        }

        public static void SendDamage(String id, int damages)
        {
            if (online)
            {
                servWriter.WriteLine("dam:" + id + "&" + damages.ToString());
                servWriter.Flush();
            }
        }

        public static void Command(String str)
        {
            if (online)
            {
                servWriter.WriteLine(str);
                servWriter.Flush();
            }
        }

        public static void HeroDead()
        {
            if (online)
            {
                servWriter.WriteLine("rm:" + UserID);
                servWriter.Flush();
                update = false;
            }
        }

        public static bool isOnline()
        {
            return online;
        }

        public static void ClientThread()
        {
            float x = 0, y = 0;
            DateTime lastTick = DateTime.Now;
            DateTime lastPing = DateTime.Now;
            bool pingSent = false;
            bool logged = false;
            String serverReq;
            while (server_socket.Connected && online)
            {
                try
                {
                    if (server_socket.Poll(10, SelectMode.SelectRead))
                    {
                        
                        serverReq = servReader.ReadLine();
                        if (serverReq == "Pong")
                        {
                            pingSent = false;
                            int pingint = DateTime.Now.Millisecond - lastPing.Millisecond;
                            if (pingint < 0)
                                pingint += 1000;
                            ping = pingint.ToString();
                            lastPing = DateTime.Now;
                        }
                        else if (serverReq == "?getpos")
                        {
                            servWriter.WriteLine("pos:" + BitConverter.ToString(BitConverter.GetBytes(GameScreen.hero.X)) + "x" + BitConverter.ToString(BitConverter.GetBytes(GameScreen.hero.Y)));
                            servWriter.Flush();
                        }
                        else if (serverReq.StartsWith("msg:"))
                        {
                            Chat.AddMessage(serverReq.Remove(0, 4));
                        }
                        else if (serverReq.StartsWith("kick:"))
                        {
                            Chat.AddMessage("KICK:" + serverReq.Remove(0, 5), Color.Red);
                        }
                        else if (serverReq.StartsWith("logged:"))
                        {
                            logged = true;
                        }
                        else if (serverReq.StartsWith("map:")) // request : "map:JSONMAPAdress"
                        {
                            /*
                            serverReq = serverReq.Remove(0, 4);
                            try 
	                        {

                                GameStates.MainMenuScreen.isPlayable = false;
                             * */
		                        /*
                                 * int tokenPos = serverReq.IndexOf('&');
                                if (tokenPos <= 0)
                                    throw new Exception("Token not found");
                                int posX = Convert.ToInt32(serverReq.Substring(0, tokenPos));
                                serverReq = serverReq.Remove(0, tokenPos + 1);

                                tokenPos = serverReq.IndexOf('&');
                                if((posX <= 0) || (tokenPos <= 0))
                                    throw new Exception("Token not found or X-size error" + posX + " " + tokenPos);
                                
                                int posY = Convert.ToInt32(serverReq.Substring(0, tokenPos));
                                serverReq = serverReq.Remove(0, tokenPos + 1);
                                if(posY <= 0)
                                    throw new Exception("Token not found or Y-size error" + posY);
                                 * */
                            /*
                                byte[] myDataBuffer;
                                String json;
                                WebClient myWebClient = new WebClient();
                                
                                try
                                {
                                    myDataBuffer = myWebClient.DownloadData(serverReq);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    throw;
                                }
                                json = Encoding.ASCII.GetString(myDataBuffer);
                                while (!Textures.everythingLoaded) { };
                                Tilemap.MapLoadFromJSON(json);
                                GameStates.MainMenuScreen.isPlayable = true;
	                        }
	                        catch (Exception e)
	                        {
                                Debug.Show(e.Message);
                            }
                            */
                        }
                        else if (serverReq.StartsWith("rm:"))
                        {
                            string message = serverReq.Remove(0, 3);
                            if (GameScreen.GOList.Exists(z => z.UID == message))
                            {
                                int pos = GameScreen.GOList.FindIndex(z => z.UID == message);
                                GameScreen.GOList[pos].toupdate = false;
                            }

                        }
                        if (serverReq.StartsWith("gobs:"))
                        {
                            //System.Windows.Forms.MessageBox.Show(JsonConvert.SerializeObject(serverReq));
                            //System.Windows.Forms.MessageBox.Show(42.ToString());
                            Server.GameObject[] newgoblist = JsonConvert.DeserializeObject<Server.GameObject[]>(serverReq.Remove(0, 5));


                            System.Windows.Forms.MessageBox.Show(newgoblist.Length.ToString());
                            foreach (Server.GameObject gob in newgoblist)
                            {

                                if (GameScreen.GOList.Exists(z => z.UID == gob.ID))
                                {
                                    //System.Windows.Forms.MessageBox.Show(gob.ID);
                                    int pos = GameScreen.GOList.FindIndex(z => z.UID == gob.ID);
                                    /*GameScreen.GOList[pos].X = System.BitConverter.ToSingle(Convert.FromBase64String(gob.X), 0);
                                    GameScreen.GOList[pos].Y = System.BitConverter.ToSingle(Convert.FromBase64String(gob.Y), 0);*/
                                    /*GameScreen.GOList[pos].SetPosition(new Vector2(
                                        System.BitConverter.ToSingle(Convert.FromBase64String(gob.X), 0),
                                        System.BitConverter.ToSingle(Convert.FromBase64String(gob.Y), 0)));*/
                                    GameScreen.GOList[pos].TravelTo(new Vector2(
                                        System.BitConverter.ToSingle(Convert.FromBase64String(gob.X), 0),
                                        System.BitConverter.ToSingle(Convert.FromBase64String(gob.Y), 0)), gob.speed);
                                }
                                else
                                {
                                    //System.Windows.Forms.MessageBox.Show(gob.ID);
                                    GameScreen.GOList.Add(new Player(gob.ID, gob.spr, new Vector2(
                                        System.BitConverter.ToSingle(Convert.FromBase64String(gob.X), 0),
                                        System.BitConverter.ToSingle(Convert.FromBase64String(gob.Y), 0))));
                                }
                            }
                        }
                        if (serverReq.StartsWith("add:"))
                        {
                            Server.GameObject newgob;
                            try
                            {
                                newgob = JsonConvert.DeserializeObject<Server.GameObject>(serverReq.Remove(0, 4));
                            }
                            catch (Exception e)
                            {
                                System.Windows.Forms.MessageBox.Show(e.Message);
                                throw;
                            }
                            //System.Windows.Forms.MessageBox.Show(serverReq.Remove(0, 4));


                            if (GameScreen.GOList.Exists(z => z.UID == newgob.ID))
                            {
                                //System.Windows.Forms.MessageBox.Show(newgob.ID);
                                int pos = GameScreen.GOList.FindIndex(z => z.UID == newgob.ID);
                                /*GameScreen.GOList[pos].X = System.BitConverter.ToSingle(Convert.FromBase64String(gob.X), 0);
                                GameScreen.GOList[pos].Y = System.BitConverter.ToSingle(Convert.FromBase64String(gob.Y), 0);*/
                                /*GameScreen.GOList[pos].SetPosition(new Vector2(
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.X), 0),
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.Y), 0)));*/
                                GameScreen.GOList[pos].TravelTo(new Vector2(
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.X), 0),
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.Y), 0)), newgob.speed);
                                GameScreen.GOList[pos].ticked = true;
                                GameScreen.GOList[pos].toupdate = true;
                            }
                            else
                            {
                                //System.Windows.Forms.MessageBox.Show(newgob.ID);
                                TryAgain.GameElements.GameObject gobElement;
                                if (newgob.type == "Player")
                                    gobElement = new Player(newgob.ID, newgob.spr, new Vector2(
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.X), 0),
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.Y), 0)));
                                else if (newgob.type == "Item")
                                    gobElement = new Player(newgob.ID, newgob.spr, new Vector2(
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.X), 0),
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.Y), 0)));
                                else
                                    gobElement = new Player(newgob.ID, newgob.spr, new Vector2(
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.X), 0),
                                    System.BitConverter.ToSingle(Convert.FromBase64String(newgob.Y), 0)));
                                gobElement.ticked = true;
                                gobElement.toupdate = true;
                                GameScreen.GOList.Add(gobElement);
                            }
                        }
                    }
                    if (DateTime.Now.Second != lastPing.Second)
                    {
                        if (pingSent == true) // Ping superieur à une seconde, donc on se déconnecte
                            online = false;
                        pingSent = true;
                        lastPing = DateTime.Now;
                        servWriter.WriteLine("Ping");
                        servWriter.Flush();
                    }

                    if (update && (Math.Abs(lastTick.Millisecond - DateTime.Now.Millisecond) > 40)) // Tick
                    {
                        if ((x != GameScreen.hero.X) || (y != GameScreen.hero.Y))
                        {
                            x = GameScreen.hero.X;
                            y = GameScreen.hero.Y;
                            servWriter.WriteLine("pos:" + Convert.ToBase64String(BitConverter.GetBytes(GameScreen.hero.X)) + "x" + Convert.ToBase64String(BitConverter.GetBytes(GameScreen.hero.Y)));
                            servWriter.Flush();
                        }

                        foreach (var gobject in GameScreen.GOList)
                        {
                            if (gobject.UID != GameScreen.hero.UID)
                            {
                                if (gobject.ticked == false)
                                {
                                    /*
                                    GameScreen.GOList.Remove(gobject);
                                    TryAgain.GameElements.GameObject.GobjectList.Remove(gobject.UID);
                                    */
                                    //gobject.toupdate = false;
                                }
                                else
                                    gobject.ticked = false;
                            }
                        }

                        // UpdateGObjects
                        Datas.SRectangle rect;
                        rect.X = Hero.view.X;
                        rect.Y = Hero.view.Y;
                        rect.Width = Hero.view.Width;
                        rect.Height = Hero.view.Height;

                        servWriter.WriteLine("view:" + JsonConvert.SerializeObject(rect));
                        //System.Windows.Forms.MessageBox.Show(JsonConvert.SerializeObject(Hero.view));
                        servWriter.Flush();
                        lastTick = DateTime.Now;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
