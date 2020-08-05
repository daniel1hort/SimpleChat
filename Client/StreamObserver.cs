using Grains;
using Microsoft.Extensions.Logging;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class StreamObserver : IAsyncObserver<MessageGrain>
    {
        private readonly ILogger logger;

        public StreamObserver(ILogger logger)
        {
            this.logger = logger;
        }

        public Task OnCompletedAsync()
        {
            logger.LogInformation("Chatroom message stream received stream completed event");
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            logger.LogInformation($"Chatroom is experiencing message delivery failure, ex :{ex}");
            return Task.CompletedTask;
        }

        public Task OnNextAsync(MessageGrain item, StreamSequenceToken token = null)
        {
            this.logger.LogInformation(item.ToString());
            return Task.CompletedTask;
        }
    }
}
