﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProfilePage : ContentPage
	{
		public UserProfilePage ()
		{
			InitializeComponent ();
		    
		}

       
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage()
            {
                IsGestureEnabled = false
            };
        }
    }
}