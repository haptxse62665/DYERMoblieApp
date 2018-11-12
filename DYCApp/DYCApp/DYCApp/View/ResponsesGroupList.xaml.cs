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
	public partial class ResponsesGroupList : TabbedPage
	{
		public ResponsesGroupList ()
		{
			InitializeComponent ();
		}

        private void listViewOk_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void listViewNotOk_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void listViewWaiting_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
        //private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        //{
        //    //await Navigation.PushAsync(new RespondedOkayPage());
        //}
    }
}