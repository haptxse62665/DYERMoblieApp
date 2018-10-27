using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationPage : ContentPage
	{
		public NotificationPage ()
		{
			InitializeComponent (); 
            
		}
        //Disable back-button
	    protected override bool OnBackButtonPressed()

	    {

	        return true;

	    }

        private async void Button_Ok(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            
        }

        private async void Button_NotOk(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            
        }
    }
}