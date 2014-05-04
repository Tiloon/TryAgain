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
            while (true)
            {
                if (clients.Count == 0)
                    continue;

                for (int i = 0; i < clients.Count; i++)
                {
                    Client client = clients.ElementAt(i);

                    if (client.sock.Poll(1, SelectMode.SelectRead))
                    {
                        string message = client.Receive();

                        if (message == null)
                        {
                            Console.WriteLine("Client " + client.name + " disconnected");
                            clients.Remove(client);
                            continue;
                        }

                        if (message == "Ping")
                        {
                            client.Send("Pong");
                            continue;
                        }

                        Console.WriteLine(client.name + " : " + message);

                        foreach (Client sclient in clients)
                            sclient.Send(client.name + ": " + message);
                    }
                }
            }
        }
    }
}
