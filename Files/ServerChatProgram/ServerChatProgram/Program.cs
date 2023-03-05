// Alexsandria Ryan
// Assignment #1
// PROG2200
// March 4, 2023

using System;
using Messenger;

namespace ServerChatProgram {
    
    class Program {
        
        static void Main(string[] args) {

            const Int32 port = 13000;
            const string connection = "127.0.0.1";
            
            // CLIENT MODE
            if (args.Length == 0) {
                Menu();
                Protocol.PrintGreen("***CLIENT MODE***");
                Client client = new Client(port, connection);
                client.StartClient();

            // SERVER MODE
            } else if (args[0].Equals("-server", StringComparison.CurrentCultureIgnoreCase)) {
                Menu();
                Protocol.PrintGreen("***SERVER MODE***");
                Server server = new Server(port, connection);
                server.StartServer();

            // ERROR    
            } else {
                Protocol.PrintError("ERROR:");
                Console.WriteLine("CLIENT MODE: enter application with 0 arguments.");
                Console.WriteLine("SERVER MODE: enter application with '-server' argument.");
                Console.WriteLine("Please restart the app and try again.");
                Environment.Exit(1);
            }
        }

        private static void Menu() {
            Protocol.PrintGreen("***SERVER CHAT PROGRAM***");
            Protocol.PrintYellow("\tInstructions:");
            Console.WriteLine("\t--> Select 'I' for INPUT MODE");
            Console.WriteLine("\t--> Select 'Esc' to DISCONNECT\n");
        }
    }
}