using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CapstoneApp.Shared.AppEvents;
using CapstoneApp.Shared.Services.Interfaces;

namespace CapstoneApp.Shared.Services.Implementations
{
    public class EventHandler : IEventHandler
    {
        public EventHandler() {}

        public async void CaptureEvent(object sender, ConfigurationEventArgs args)
        {
            if (args == null)
                return;

        }
    }
}
