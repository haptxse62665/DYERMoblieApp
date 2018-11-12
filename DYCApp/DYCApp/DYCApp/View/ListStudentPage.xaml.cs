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
	public partial class ListStudentPage : ContentPage
	{
        ObservableCollection<Student> listStudent;
        string hostId;

        public ListStudentPage (Host host)
		{
			InitializeComponent ();

            //cannot hide in list view
            //if (Helpers.Settings.RoleNameSettings.Equals("DYC")) lbFaculty.IsVisible = false;
            hostId = host.HostID.ToString();
            GetListStudents(hostId, Helpers.Settings.tmpFacultyIDSettings);
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem == null) return;
            var studentInfo = e.SelectedItem as Student;
            await Navigation.PushAsync(new StudentProfilePage(studentInfo));
            listView.SelectedItem = null;
        }

        public async void GetListStudents(string countryID, string facultyID)
        {

            try
            {
                string URL = Helpers.Settings.URLSettings + "api/DYCAndAdmin/listHStudentInHost?facultyId=" + facultyID + "&hostID=" + countryID;

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


                    listView.ItemsSource = listStudent.OrderBy(x => x.FullName).ToList();
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
                GetListStudents(hostId, Helpers.Settings.tmpFacultyIDSettings);
            }
            else
            {
                listView.ItemsSource = GetSearch(e.NewTextValue).OrderBy(x => x.FullName).ToList();
            }
        }

        IEnumerable<Student> GetSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return listStudent;
            return listStudent.Where(c => c.FullName.ToUpper().Contains(searchText.ToUpper()));
        }
    }
}