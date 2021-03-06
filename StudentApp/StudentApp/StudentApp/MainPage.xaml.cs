﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApp.Pages;
using Xamarin.Forms;

namespace StudentApp
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            IsGestureEnabled = false;
        }

        private void Button_Login(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new NotificationOverview())
            {
                BarBackgroundColor = Color.FromHex("#254F6E"),
                BarTextColor = Color.White
            };
            IsPresented = false;
            IsGestureEnabled = true;
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
            Detail = new NavigationPage(new NotificationPage())
            {
                BarBackgroundColor = Color.FromHex("#254F6E"),
                BarTextColor = Color.White
            };
            IsPresented = false;
        }

        private void Button_Emergency(object sender, EventArgs e)
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

