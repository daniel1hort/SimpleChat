using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains.Interfaces
{
    public interface IMessage : Orleans.IGrainWithIntegerKey
    {
        Task<string> SendMessage(string message);
    }
}
