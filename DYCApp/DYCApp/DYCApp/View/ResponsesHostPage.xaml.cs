using DYCApp.Model;
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

namespace DYCApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResponsesHostPage : ContentPage
    {
        ObservableCollection<Responses> listResponses;
        string title;
        DateTime dateCreate;
        public ResponsesHostPage(Responses responses)
        {
            InitializeComponent();
            title = responses.NotifiTitle;
            dateCreate = responses.DateCreate;
            GetListResponsesHost(title, dateCreate, Helpers.Settings.tmpFacultyIDSettings);
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem == null) return;
            var responses = e.SelectedItem as Responses;
            //await Navigation.PushAsync(new ResponsesHostPage(responses));
            listView.SelectedItem = null;
        }
        public async void GetListResponsesHost(string title, DateTime dateCreate, string facultyID)
         {

            try
            {

                string URL = Helpers.Settings.URLSettings + "api/userSendNotification/listHostAndCount?title=" + title + "&dateCreate=" + dateCreate + "&facultyID=" + facultyID;


                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<List<Responses>>(content);
                    listResponses = new ObservableCollection<Responses>(tmp);


                    listView.ItemsSource = listResponses.OrderBy(x => x.HostName).ToList();
                }
                else
                {
                    DisplayAlert("No Response", "URL :"+URL, "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("No Response", "Can not connect to the server. Please check your internet", "OK");
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
            {
                GetListResponsesHost(title, dateCreate, Helpers.Settings.FacultyIDSettings);
            }
            else
            {
                listView.ItemsSource = GetSearch(e.NewTextValue).OrderBy(x => x.CountryName).ToList();
            }
        }

        IEnumerable<Responses> GetSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return listResponses;
            return listResponses.Where(c => c.NotifiTitle.ToUpper().Contains(searchText.ToUpper()));
        }

    }
}