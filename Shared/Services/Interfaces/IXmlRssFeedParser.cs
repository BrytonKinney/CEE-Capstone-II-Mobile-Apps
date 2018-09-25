using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities.RssFeed;

namespace Shared.Services.Interfaces
{
    public interface IXmlRssFeedParser
    {
        void ParseFeed(RssFeed feed, byte[] content);
    }
}
