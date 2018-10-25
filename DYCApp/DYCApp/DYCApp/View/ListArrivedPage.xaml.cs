using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DYCApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListArrivedPage : ContentPage
	{
		public ListArrivedPage ()
		{
			InitializeComponent ();
            listViewARRIVED.ItemsSource = new List<string> { "WaWa", "Fidaus", "Vu" };
            listView.ItemsSource = new List<string> { "WaWa", "Fidaus", "Vu" };
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new StudentProfilePage());
        }
    }
}