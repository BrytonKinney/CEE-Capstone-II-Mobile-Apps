
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using LightInject;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using Exception = Java.Lang.Exception;

namespace Android_Capstone
{
    [Activity(Label = "Rss Feed Details")]
    public class RssFeedDetails : Activity
    {
        private readonly IDatabaseProvider _provider;

        public RssFeedDetails()
        {
            if(_provider == null)
                _provider = MainActivity.Container.GetInstance<IDatabaseProvider>();
        }

        public async void UpdateFeed(RssFeed feed)
        {
            feed.Enabled = FindViewById<CheckBox>(Resource.Id.enabledCheckbox).Checked ? 1 : 0;
            feed.MaxArticles = Convert.ToInt32(FindViewById<EditText>(Resource.Id.numOfArticles).Text);
            var result = await _provider.AddOrUpdateAsync(feed);
            if (result > 0)
                RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Feed updated", ToastLength.Long).Show();
                    StartActivity(typeof(MainActivity)); 
                });
        }

        public void BindFeedToUi(RssFeed feed)
        {
            Title = feed.Name;
            
            var numOfArticles = FindViewById<EditText>(Resource.Id.numOfArticles);
            numOfArticles.Text = feed.MaxArticles.ToString();

            var enabled = FindViewById<CheckBox>(Resource.Id.enabledCheckbox);
            enabled.Checked = feed.Enabled == 1;

            var button = FindViewById<Button>(Resource.Id.saveChangesBtn);
            button.Click += (s, args) => { UpdateFeed(feed); };
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.rss_feed_details);
                RssFeed feed = Newtonsoft.Json.JsonConvert.DeserializeObject<RssFeed>(Intent.GetStringExtra("Feed"));
                BindFeedToUi(feed);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }
    }
}