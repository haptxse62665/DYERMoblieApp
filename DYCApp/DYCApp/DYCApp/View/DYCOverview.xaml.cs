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

namespace DYCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DYCOverview : ContentPage
    {
        ObservableCollection<AllFaculty> allFaculty;
        ObservableCollection<Country> listCountry;
        public DYCOverview()
        {
            InitializeComponent();
            if (Helpers.Settings.RoleNameSettings.Equals("DYC"))
            {

                GetListCountry(Helpers.Settings.FacultyIDSettings);
                Helpers.Settings.tmpFacultyIDSettings = Helpers.Settings.FacultyIDSettings;
            }
            else
            {
                imgFilter.IsVisible = true;
                PickerFaculty.IsVisible = true;
                GetAllFaculty();
                Helpers.Settings.tmpFacultyIDSettings = Helpers.Settings.FacultyIDSettings;
                GetListCountry(Helpers.Settings.FacultyIDSettings);
            }
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (listView.SelectedItem == null) return;

            //Navigation.PushModalAsync(new NotificationPage());
            //listView.SelectedItem = null ;

            if (listView.SelectedItem == null) return;
            var country = e.SelectedItem as Country;
            await Navigation.PushAsync(new ListHostPage(country));
            listView.SelectedItem = null;
        }

        public async void GetListCountry(string facultyID)
        {

            try
            {
                
                string URL = Helpers.Settings.URLSettings+ "api/DYCAndAdmin/listCountryAndNumberStudent?facultyId="+facultyID;
                

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<List<Country>>(content);
                    listCountry = new ObservableCollection<Country>(tmp);


                    listView.ItemsSource = listCountry.OrderBy(x => x.CountryName).ToList();
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

        public async void GetAllFaculty()
        {

            try
            {
                string URL = Helpers.Settings.URLSettings + "api/faculty/listAllFaculty";

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<List<AllFaculty>>(content);
                    allFaculty = new ObservableCollection<AllFaculty>(tmp);
                    PickerFaculty.Items.Add("Admin");
                    foreach (var item in allFaculty)
                    {
                        //if (Helpers.Settings.FacultyIDSettings.Equals(item.ID.ToString())) Helpers.Settings.FacultyNameSettings = item.FacultyName;
                        PickerFaculty.Items.Add(item.FacultyName);
                    }

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("No Notification", "Can not connect to the server. Please check your internet", "OK");
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void PickerFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex > 0)
            {
                foreach (var item in allFaculty)
                {
                    if (PickerFaculty.SelectedItem.ToString().Equals(item.FacultyName.ToString())) Helpers.Settings.tmpFacultyIDSettings = item.ID.ToString();
                    searchBar.Text = null;
                    GetListCountry(Helpers.Settings.tmpFacultyIDSettings);
                }
            }
            else {
                Helpers.Settings.tmpFacultyIDSettings = Helpers.Settings.FacultyIDSettings;
                GetListCountry(Helpers.Settings.tmpFacultyIDSettings);
                searchBar.Text = null;
            }
            
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
            {
                GetListCountry(Helpers.Settings.tmpFacultyIDSettings);
            }
            else
            {
                listView.ItemsSource = GetSearch(e.NewTextValue).OrderBy(x => x.CountryName).ToList();
            }
        }

        IEnumerable<Country> GetSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return listCountry;
            return listCountry.Where(c => c.CountryName.ToUpper().Contains(searchText.ToUpper()));
        }
    }
}