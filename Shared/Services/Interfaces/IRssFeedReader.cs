using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities.RssFeed;

namespace Shared.Services.Interfaces
{
    public interface IRssFeedReader
    {
        Task<List<Article>> GetFeedArticles(RssFeed feed);
    }
}
