using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;
using StudentApp.Models;
using System.Net.Http.Headers;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HostInfoPage : ContentPage
	{
		public HostInfoPage ()
		{
			InitializeComponent ();
            GetHostInfo(Helpers.Settings.HostIDSettings);
		}

        public async void GetHostInfo(string hostId)
        {

            try
            {
                string URL = Helpers.Settings.URLSettings + "api/host/getHostByHostID?id=   " + hostId;

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var hostInfo = JsonConvert.DeserializeObject<Host>(content);

                    HostName.Text = hostInfo.HostName;
                    HostPhoneNumber.Text = hostInfo.ContactNumber;
                    HostEmail.Text = hostInfo.Email;
                    HostAddress.Text = hostInfo.Location;

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Fail", "Check your connection ", "OK");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void Call_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:"+HostPhoneNumber.Text));
        }
        private void Mail_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("mailto:"+HostEmail.Text+"?subject=Your issue &body=Your info and your issue's detail"));
        }
        private async void Address_Tapped(object sender, EventArgs e)
        {
            //Device.OpenUri(new Uri("geo:" + _branch.Latitude + "," + _branch.Longitude));
            //Device.OpenUri(new Uri("http://maps.google.com/?daddr=4.912967,114.888674"));
            var address = HostAddress.Text;
            var locations = await Geocoding.GetLocationsAsync(address);

            var location = locations?.FirstOrDefault();
            string Latitude = location.Latitude.ToString().Replace(',', '.');
            string Longitude = location.Longitude.ToString().Replace(',', '.');
            //Device.OpenUri(new Uri("http://maps.google.com/?daddr=" + Latitude + "," + Longitude));
            Device.OpenUri(new Uri("http://maps.google.com/?q=" + Latitude + "," + Longitude));
        }
    }
}