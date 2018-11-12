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
	public partial class ListArrivedPage : TabbedPage
	{
        ObservableCollection<Student> listStudent;
        string tmpCountryName;
        public ListArrivedPage (string CountryName)
		{
			InitializeComponent ();
            tmpCountryName = CountryName;
            GetListArrivalStudent(Helpers.Settings.FacultyIDSettings, CountryName);
        }

        

        public async void GetListArrivalStudent(string facultyID, string countryName)
        {

            try
            {

                string URL = Helpers.Settings.URLSettings + "api/DYCAndAdmin/listStudentArrivalOrNot?facultyId=" + facultyID+ "&countryName="+countryName;


                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<List<Student>>(content);
                    listStudent = new ObservableCollection<Student>(tmp);


                    listViewARRIVED.ItemsSource = listStudent.Where(c => c.Arrival== true);
                    listView.ItemsSource = listStudent.Where(c => c.Arrival == false);
                    //listView.ItemsSource = listCountry.OrderBy(x => x.CountryName).ToList();
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
        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem == null) return;
            var studentInfo = e.SelectedItem as Student;
            await Navigation.PushAsync(new StudentProfilePage(studentInfo));
            listView.SelectedItem = null;
        }


        private async void listViewARRIVED_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listViewARRIVED.SelectedItem == null) return;
            var studentInfo = e.SelectedItem as Student;
            await Navigation.PushAsync(new StudentProfilePage(studentInfo));
            listViewARRIVED.SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
            {
                GetListArrivalStudent(Helpers.Settings.FacultyIDSettings, tmpCountryName);
            }
            else
            {
                listViewARRIVED.ItemsSource = GetSearch(e.NewTextValue).OrderBy(x => x.FullName).ToList();
            }
        }

        IEnumerable<Student> GetSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return listStudent.Where(c => c.Arrival == true);
            return listStudent.Where(c => c.Arrival == true && c.FullName.ToUpper().Contains(searchText.ToUpper()));
        }

        private void searchBar_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
            {
                GetListArrivalStudent(Helpers.Settings.FacultyIDSettings, tmpCountryName);
            }
            else
            {
                listView.ItemsSource = GetSearchNotArrived(e.NewTextValue).OrderBy(x => x.FullName).ToList();
            }
        }

        IEnumerable<Student> GetSearchNotArrived(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return listStudent.Where(c => c.Arrival == true);
            return listStudent.Where(c => c.Arrival == false && c.FullName.ToUpper().Contains(searchText.ToUpper()));
        }
    }
}