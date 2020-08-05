using Grains;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class ChatClientHostedService : IHostedService
    {
        private readonly IClusterClient client;

        public ChatClientHostedService(IClusterClient client)
        {
            this.client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Nu mere");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
