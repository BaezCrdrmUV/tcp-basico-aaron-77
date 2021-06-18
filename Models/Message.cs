using System;

namespace tcp_com
{
    public class Message
    {
        public string idMessage {get;set;}
        public string MessageString { get; set; }
        public string User { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }

        public Message()
        {
            MessageString = "";
            User = "Default";
        }

        public Message(string messageString, string user)
        {
            this.MessageString = messageString;
            this.User = user;
        }
    }
}