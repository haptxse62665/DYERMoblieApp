using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.FirebasePushNotification;
using StudentApp.Models;
using StudentApp.Pages;
using Xamarin.Forms;

namespace StudentApp
{
    public partial class MainPage : MasterDetailPage
    {
        string url = Helpers.Settings.URLSettings+"Token";
        public MainPage()
        {
            InitializeComponent();
            IsGestureEnabled = false;
            GetInfoToSubFirebase(Helpers.Settings.UsernameSettings);
        }

        private async void Button_Login(object sender, EventArgs e)
        {
            //Activity indicator visibility on
            activity_indicator.IsRunning = true;

            //get username and password
            var dict = new Dictionary<string, string>();
            dict.Add("username", Username.Text);
            dict.Add("password", Password.Text);
            dict.Add("grant_type", "password");
            var client = new HttpClient();

            try
            {
                //post
               var response = await client.PostAsync(url, new FormUrlEncodedContent(dict));

                if (response.IsSuccessStatusCode)
                {
                    var text = response.Content.ReadAsStringAsync(); //Json response from API

                    var token = JsonConvert.DeserializeObject<Token>(text.Result);

                   
                    Helpers.Settings.TokenSettings = token.access_token;
                    Helpers.Settings.UsernameSettings = token.userName;
                    
                    GetStudentInfo(Helpers.Settings.UsernameSettings);


                    Detail = new NavigationPage(new NotificationOverview())
                    {
                        BarBackgroundColor = Color.FromHex("#254F6E"),
                        BarTextColor = Color.White
                    };

                    IsPresented = false;
                    IsGestureEnabled = true;
                    
                }
                else
                {
                    DisplayAlert("Login fail", "Your username or password is incorrect", "OK");
                }
                activity_indicator.IsRunning = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                activity_indicator.IsRunning = false;
                DisplayAlert("Login fail", "Check your connection", "OK");
            }



            //direct login
            //Detail = new NavigationPage(new NotificationOverview())
            //{
            //    BarBackgroundColor = Color.FromHex("#254F6E"),
            //    BarTextColor = Color.White
            //};

            //IsPresented = false;
            //IsGestureEnabled = true;

            //CrossFirebasePushNotification.Current.Subscribe("Students");
        }
        private void Button_HostInfo(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new HostInfoPage())
            {
                BarBackgroundColor = Color.FromHex("#53CD8A"),
                BarTextColor = Color.White
            };
            IsPresented = false;
        }
        private void Button_Notifications(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new NotificationOverview())
            {
                BarBackgroundColor = Color.FromHex("#254F6E"),
                BarTextColor = Color.White
            };
            IsPresented = false;
        }

        private async void Button_Emergency(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new EmergencyPage())
            {
                BarBackgroundColor = Color.FromHex("#FE3F3F"),
                BarTextColor = Color.White
            };

            IsPresented = false;

        }
        private void Button_Arrival(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ArrivalPage())
            {
                BarBackgroundColor = Color.FromHex("#47FD91"),
                BarTextColor = Color.White
            };
            IsPresented = false;
        }

        private void Button_logout(object sender, EventArgs e)
        {
            CrossFirebasePushNotification.Current.UnsubscribeAll();

            IsGestureEnabled = false;
            Helpers.Settings.TokenSettings="";
            Application.Current.MainPage = new MainPage();

        }

        private void Button_UserProfile(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new UserProfilePage())
            {
                BarBackgroundColor = Color.FromHex("#04B5ED"),
                BarTextColor = Color.White
            };
            IsPresented = false;
        }

        protected override void OnAppearing()
        {

            Helpers.Settings.URLSettings = "http://202.160.1.102:8082/";
            if (Helpers.Settings.TokenSettings != "")
            {
                GetInfoToSubFirebase(Helpers.Settings.UsernameSettings);
                //CrossFirebasePushNotification.Current.Subscribe(Helpers.Settings.HostIDSettings);
                if (Helpers.Settings.ArrivalSettings.Equals(bool.TrueString))
                {
                    isArrical.IsVisible = false;
                    lineOfArrival.IsVisible = false;
                }
                GetStudentInfo(Helpers.Settings.UsernameSettings);
                Detail = new NavigationPage(new NotificationOverview())
                {
                    BarBackgroundColor = Color.FromHex("#254F6E"),
                    BarTextColor = Color.White
                };

                IsPresented = false;
                IsGestureEnabled = true;
            }
        }
        public async void GetStudentInfo(string username)
        {

            try
            {
                string URL = Helpers.Settings.URLSettings+"api/student/info?username=" +username;

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

                    if (studentInfo.Arrival)
                    {
                        isArrical.IsVisible = false;
                        lineOfArrival.IsVisible = false;
                    }
                    Helpers.Settings.ArrivalSettings = studentInfo.Arrival.ToString();
                    Helpers.Settings.HostIDSettings = studentInfo.HostID.ToString();
                    Helpers.Settings.IdSettings = studentInfo.Id.ToString();

                    Helpers.Settings.EmailSettings = studentInfo.Email;
                    Helpers.Settings.FullNameSettings = studentInfo.FullName;
                    Helpers.Settings.StudentIDSettings = studentInfo.StudentID;
                    Helpers.Settings.NewPhoneNumberSettings = studentInfo.NewPhoneNumber;
                    Helpers.Settings.ContactNumberSettings = studentInfo.ContactNumber;
                    Helpers.Settings.FacultySettings = studentInfo.FacultyName;

                }
            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public async void GetInfoToSubFirebase(string username)
        {

            try
            {
                string URL = Helpers.Settings.URLSettings + "api/student/info?username=" + username;

                HttpClient httpClient = new HttpClient();

                //// add header get
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);

                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var subFirebase = JsonConvert.DeserializeObject<SubFirebase>(content);

                    CrossFirebasePushNotification.Current.Subscribe(subFirebase.CountryName);
                    CrossFirebasePushNotification.Current.Subscribe(subFirebase.FacultyName);
                    CrossFirebasePushNotification.Current.Subscribe(subFirebase.HostName);


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

