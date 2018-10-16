
using CapstoneApp.Models;

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
