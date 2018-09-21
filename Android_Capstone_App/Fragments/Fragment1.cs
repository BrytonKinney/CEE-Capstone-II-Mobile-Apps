using System.Threading.Tasks;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;

namespace Android_Capstone_App.Fragments
{
    public class Fragment1 : Fragment
    {
        private readonly IRssFeedReader _feedReader;
        public Fragment1(IRssFeedReader feedReader)
        {
            _feedReader = feedReader;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Fragment1 NewInstance(IRssFeedReader reader)
        {
            var frag1 = new Fragment1(reader) { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var articles = Task.Run(async () => await _feedReader.GetFeedArticles(new RssFeed("http://www.wsj.com/xml/rss/3_7041.xml"))).Result;
            return inflater.Inflate(Resource.Layout.fragment1, null);
        }
    }
}