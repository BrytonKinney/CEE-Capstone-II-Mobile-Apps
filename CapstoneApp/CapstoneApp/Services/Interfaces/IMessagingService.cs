using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CapstoneApp.Shared.Entities;

namespace CapstoneApp.Shared.Services.Interfaces
{
    public interface IMessagingService
    {
        Task SendConfig(MirrorConfig config);
    }
}
