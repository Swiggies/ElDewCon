using System;
using WebSocketSharp;
using System.Timers;

namespace ElDewCon
{
    class Program
    {
        static WebSocket ws;

        static void Main(string[] args)
        {

            Console.WriteLine("Server IP Address:");
            string address = Console.ReadLine();

            Console.WriteLine("Server rcon port:");
            string port = Console.ReadLine();

            Console.WriteLine("Server rcon password:");
            string pass = Console.ReadLine();

            ws = new WebSocket($"ws://{address}:{port}", "dew-rcon");

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

            try
            {
                Message[] messages = new Message().LoadFromJson();
                foreach (Message m in messages)
                    Console.WriteLine($"Loaded message: {m.msg}");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            while (true)
            {
                string command = Console.ReadLine();
                if (command == "exit" || command == "quit")
                    Environment.Exit(0);
                ws.Send(command);
            }
        }

        static Timer InitializeMessageTimers(Message msg)
        {
            Timer t = new Timer();
            t.AutoReset = true;
            t.Interval = msg.time * 60000;
            t.Elapsed += (sender, e) => SendServerMsg(sender, e, msg.msg);
            t.Enabled = true;
            return t;
        }

        static void SendServerMsg(Object source, ElapsedEventArgs e, string msg)
        {
            ws.Send($"server.say {msg}");
        }
    }
}
