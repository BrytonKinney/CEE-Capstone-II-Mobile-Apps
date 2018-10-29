using System;
using System.Collections.Generic;
using System.Text;
using CapstoneApp.Shared.Entities;

namespace CapstoneApp.Shared.AppEvents
{
    public class ConfigurationEventArgs : EventArgs
    {
        private MirrorConfig _config;
        private BaseEntity _entity;

        public ConfigurationEventArgs(MirrorConfig config)
        {
            _config = config;
        }

        public ConfigurationEventArgs(BaseEntity entity)
        {
            _entity = entity;
        }

        public MirrorConfig Configuration => _config;
        public BaseEntity Entity => _entity;
    }
}
