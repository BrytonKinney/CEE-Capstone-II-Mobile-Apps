using System;

using CapstoneApp.Models;
using Shared.Services.Interfaces;
using LightInject;
using System.Threading.Tasks;
using Shared.Entities.RssFeed;
using Xamarin.Forms;

namespace CapstoneApp.ViewModels
{
    public class RssFeedDetailViewModel : BaseViewModel
    {
        public RssFeedModel Item { get; set; }
        public RssFeedDetailViewModel(RssFeedModel item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
