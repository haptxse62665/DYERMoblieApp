using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Xamarin.Essentials;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HostInfoPage : ContentPage
	{
		public HostInfoPage ()
		{
			InitializeComponent ();
		}

        private void Call_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:+123 456 789"));
        }
        private void Mail_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("mailto:fptUniversity@test.com?subject=Your issue &body=Your info and your issue's detail"));
        }
        private async void Address_Tapped(object sender, EventArgs e)
        {
            //Device.OpenUri(new Uri("geo:" + _branch.Latitude + "," + _branch.Longitude));
            //Device.OpenUri(new Uri("http://maps.google.com/?daddr=4.912967,114.888674"));
            var address = "Hoa Lac High Tech Park,Hanoi, Vietnam";
            var locations = await Geocoding.GetLocationsAsync(address);

            var location = locations?.FirstOrDefault();
            string Latitude = location.Latitude.ToString().Replace(',', '.');
            string Longitude = location.Longitude.ToString().Replace(',', '.');
            //Device.OpenUri(new Uri("http://maps.google.com/?daddr=" + Latitude + "," + Longitude));
            Device.OpenUri(new Uri("http://maps.google.com/?q=" + Latitude + "," + Longitude));
        }
    }
}