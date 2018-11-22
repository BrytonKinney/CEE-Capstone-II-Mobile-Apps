using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuadrantSettingsPage : ContentPage
	{
	    public QuadrantSettingsViewModel viewModel;
	    
		public QuadrantSettingsPage ()
		{
			InitializeComponent ();
		    BindingContext = viewModel = new QuadrantSettingsViewModel();
		    Q1.ItemsSource = Constants.QuadrantConstants.PickerOptions;
		    Q2.ItemsSource = Constants.QuadrantConstants.PickerOptions;
		    Q3.ItemsSource = Constants.QuadrantConstants.PickerOptions;
		    Q4.ItemsSource = Constants.QuadrantConstants.PickerOptions;
		    Q5.ItemsSource = Constants.QuadrantConstants.PickerOptions;
		}

	    private void Q_OnSelectedIndexChanged(object sender, EventArgs e) 
	    {
	        MessagingCenter.Send(this, "QuadrantChanged", (Picker)sender);
	    }

	    private void Save_Clicked(object sender, EventArgs e)
	    {
	        Dictionary<int, string> settings = new Dictionary<int, string>()
	        {
	            {Constants.QuadrantConstants.Q1, (string)Q1.SelectedItem},
	            {Constants.QuadrantConstants.Q2, (string)Q2.SelectedItem },
	            {Constants.QuadrantConstants.Q3, (string)Q3.SelectedItem },
	            {Constants.QuadrantConstants.Q4, (string)Q4.SelectedItem },
	            {Constants.QuadrantConstants.Q5, (string)Q5.SelectedItem },
	        };
	        MessagingCenter.Send(this, "QuadrantsSaved", settings);
	    }
	}
}