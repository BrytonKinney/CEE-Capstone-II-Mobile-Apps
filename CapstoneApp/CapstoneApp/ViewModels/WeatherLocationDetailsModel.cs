using CapstoneApp.Shared.Constants;
using CapstoneApp.ViewModels;
using Shared.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CapstoneApp.Shared.ViewModels
{
    public class WeatherLocationDetailsModel : BaseViewModel
    {
        public List<string> LocationDropdowns => WeatherSettings.LocationDropdownOptions.ToList();
        public ObservableCollection<Country> CountryNames => new ObservableCollection<Country>(Country.List.ToList());
    }
}
