using Shared.Entities.RssFeed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Services.Interfaces
{
    public interface IRssFeedReader
    {
        Task<List<Article>> GetFeedArticles(RssFeed feed);
    }
}
