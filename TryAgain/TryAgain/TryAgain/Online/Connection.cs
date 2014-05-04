﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TryAgain.Online
{
    class Connection
    {
        public static Thread clientThread;
        public volatile static String UserID = "IUSer";
        public volatile static String host = "127.0.0.1";
        public volatile static int port = 4242;
        static volatile Socket server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static volatile StreamWriter servWriter;
        static volatile StreamReader servReader;
        static volatile bool online;
        static volatile String ping = "0";

        public bool Connected { get { return server_socket.Connected; } }

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
                    online = false;
                    Close();
                    server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }

            }
        }

        private static void Close()
        {

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
            if (online)
            {
                clientThread.Abort();
                Close();
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            if (server_socket.Connected && online)
                sb.DrawString(Textures.UIfont, "Online (" + ping + ")", new Vector2(0, 0), Color.Green);
            else
                sb.DrawString(Textures.UIfont, "Offline", new Vector2(0, 0), Color.Red);
        }

        public static void ClientThread()
        {
            DateTime lastPing = DateTime.Now;
            bool pingSent = false;
            String serverReq;
            while (server_socket.Connected && online)
            {
                if (server_socket.Poll(10, SelectMode.SelectRead))
                {
                    serverReq = servReader.ReadLine();
                    if (serverReq == "Pong")
                    {
                        pingSent = false;
                        int pingint = DateTime.Now.Millisecond - lastPing.Millisecond;
                        if(pingint < 0)
                            pingint += 1000;
                        ping = pingint.ToString();
                        lastPing = DateTime.Now;
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

            }
        }
    }
}