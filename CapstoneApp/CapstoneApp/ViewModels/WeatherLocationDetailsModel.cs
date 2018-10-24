using CapstoneApp.Shared.Constants;
using CapstoneApp.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CapstoneApp.Shared.ViewModels
{
    public class WeatherLocationDetailsPage : BaseViewModel
    {
        public List<string> LocationDropdowns => WeatherSettings.LocationDropdownOptions.ToList();
        public ObservableCollection<Country> CountryNames => new ObservableCollection<Country>(Country.List.ToList());
    }
}
