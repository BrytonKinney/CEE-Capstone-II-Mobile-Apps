using System;
using System.Collections.Generic;
using System.Text;
using CapstoneApp.Shared.Entities;
using Xamarin.Forms;

namespace CapstoneApp.Shared.AppEvents
{
    public class ConfigurationEventArgs : EventArgs
    {
        private MirrorConfig _config;
        private BaseEntity _entity;
        private ContentPage _page;
        public ConfigurationEventArgs(MirrorConfig config, object sendingPage)
        {
            _config = config;
            _page = (ContentPage) sendingPage;
        }

        public ConfigurationEventArgs(BaseEntity entity, object sendingPage)
        {
            _entity = entity;
            _page = (ContentPage) sendingPage;
        }

        public ContentPage Page => (ContentPage) _page;
        public MirrorConfig Configuration => _config;
        public BaseEntity Entity => _entity;
    }
}
