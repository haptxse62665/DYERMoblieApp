﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DYCApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeacherProfilePage : ContentPage
	{
		public TeacherProfilePage ()
		{
			InitializeComponent ();
            FullName.Text = Helpers.Settings.FullNameSettings;
            Email.Text = Helpers.Settings.EmailSettings;
            Phone.Text = Helpers.Settings.ContactNumberSettings;
		}
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Helpers.Settings.TokenSettings = "";
            Helpers.Settings.RoleNameSettings = "";
            Application.Current.MainPage = new MainPage()
            {
                IsGestureEnabled = false
            };
        }
    }
}