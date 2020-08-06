using Grains;
using Grains.Interfaces;
using Grains.Interfaces.SimpleModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class ChatClientHostedService : IHostedService
    {
        private IClusterClient Client { get; }
        private const string generalChannel = "general";

        public ChatClientHostedService(IClusterClient client)
        {
            Client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!Client.IsInitialized) ;

            Console.Write("\nPlease register a name: ");
            var name = Console.ReadLine().Trim('\n');
            var user = Client.GetGrain<IUser>(name);
            await user.SetName(name);

            await JoinChannel(user.GetName().Result, generalChannel);

            while (!cancellationToken.IsCancellationRequested)
            {
                var data = Console.ReadLine();
                var msg = new Message(data, user.GetName().Result);
                await SendMessage(msg);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task JoinChannel(string userName, string channelName)
        {
            var channel = Client.GetGrain<IChannel>(channelName);
            var streamId = await channel.JoinChannel(userName);
            var stream = Client.GetStreamProvider("SimpleChat").GetStream<Message>(streamId, "Channels");
            await stream.SubscribeAsync(new StreamObserver());
        }

        private async Task SendMessage(Message msg)
        {
            var channel = Client.GetGrain<IChannel>(generalChannel);
            await channel.SendMessage(msg);
        }
    }
}
