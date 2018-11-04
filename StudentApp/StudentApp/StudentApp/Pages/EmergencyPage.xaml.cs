using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmergencyPage : ContentPage
	{
        string link;
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
            link = String.Format("https://maps.google.com/?q={0},{1}", latitude, longitude);

            CreateEmergencyAsync(Helpers.Settings.IdSettings, link);

        }

        public async void UpdateByPostAsync(string Id, string link)
        {
            string URL = Helpers.Settings.URLSettings;

            var dict = new Dictionary<string, string>();
            dict.Add("id", Id);
            dict.Add("link", link);
            var client = new HttpClient();

            try
            {
                //post
                var response = await client.PostAsync(URL, new FormUrlEncodedContent(dict));

                if (response.IsSuccessStatusCode)
                {
                    //var content = await response.Content.ReadAsStringAsync();
                    //var tmp = JsonConvert.DeserializeObject<List<Notification>>(content);

                    //Helpers.Settings.NewPhoneNumberSettings = phone;
                    DisplayAlert("Send successful", " ", "OK");
                }
                else
                {
                    DisplayAlert("Fail", " ", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Fail", "Check your connection ", "OK");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }

        public async void CreateEmergencyAsync(string Id, string link)
        {
            try
            {
                string URL = Helpers.Settings.URLSettings + "api/student/createStudentSentNotofication?studentID=" + Id + "&Coordinate=" + link;

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    //var content = await response.Content.ReadAsStringAsync();
                    //var tmp = JsonConvert.DeserializeObject<List<Notification>>(content);


                    DisplayAlert("Send successful", "Your Location: " + link, "ok");
                }
                else
                {
                    DisplayAlert("Fail", " ", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Fail", "Check your connection ", "OK");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void Button_Cancel(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}