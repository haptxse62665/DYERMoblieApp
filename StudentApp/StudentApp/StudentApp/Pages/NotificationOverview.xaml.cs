using Newtonsoft.Json;
using Plugin.Geolocator;
using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationOverview : ContentPage
	{
        
        public NotificationOverview ()
		{
			InitializeComponent ();

            GetNotificationFromDYC( Helpers.Settings.IdSettings);
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (listView.SelectedItem == null) return;
            
            //Navigation.PushModalAsync(new NotificationPage());
            //listView.SelectedItem = null ;

            if (listView.SelectedItem == null) return;
            var noti = e.SelectedItem as Notification;
            await Navigation.PushModalAsync(new NotificationPage(noti));
            listView.SelectedItem = null;
        }

        public async void GetNotificationFromDYC( string studentId)
        {

            try
            {
                string URL = Helpers.Settings.URLSettings + "api/userSendNotification/inforByStudentID?studentID=" + studentId;

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<List<Notification>>(content);
                    ObservableCollection<Notification> listNotification = new ObservableCollection<Notification>(tmp);


                    listView.ItemsSource = listNotification.OrderBy(x => x.DateCreated).ToList();

                }
                else
                {
                    DisplayAlert("No Notification", "All notification from DYC have been responsed ", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("No Notification", "Can not connect to the server. Please check your internet", "OK");
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

      

        public async Task GetLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            string latitude = position.Latitude.ToString().Replace(',', '.');
            string longitude = position.Longitude.ToString().Replace(',', '.');

            //Display latitude and longitude
            string link = String.Format("https://maps.google.com/?q={0},{1}", latitude, longitude);

            CreateEmergencyAsync(Helpers.Settings.IdSettings, link);

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

                activity_indicator.IsRunning = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Fail", "Check your connection ", "OK");
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                activity_indicator.IsRunning = false;
            }
        }

        private async Task ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //activity_indicator.IsRunning = true;
            //GetLocation();
            await Navigation.PushModalAsync(new EmergencyPage());
            
        }

        
    }
}