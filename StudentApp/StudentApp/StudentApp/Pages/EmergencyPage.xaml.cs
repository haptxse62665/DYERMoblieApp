using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmergencyPage : ContentPage
	{
		public EmergencyPage ()
		{
			InitializeComponent ();
		}

        private async Task Button_Clicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            string latitude = position.Latitude.ToString().Replace(',', '.');
            string longitude = position.Longitude.ToString().Replace(',', '.');

            //Display latitude and longitude
            string link = String.Format("https://maps.google.com/?q={0},{1}", latitude, longitude);
            DisplayAlert("Send successful", "Your Location: " + link, "ok");
        }
    }
}