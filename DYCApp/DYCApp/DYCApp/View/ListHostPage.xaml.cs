using DYCApp.Model;
using DYCApp.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DYCApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListHostPage : ContentPage
	{
		public ListHostPage (Country country)
		{
			InitializeComponent ();
            GetListHost(country.ID.ToString(), Helpers.Settings.FacultyIDSettings);
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem == null) return;
            var country = e.SelectedItem as Host;
            await Navigation.PushAsync(new ListStudentPage(country));
            listView.SelectedItem = null;
        }

        public async void GetListHost(string countryID, string facultyID)
        {

            try
            {
                string URL = Helpers.Settings.URLSettings + "api/DYCAndAdmin/listHostAndNumberStudent?facultyId="+facultyID+"&countryId=" + countryID;

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<List<Host>>(content);
                    ObservableCollection<Host> listHost = new ObservableCollection<Host>(tmp);


                    listView.ItemsSource = listHost;
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
    }
}