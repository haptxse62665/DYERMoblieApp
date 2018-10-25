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
	public partial class ListStudentPage : ContentPage
	{
		public ListStudentPage ()
		{
			InitializeComponent ();
            listView.ItemsSource = new List<string> { "Wawa", "Fifi", "Binh","Vu", "Ha" };
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new StudentProfilePage());
        }
    }
}