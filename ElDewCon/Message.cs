using System;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace ElDewCon
{
    class Message
    {
        public int time;
        public string[] msg;

        public Message(int time, string msg)
        {
            this.time = time;
        }

        public Message() { }
        
        public Message LoadFromJson()
        {
            try
            {
                string text = File.ReadAllText("Messages.json");
                var s = JsonConvert.DeserializeObject<Message>(text);
                return s;
            }
            catch(Exception e) { Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
