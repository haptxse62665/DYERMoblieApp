﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationOverview : ContentPage
	{
		public NotificationOverview ()
		{
			InitializeComponent ();

		    listView.ItemsSource = new List<string> { "Wafi", "Nur", "Arif" };
        }
	}
}