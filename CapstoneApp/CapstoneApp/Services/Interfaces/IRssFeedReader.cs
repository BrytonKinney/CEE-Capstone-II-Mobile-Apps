using Shared.Entities.RssFeed;
using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneApp.Shared.Entities.RssFeed;

namespace Shared.Services.Interfaces
{
    public interface IRssFeedReader
    {
        Task<List<Article>> GetFeedArticles(RssFeed feed);
    }
}
