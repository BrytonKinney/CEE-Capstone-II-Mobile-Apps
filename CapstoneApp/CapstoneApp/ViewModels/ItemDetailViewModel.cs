using System;

using CapstoneApp.Models;
using Shared.Services.Interfaces;
using LightInject;
using System.Threading.Tasks;
using Shared.Entities.RssFeed;
using Xamarin.Forms;

namespace CapstoneApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
