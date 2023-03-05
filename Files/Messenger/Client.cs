// Alexsandria Ryan
// Assignment #1
// PROG2200
// March 4, 2023

using System;
using System.IO;
using System.Net.Sockets;

namespace Messenger {

    public class Client : Protocol {

        public Client(Int32 newPort, string newAddress) : base(newPort, newAddress) {
            CurrentName = "CLIENT";
            OtherName = "SERVER";
        }

        public void StartClient() {

            try {
                Client = new TcpClient(LocalAddress, Port);
                SendMessage();

            } catch (ArgumentNullException e) {
                Console.WriteLine("ArgumentNullException: {0}", e);

            } catch (SocketException e) {
                Console.WriteLine("SocketException: {0}", e);

            } catch (IOException e) {
                Console.WriteLine("IOException: {0}", e);
            }
        }
    }
}