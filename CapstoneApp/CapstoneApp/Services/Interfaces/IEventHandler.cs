using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CapstoneApp.Shared.AppEvents;

namespace CapstoneApp.Shared.Services.Interfaces
{
    public interface IEventHandler
    {
        void CaptureEvent(object sender, ConfigurationEventArgs args);
    }
}
