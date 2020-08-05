using Grains.Interfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class MessageGrain : Grain, IMessage
    {
        public string Author { get; set; }

        public MessageGrain()
        {
            Author = "guest";
        }

        public Task<string> SendMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
