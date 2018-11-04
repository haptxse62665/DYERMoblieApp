using DYCApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DYCApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentProfilePage : ContentPage
	{
		public StudentProfilePage (Student student)
		{
			InitializeComponent ();
            GetStudentInfo(student.UserName);
        }
        public async void GetStudentInfo(string username)
        {

            try
            {
                string URL = Helpers.Settings.URLSettings + "api/student/info?username=" + username;

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
                    var studentInfo = JsonConvert.DeserializeObject<Student>(content);

                    StudentId.Text =studentInfo.StudentID;
                    FullName.Text = studentInfo.FullName;
                    Email.Text = studentInfo.Email;
                    PhoneNumber.Text = studentInfo.ContactNumber;
                    NewNumber.Text = studentInfo.NewPhoneNumber;
                    Faculty.Text = studentInfo.FacultyName;

                }
            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}