// Alexsandria Ryan
// Assignment #1
// PROG2200
// March 4, 2023

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Messenger {
    
    public class Server : Protocol {
        
        public Server(Int32 newPort, string newAddress) : base(newPort, newAddress) {
            CurrentName = "SERVER";
            OtherName = "CLIENT";
        }
        
        public void StartServer() {

            try {
                Server = new TcpListener(IPAddress.Parse(LocalAddress), Port);
                Server.Start();
                Connect();
                Listen();

            } catch (SocketException e) {
                PrintError("SocketException", e);

            } catch (IOException e) {
                PrintError("IOException", e);
            }
        }

        private void Connect() {
            Console.Write("Waiting for a connection... ");
            Client = Server.AcceptTcpClient();
            PrintGreen("Connected!");
            Console.WriteLine("Waiting for message from Client...\n");
        }
    }
}