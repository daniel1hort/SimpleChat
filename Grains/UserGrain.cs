using Grains.Interfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class UserGrain : Grain, IUser
    {
        private string name = "no name";

        public Task<string> GetName()
        {
            return Task.FromResult(name);
        }

        public Task SetName(string name)
        {
            this.name = name;
            return Task.CompletedTask;
        }
    }
}
