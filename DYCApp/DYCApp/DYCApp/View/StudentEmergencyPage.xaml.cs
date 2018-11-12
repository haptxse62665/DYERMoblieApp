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
	public partial class StudentEmergencyPage : ContentPage
	{
        
		public StudentEmergencyPage ()
		{
			InitializeComponent ();
            GetStudentEmergency(Helpers.Settings.FacultyIDSettings);
		}

        private void Button_Phone(object sender, EventArgs e)
        {
            
            string phone = (sender as Button).CommandParameter.ToString();
            if (phone.Length == 0)
            {
                DisplayAlert("No Phone Number", "Your student have not updated phone number yet", "OK");
            }
            else Device.OpenUri(new Uri("tel:" + phone));
        }

        private void Button_Location(object sender, EventArgs e)
        {
            string location  = (sender as Button).CommandParameter.ToString();
            try
            {

                Device.OpenUri(new Uri(location));
            }
            catch (Exception)
            {
                DisplayAlert("No Information", "Your student's location have problem","OK");
            }
        }

        private void Button_Profile(object sender, EventArgs e)
        {
            string username  = (sender as Button).CommandParameter.ToString();
            Student stuProfile = new Student();
            stuProfile.UserName = username;
            Navigation.PushAsync(new StudentProfilePage(stuProfile));
        }

        public async void GetStudentEmergency(string facultyID)
        {

            try
            {
                string URL = Helpers.Settings.URLSettings + "api/studentResponse/studentEmergency?facultyID=" + facultyID;

                HttpClient httpClient = new HttpClient();

                //// add header get
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);

                // try to use with post
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                ////post
                //var dict = new Dictionary<string, string>();
                //dict.Add("username", username);
                //var response = await httpClient.PostAsync(url, new FormUrlEncodedContent(dict));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<List<StudentEmergency>>(content);
                    ObservableCollection<StudentEmergency> listEmergency = new ObservableCollection<StudentEmergency>(tmp);


                    listView.ItemsSource = listEmergency;

                }
            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem == null) return;
            var stuInfo = e.SelectedItem as StudentEmergency;
            Student tmp = new Student();
            tmp.UserName = stuInfo.UserName;
            await Navigation.PushAsync(new StudentProfilePage(tmp));
            listView.SelectedItem = null;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var button = sender as MenuItem;
            var tmp = button.BindingContext as StudentEmergency;
            DeleteNoti(tmp.ID.ToString(), tmp.FullName);
        }

        public async void DeleteNoti(string studentEmergencyID, string fullname)
        {

            try
            {

                string URL = Helpers.Settings.URLSettings + "api/student/remmoveStudentEmergency/?studentEmergencyID=" + studentEmergencyID;


                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.PostAsync(new Uri(URL),null);

                if (response.IsSuccessStatusCode)
                {
                    DisplayAlert("Success", fullname + "'s situation was handled", "OK");
                    GetStudentEmergency(Helpers.Settings.FacultyIDSettings);
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