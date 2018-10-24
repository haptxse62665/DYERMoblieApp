using DYCApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DYCApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListHostPage : ContentPage
	{
		public ListHostPage ()
		{
			InitializeComponent ();
            listView.ItemsSource = new List<string> { "Google", "Facebook", "Amazon" };
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new ListStudentPage());
        }
    }
}