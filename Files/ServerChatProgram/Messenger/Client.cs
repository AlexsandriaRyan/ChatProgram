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
                PrintError("ArgumentNullException", e);

            } catch (SocketException e) {
                PrintError("SocketException", e);

            } catch (IOException e) {
                PrintError("IOException", e);
            }
        }
    }
}