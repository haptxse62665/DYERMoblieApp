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
        ObservableCollection<Host> listHost;
        string idCountry;

        public ListHostPage (Country country)
		{
			InitializeComponent ();
            idCountry = country.ID.ToString();
            GetListHost(idCountry, Helpers.Settings.tmpFacultyIDSettings);
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem == null) return;
            var host = e.SelectedItem as Host;
            await Navigation.PushAsync(new ListStudentPage(host));
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
                     listHost = new ObservableCollection<Host>(tmp);


                    listView.ItemsSource = listHost.OrderBy(x => x.HostName).ToList(); 
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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
            {
                GetListHost(idCountry,Helpers.Settings.tmpFacultyIDSettings);
            }
            else
            {
                listView.ItemsSource = GetSearch(e.NewTextValue).OrderBy(x => x.HostName).ToList();
            }
        }

        IEnumerable<Host> GetSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return listHost;
            return listHost.Where(c => c.HostName.ToUpper().Contains(searchText.ToUpper()));
        }
    }
}
