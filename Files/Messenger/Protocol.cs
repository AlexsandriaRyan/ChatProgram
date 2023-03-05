﻿// Alexsandria Ryan
// Assignment #1
// PROG2200
// March 4, 2023

using System;
using System.Net.Sockets;

namespace Messenger {
    
    public abstract class Protocol {
        
        protected readonly Int32 Port;
        protected readonly string LocalAddress;
        protected TcpListener Server;
        protected TcpClient Client;
        protected string CurrentName;
        protected string OtherName;
        private const string DisconnectMessage = " has decided to end the chat.";

        protected Protocol(Int32 newPort, string newAddress) {
            Port = newPort;
            LocalAddress = newAddress;
        }

        protected void Listen() {

            while (true) {
                
                NetworkStream stream = null;
                
                try {
                    stream = Client.GetStream();

                } catch (ObjectDisposedException e) {
                    Console.WriteLine("ObjectDisposedException: {0}", e);
                    Exit();

                } catch (NullReferenceException e) {
                    Console.WriteLine("NullReferenceException: {0}", e);
                    Exit();
                }

                // Receive data from other connection
                int i;
                Byte[] bytes = new Byte[256];
                
                while (stream != null && (i = stream.Read(bytes, 0, bytes.Length)) != 0) {
                    
                    // Translate to ASCII & print
                    string data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("{0} >>\t{1}", OtherName, data);
                    
                    // If the message ends with the "disconnect" message, it will disconnect any remaining client / server objects then exit the program
                    if (data.EndsWith(DisconnectMessage)) {
                        Disconnect();
                        Exit();
                    }
                    
                    // Switch to message mode
                    SendMessage();
                }
            }
        }

        protected void SendMessage() {
            
            while (true) {

                if (Console.KeyAvailable) {
                    
                    ConsoleKeyInfo userKey = Console.ReadKey(true);

                    // Continue taking input until the key is "I" or "Escape"
                    while ((userKey.Key != ConsoleKey.I) && (userKey.Key != ConsoleKey.Escape)) {
                        userKey = Console.ReadKey(true);
                    }
                    
                    string message = "";
                    bool end = false;
                    
                    // Input Mode
                    if (userKey.Key == ConsoleKey.I) {
                        
                        while (string.IsNullOrEmpty(message)) {
                            Console.Write("INPUT MODE >>\t");
                            message = Console.ReadLine();
                        }

                        // Disconnection via 'quit' message
                        if (message.Equals("quit", StringComparison.CurrentCultureIgnoreCase)) {
                            message = CurrentName + DisconnectMessage;
                            end = true;
                        }

                        // Disconnection via Escape key
                    } else if (userKey.Key == ConsoleKey.Escape) {
                        message = CurrentName + DisconnectMessage;
                        end = true;
                    }
                    
                    // Send Message
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    NetworkStream stream = Client.GetStream();
                    stream.Write(data, 0, data.Length);

                    // If disconnection request, continue with disconnection, otherwise move to Listen mode
                    if (end) {
                        Console.WriteLine(message);
                        Disconnect();
                        Exit();
                    } 
                    
                    // Switch to listen mode
                    Listen();
                }
            }
        }

        // Disconnects any client / server objects that are instantiated
        private void Disconnect() {
            Console.WriteLine("\nLost connection...");
            Client?.Close();
            Server?.Stop();
        }

        // Exits the program
        private void Exit() {
            Console.WriteLine("Exiting program...");
            Environment.Exit(1);
        }
    }
}