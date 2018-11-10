namespace Shared.Entities.RssFeed
{
    public class Article
    {
        private string _url;
        private string _title;
        private string _description;

        public Article(string url, string title, string description)
        {
            _url = url;
            _title = title;
            _description = description;
        }

        public string Url => _url;
        public string Title => _title;
        public string Description => _description;
    }
}
