using System;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace ElDewCon
{
    class Message
    {
        public int time;
        public string msg;

        public Message(int time, string msg)
        {
            this.time = time;
            this.msg = msg;
        }

        public Message() { }
        
        public Message[] LoadFromJson()
        {
            string text = File.ReadAllText("Messages.json");
            var s = JsonConvert.DeserializeObject<Message[]>(text);
            return s;
        }
    }
}
