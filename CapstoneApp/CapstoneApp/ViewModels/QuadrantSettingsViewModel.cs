using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.Views;
using CapstoneApp.ViewModels;
using Xamarin.Forms;

namespace CapstoneApp.Shared.ViewModels
{
    public class QuadrantSettingsViewModel : BaseViewModel
    {
        public ObservableCollection<QuadrantSettingsModel> Quadrants { get; set; }
        public QuadrantSettingsModel Q1 { get; set; }
        public QuadrantSettingsModel Q2 { get; set; }
        public QuadrantSettingsModel Q3 { get; set; }
        public QuadrantSettingsModel Q4 { get; set; }
        public QuadrantSettingsModel Q5 { get; set; }

        public Command LoadQuadrantsCommand { get; set; }

        public ObservableCollection<string> PickerOptions = new ObservableCollection<string>();
        public QuadrantSettingsViewModel()
        {
            Quadrants = new ObservableCollection<QuadrantSettingsModel>();
            LoadQuadrantsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadQuadrantsCommand.Execute(null);
            MessagingCenter.Subscribe<QuadrantSettingsPage, Picker>(this, "QuadrantChanged", (page, picker) =>
            {
                if (picker.Title == "Quadrant One")
                    Q1.ItemType = picker.SelectedItem.ToString().ToLower();
            });
            MessagingCenter.Subscribe<QuadrantSettingsPage, Dictionary<int, string>>(this, "QuadrantsSaved",
            async (page, dict) =>
            {
                foreach (KeyValuePair<int, string> kvp in dict)
                {
                    await DbProvider.AddOrUpdateAsync(new QuadrantSettings() {Id = kvp.Key, ItemType = kvp.Value, Quadrant = kvp.Key});
                }

                Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert("Quadrant settings updated.",
                            "The quadrants have been sent and saved.", "Ok");
                    });
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                var quadrants = await DbProvider.GetConnection().Table<QuadrantSettings>().ToListAsync();
                if (quadrants.Count == 0)
                {
                    Q1 = new QuadrantSettingsModel() { Quadrant = Constants.QuadrantConstants.Q1, ItemType = Constants.QuadrantConstants.ItemTypeDescriptors.RSS_FEEDS };
                    Q2 = new QuadrantSettingsModel() { Quadrant = Constants.QuadrantConstants.Q2, ItemType = Constants.QuadrantConstants.ItemTypeDescriptors.EMAIL };
                    Q3 = new QuadrantSettingsModel() { Quadrant = Constants.QuadrantConstants.Q3, ItemType = Constants.QuadrantConstants.ItemTypeDescriptors.CALENDAR };
                    Q4 = new QuadrantSettingsModel() { Quadrant = Constants.QuadrantConstants.Q4, ItemType = Constants.QuadrantConstants.ItemTypeDescriptors.WEATHER_LOCATIONS };
                    Q5 = new QuadrantSettingsModel() { Quadrant = Constants.QuadrantConstants.Q5 };
                }
                else
                {
                    QuadrantSettings q1 = quadrants.FirstOrDefault(q => q.Quadrant == Constants.QuadrantConstants.Q1) ?? new QuadrantSettings();
                    QuadrantSettings q2 = quadrants.FirstOrDefault(q => q.Quadrant == Constants.QuadrantConstants.Q2) ?? new QuadrantSettings();
                    QuadrantSettings q3 = quadrants.FirstOrDefault(q => q.Quadrant == Constants.QuadrantConstants.Q3) ?? new QuadrantSettings();
                    QuadrantSettings q4 = quadrants.FirstOrDefault(q => q.Quadrant == Constants.QuadrantConstants.Q4) ?? new QuadrantSettings();
                    QuadrantSettings q5 = quadrants.FirstOrDefault(q => q.Quadrant == Constants.QuadrantConstants.Q5) ?? new QuadrantSettings();
                    Q1 = new QuadrantSettingsModel(q1);
                    Q2 = new QuadrantSettingsModel(q2);
                    Q3 = new QuadrantSettingsModel(q3);
                    Q4 = new QuadrantSettingsModel(q4);
                    Q5 = new QuadrantSettingsModel(q5);
                }

                for (int i = 1; i < 6; i++)
                {
                    OnPropertyChanged("Q" + i);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
