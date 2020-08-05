using System;
using System.Collections.Generic;
using System.Text;

namespace Grains.Interfaces.SimpleModels
{
    class Message
    {
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }

        public Message(string text, string userId)
        {
            Text = text;
            UserId = userId;
            Timestamp = DateTime.Now;
        }

        public override string ToString() => $"{Timestamp} - {UserId}: {Text}";
    }
}
