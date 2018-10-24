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
    public partial class DYCOverview : ContentPage
    {
        public DYCOverview()
        {
            InitializeComponent();

            listView.ItemsSource = new List<string> { "Viet Nam", "Japan", "Malay" };
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new ListHostPage());
        }
    }
}