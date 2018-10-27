using DYCApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DYCApp
{
        public partial class MainPage : MasterDetailPage
        {
            public MainPage()
            {
                InitializeComponent();

                IsGestureEnabled = false;
            }

            private async void Button_Login(object sender, EventArgs e)
            {
               
                Detail = new NavigationPage(new DYCOverview())
                {
                    BarBackgroundColor = Color.FromHex("#254f6e"),
                    BarTextColor = Color.White
                };
                IsPresented = false;
                IsGestureEnabled = true;
            

            }
            //homepage
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
            Detail = new NavigationPage(new Create_Notification())
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
    }
    }

