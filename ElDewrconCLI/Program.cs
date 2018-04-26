using System;
using WebSocketSharp;

namespace ElDewrconCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server IP Address:");
            string address = Console.ReadLine();

            Console.WriteLine("Server rcon port:");
            string port = Console.ReadLine();

            Console.WriteLine("Server rcon password:");
            string pass = Console.ReadLine();

            WebSocket ws = new WebSocket($"ws://{address}:{port}", "dew-rcon");

            ws.OnOpen += (sender, r) =>
            {
                Console.WriteLine("Connected");
                Console.Clear();
                return;
            };

            ws.OnMessage += (sender, r) =>
            {
                Console.WriteLine(r.Data);
                return;
            };

            ws.OnError += (sender, r) =>
            {
                Console.WriteLine(r.Message);
                return;
            };

            ws.OnClose += (sender, r) =>
            {
                Console.WriteLine(r.Reason);
                return;
            };

            ws.Connect();
            ws.Send(pass);

            while (true)
            {
                string command = Console.ReadLine();
                if (command != "exit")
                    ws.Send(command);
                else
                    Environment.Exit(0);
            }
        }
    }
}
