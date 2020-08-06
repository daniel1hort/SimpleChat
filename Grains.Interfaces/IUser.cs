using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains.Interfaces
{
    public interface IUser : IGrainWithStringKey
    {
        Task SetName(string name);
        Task<string> GetName();
    }
}
