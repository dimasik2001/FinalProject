using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlxAPI.Services.Abstractions
{
    public interface IDbInitializer
    {
        public Task Seed();
    }
}
