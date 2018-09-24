using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Database;
using Android.OS;
using Android.Support.V4.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.Constants;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;

namespace Android_Capstone_App.Fragments
{
    public class Fragment2 : ListFragment
    {
        private readonly IRssFeedReader _reader;
        private static ArrayAdapter<string> _adapter;
        private static List<RssFeed> _feeds = new List<RssFeed>();
        public Fragment2(IRssFeedReader reader)
        {
            _reader = reader;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ProgressBar progressBar = new ProgressBar(this.Context);
            Task.Run(async () =>
            {
                foreach (var rss in DefaultRssFeedUrls.GetAll())
                {
                    var rssFeed = new RssFeed(rss);
                    await _reader.GetFeedArticles(rssFeed);
                    _adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleExpandableListItem1);
                    _adapter.Add(rssFeed.Name);
                }
            }).ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    this.ListAdapter = _adapter;
                    _adapter.NotifyDataSetChanged();
                }
            });
        }

        public static Fragment2 NewInstance(IRssFeedReader reader)
        {
            var frag2 = new Fragment2(reader) { Arguments = new Bundle() };
            return frag2;
        }

        public static void AddRssFeedToList(RssFeed feed)
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.fragment2, null);
        }
    }
}