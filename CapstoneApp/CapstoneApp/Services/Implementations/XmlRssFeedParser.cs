using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CapstoneApp.Shared.Entities.RssFeed;
using RXC = CapstoneApp.Shared.Constants.RssXmlConstants;
namespace Shared.Services.Implementations
{
    public class XmlRssFeedParser : IXmlRssFeedParser
    {
        
        public void ParseFeed(RssFeed feed, byte[] content)
        {
            List<Article> articles = new List<Article>();
            // Let's hope it's UTF-8
            var xmlDoc = XDocument.Parse(Encoding.UTF8.GetString(content));
            // Root node should be rss, then next should be channel
            var children = xmlDoc.Root.Descendants().First().Descendants();

            string name = children.FirstOrDefault(e => e.Name == RXC.TITLE).Value, description = children.FirstOrDefault(e => e.Name == RXC.DESC).Value;
            // O(n^2).. could be better
            foreach (XElement ele in children.Where(e => e.Name == RXC.ITEM))
            {
                string articleTitle = "", articleLink = "", articleDesc = "";
                foreach (XElement childEle in ele.Descendants())
                {
                    if (childEle.Name == RXC.TITLE)
                        articleTitle = childEle.Value;
                    else if (childEle.Name == RXC.LINK)
                        articleLink = childEle.Value;
                    else if (childEle.Name == RXC.DESC)
                        articleDesc = childEle.Value;
                }
                articles.Add(new Article(articleTitle, articleLink, articleDesc));
            }

            feed.Articles = articles;
            feed.Description = description;
            feed.Enabled = 0;
            feed.Name = name;
            feed.MaxArticles = 3;
        }
    }
}
