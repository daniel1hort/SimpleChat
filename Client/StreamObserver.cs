using Grains.Interfaces.SimpleModels;
using Microsoft.Extensions.Logging;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class StreamObserver : IAsyncObserver<Message>
    {
        public Task OnCompletedAsync()
        {
            Console.WriteLine("Chatroom message stream received stream completed event");
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            Console.WriteLine($"Chatroom is experiencing message delivery failure, ex :{ex}");
            return Task.CompletedTask;
        }

        public Task OnNextAsync(Message item, StreamSequenceToken token = null)
        {
            Console.WriteLine(item.ToString());
            return Task.CompletedTask;
        }
    }
}
