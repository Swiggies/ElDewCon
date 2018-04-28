using System;
using WebSocketSharp;
using System.Timers;

namespace ElDewCon
{
    class Program
    {
        static WebSocket ws;
        static int counter = 0;

        static void Main(string[] args)
        {
            string address = string.Empty;
            string port = string.Empty;
            string pass = string.Empty;
            if (args.Length == 0)
            {
                Console.WriteLine("Server IP Address:");
                address = Console.ReadLine();

                Console.WriteLine("Server rcon port:");
                port = Console.ReadLine();

                Console.WriteLine("Server rcon password:");
                pass = Console.ReadLine();
            }
            else
            {
                address = args[0];
                port = args[1];
                pass = args[2];
            }

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
                PrepareForClose();
                return;
            };

            ws.OnClose += (sender, r) =>
            {
                Console.WriteLine(r.Reason);
                PrepareForClose();
                return;
            };

            ws.Connect();
            ws.Send(pass);

            try
            {
                Message messages = new Message().LoadFromJson();
                if (messages != null)
                {
                    InitializeMessageTimers(messages);
                    foreach(string s in messages.msg)
                    Console.WriteLine($"Loaded message: {s} every {messages.time} minute(s).");
                }
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
            t.Elapsed += (sender, e) => SendServerMsg(sender, e, msg);
            t.Enabled = true;
            return t;
        }

        static void SendServerMsg(Object source, ElapsedEventArgs e, Message msg)
        {
            ws.Send($"server.say {msg.msg[counter]}");
            counter++;//Make this loop back to 0 when reached the end.
        }

        static void PrepareForClose()
        {
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
