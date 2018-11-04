using DYCApp.Model;
using DYCApp.Models;
using DYCApp.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DYCApp
{
        public partial class MainPage : MasterDetailPage
        {
        string url = Helpers.Settings.URLSettings + "Token";
        public MainPage()
            {
                InitializeComponent();

                IsGestureEnabled = false;
            }

        private async void Button_Login(object sender, EventArgs e)
        {
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

                    GetUserInfo(Helpers.Settings.UsernameSettings);

                    if (!Helpers.Settings.RoleNameSettings.Equals("Student")){
                        Detail = new NavigationPage(new DYCOverview())
                        {
                            BarBackgroundColor = Color.FromHex("#254F6E"),
                            BarTextColor = Color.White
                        };

                        IsPresented = false;
                        IsGestureEnabled = true;
                    }
                    else
                    {
                        Helpers.Settings.TokenSettings = "";
                        DisplayAlert("Login fail", "Your account cannot be accepted in Admin App", "OK");
                    }
                    

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
            //Detail = new NavigationPage(new DYCOverview())
            //{
            //    BarBackgroundColor = Color.FromHex("#254F6E"),
            //    BarTextColor = Color.White
            //};

            //IsPresented = false;
            //IsGestureEnabled = true;
        }            //homepage
        private void Button_Overview(object sender, EventArgs e)
            {
                Detail = new NavigationPage(new DYCOverview())
                {
                    BarBackgroundColor = Color.FromHex("#254f6e"),
                    BarTextColor = Color.White
                };
                IsPresented = false;
                IsGestureEnabled = true;


                //logOut.IsVisible = true;

            }

       
        private void Button_CreateNotifications(object sender, EventArgs e)
        {
            var user = Helpers.Settings.RoleNameSettings;
            if (user == "Admin")
            {
                /* Show and hide DYC and Students for user Admin */
                NotificationToDYC.IsVisible = !NotificationToDYC.IsVisible;
                NotificationToStudents.IsVisible = !NotificationToStudents.IsVisible;
                NotificatonLine1.IsVisible = !NotificatonLine1.IsVisible;
                NotificationLine2.IsVisible = !NotificationLine2.IsVisible;
            }
            else
            {
                Detail = new NavigationPage(new Create_Notification())
                {
                    BarBackgroundColor = Color.FromHex("#254F6E"),
                    BarTextColor = Color.White
                };
                IsPresented = false;
            }
        }
        private void Button_CreateNotificationsToStudents(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Create_Notification())
            {
                BarBackgroundColor = Color.FromHex("#254F6E"),
                BarTextColor = Color.White
            };
            IsPresented = false;
        }

        private void Button_CreateNotificationsToDYC(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Create_NotificationToDYC())
            {
                BarBackgroundColor = Color.FromHex("#254F6E"),
                BarTextColor = Color.White
            };
            IsPresented = false;
        }

        private void Button_Responses(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ResponsesPage())
            {
                BarBackgroundColor = Color.FromHex("#254f6e"),
                BarTextColor = Color.White
            };
            IsPresented = false;

        }
        private void Button_Arrived(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ArrivedPage())
            {
                BarBackgroundColor = Color.FromHex("#254f6e"),
                BarTextColor = Color.White
            };
            IsPresented = false;
        }

        private void Button_logout(object sender, EventArgs e)
        {
            IsGestureEnabled = false;
            Helpers.Settings.TokenSettings = "";
            Application.Current.MainPage = new MainPage();
        }

        private void Button_TeacherProfile(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new TeacherProfilePage())
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
                GetUserInfo(Helpers.Settings.UsernameSettings);
                
                Detail = new NavigationPage(new DYCOverview())
                {
                    BarBackgroundColor = Color.FromHex("#254F6E"),
                    BarTextColor = Color.White
                };

                IsPresented = false;
                IsGestureEnabled = true;
            }
        }

        public async void GetUserInfo(string username)
        {

            try
            {
                string URL = Helpers.Settings.URLSettings + "api/dyc/inforByUserName?username=" + username;

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
                    var userInfo = JsonConvert.DeserializeObject<UserInfo>(content);

                   
                    //Helpers.Settings.ArrivalSettings = userInfo.Arrival.ToString();
                    Helpers.Settings.FacultyIDSettings = userInfo.FacultyId.ToString();
                    Helpers.Settings.IdSettings = userInfo.Id.ToString();
                    Helpers.Settings.RoleNameSettings = userInfo.RoleName.ToString();

                    Helpers.Settings.EmailSettings = userInfo.Email;
                    Helpers.Settings.FullNameSettings = userInfo.FullName;
                    Helpers.Settings.DYCIDSettings = userInfo.DYCID;
                    Helpers.Settings.ContactNumberSettings = userInfo.PhoneNumber;

                    if (Helpers.Settings.FacultyIDSettings.Length == 0) Helpers.Settings.FacultyIDSettings = "0";
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

