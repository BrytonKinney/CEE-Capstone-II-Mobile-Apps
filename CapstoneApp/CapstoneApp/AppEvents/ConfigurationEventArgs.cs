using System;
using System.Collections.Generic;
using System.Text;
using CapstoneApp.Shared.Entities;

namespace CapstoneApp.Shared.AppEvents
{
    public class ConfigurationEventArgs : EventArgs
    {
        private MirrorConfig _config;
        public ConfigurationEventArgs(MirrorConfig config)
        {
            _config = config;
        }
        public MirrorConfig Configuration => _config;
    }
}
