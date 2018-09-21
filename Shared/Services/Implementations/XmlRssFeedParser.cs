using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using RC = Shared.Constants.RssConstants;
namespace Shared.Services.Implementations
{
    public class XmlRssFeedParser : IXmlRssFeedParser
    {
        
        public async Task ParseFeed(RssFeed feed, byte[] content)
        {
            List<Article> articles = new List<Article>();
            // Let's hope it's UTF-8
            var xmlDoc = XDocument.Parse(Encoding.UTF8.GetString(content));
            // Root node should be rss, then next should be channel
            var children = xmlDoc.Root.Descendants().First().Descendants();

            string name = children.FirstOrDefault(e => e.Name == RC.TITLE).Value, description = children.FirstOrDefault(e => e.Name == RC.DESC).Value;
            // O(n^2).. could be better
            foreach (XElement ele in children.Where(e => e.Name == RC.ITEM))
            {
                string articleTitle = "", articleLink = "", articleDesc = "";
                foreach (XElement childEle in ele.Descendants())
                {
                    if (childEle.Name == RC.TITLE)
                        articleTitle = childEle.Value;
                    else if (childEle.Name == RC.LINK)
                        articleLink = childEle.Value;
                    else if (childEle.Name == RC.DESC)
                        articleDesc = childEle.Value;
                }
                articles.Add(new Article(articleTitle, articleLink, articleDesc));
            }
            feed.SetFeedInformation(name, description, articles);
        }
    }
}
