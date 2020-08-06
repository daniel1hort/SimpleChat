using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using Grains.Interfaces;
using System.Threading.Tasks;
using Grains.Interfaces.SimpleModels;
using Orleans.Streams;

namespace Grains
{
    public class ChannelGrain : Grain, IChannel
    {
        IAsyncStream<Message> stream;

        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("SimpleChat");
            stream = streamProvider.GetStream<Message>(Guid.NewGuid(), "Channels");
            return base.OnActivateAsync();
        }

        public async Task<Guid> JoinChannel(string name)
        {
            var username = await GrainFactory.GetGrain<IUser>(name).GetName();
            var msg = new Message($"{username} has entered the chat", "info");
            await stream.OnNextAsync(msg);
            return stream.Guid;
        }

        public async Task<Guid> LeaveChannel(string name)
        {
            var username = await GrainFactory.GetGrain<IUser>(name).GetName();
            var msg = new Message($"welcome {username}", "info");
            await stream.OnNextAsync(msg);
            return stream.Guid;
        }

        public async Task SendMessage(Message msg)
        {
            await stream.OnNextAsync(msg);
        }
    }
}
