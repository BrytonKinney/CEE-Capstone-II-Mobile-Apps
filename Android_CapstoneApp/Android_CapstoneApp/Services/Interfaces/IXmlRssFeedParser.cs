using Shared.Entities.RssFeed;

namespace Shared.Services.Interfaces
{
    public interface IXmlRssFeedParser
    {
        void ParseFeed(RssFeed feed, byte[] content);
    }
}
