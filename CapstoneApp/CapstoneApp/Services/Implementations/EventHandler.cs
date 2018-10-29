using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CapstoneApp.Shared.AppEvents;
using CapstoneApp.Shared.Services.Interfaces;
using LightInject;
using Shared.Services.Interfaces;

namespace CapstoneApp.Shared.Services.Implementations
{
    public class EventHandler : IEventHandler
    {
        private IMessagingService _msgService;
        private ISmartMirrorService _smService;
        private IDatabaseProvider _dbService;

        public EventHandler()
        {
            _msgService = App.Container.GetInstance<IMessagingService>();
            _smService = App.Container.GetInstance<ISmartMirrorService>();
            _dbService = App.Container.GetInstance<IDatabaseProvider>();
        }

        public async void CaptureEvent(object sender, ConfigurationEventArgs args)
        {
            if (_smService.GetInstance() == null)
                return;
            if (args == null)
                return;

            if(args.Configuration != null)
                await _msgService.SendConfig(args.Configuration);

            else if (args.Entity != null)
                await _dbService.AddOrUpdateAsync(args.Entity);
        }
    }
}
