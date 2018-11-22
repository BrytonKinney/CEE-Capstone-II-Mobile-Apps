using System;
using CapstoneApp.Models;
using CapstoneApp.Shared.Entities.RssFeed;
using CapstoneApp.ViewModels;
using CapstoneApp.Views;
using LightInject;
using Shared.Services.Interfaces;
using Xamarin.Forms;

namespace CapstoneApp.Shared.ViewModels
{
    public class RssFeedDetailViewModel : BaseViewModel
    {
        public RssFeedModel Item { get; set; }
        public RssFeedDetailViewModel(RssFeedModel item = null)
        {
            Title = item?.Text;
            Item = item;
            MessagingCenter.Subscribe<RssFeedDetailPage, RssFeedModel>(this, "SaveRssFeed",
                async (page, model) =>
                {
                    //await SaveEntity(new RssFeed(model), page);
                    await SaveEntity(new RssFeed(model), page).ContinueWith(async (t) =>
                    {
                        if (t.IsCompleted && !t.IsFaulted)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Application.Current.MainPage.DisplayAlert("Saved changes", "RSS Feed Settings saved.", "OK");
                      //          await page.Navigation.PopAsync();
                            });

                        }
                    });
                });
            MessagingCenter.Subscribe<RssFeedDetailPage, RssFeedModel>(this, "DeleteRssFeed", async (page, model) =>
            {
                var db = App.Container.GetInstance<IDatabaseProvider>();
                RssFeed feed = await db.GetAsync<RssFeed>(Convert.ToInt32(model.Id));
                await db.DeleteAsync(feed);
                await page.Navigation.PopAsync();
            });
        }
    }
}
