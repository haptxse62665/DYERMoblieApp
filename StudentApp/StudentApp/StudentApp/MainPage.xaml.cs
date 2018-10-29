using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentApp.Models;
using StudentApp.Pages;
using Xamarin.Forms;

namespace StudentApp
{
    public partial class MainPage : MasterDetailPage
    {
        string url = "202.190.1.102:8082/Token";
        public MainPage()
        {
            InitializeComponent();
            IsGestureEnabled = false;
        }

        private async void Button_Login(object sender, EventArgs e)
        {

            //get username and password
            var dict = new Dictionary<string, string>();
            dict.Add("username", Username.Text);
            dict.Add("password", Password.Text);
            dict.Add("grant_type", "password");
            var client = new HttpClient();

            try
            {
                var response = await client.PostAsync(url, new FormUrlEncodedContent(dict));
                if (response.IsSuccessStatusCode)
                {
                    //var text = response.Content.ReadAsStringAsync(); //Json response from API

                    //var token = JsonConvert.DeserializeObject<JsonToken>(text.Result);


                    var content = await response.Content.ReadAsStringAsync();
                    JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);
                    var accessToken = jwtDynamic.Value<string>("access_token");
                    var tokenTypes = jwtDynamic.Value<string>("token_types");
                    Helpers.Settings.TokenSettings = tokenTypes + " " + accessToken;
                    Detail = new NavigationPage(new HostInfoPage())
                    {
                        BarBackgroundColor = Color.FromHex("#254F6E"),
                        BarTextColor = Color.White
                    };

                    IsPresented = false;
                    IsGestureEnabled = true;
                }
            }
            catch (Exception)
            {
                DisplayAlert("Login fail", "Your username or password is incorrect", "OK");
            }



            //direct login
            //Detail = new NavigationPage(new NotificationOverview())
            //{
            //    BarBackgroundColor = Color.FromHex("#254F6E"),
            //    BarTextColor = Color.White
            //};

            //IsPresented = false;
            //IsGestureEnabled = true;
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
            IsGestureEnabled = false;
            Application.Current.MainPage=new MainPage();
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
    }
}

