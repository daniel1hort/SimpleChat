using Grains.Interfaces.SimpleModels;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains.Interfaces
{
    public interface IChannel : IGrainWithStringKey
    {
        Task<Guid> JoinChannel(string name);
        Task<Guid> LeaveChannel(string name);
        Task SendMessage(Message msg);
    }
}
