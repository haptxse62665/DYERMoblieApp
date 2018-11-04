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
	public partial class Create_NotificationToDYC : ContentPage
	{
		public Create_NotificationToDYC ()
		{
			InitializeComponent ();
		}
	    private async void Button_CreateNotification(object sender, EventArgs e)
	    {
	        Create_Button.IsVisible = false;
	        ActivityFrame.IsVisible = true;
	        ActivityIndicator.IsVisible = true;
	        ActivityIndicator.IsRunning = true;
	        await Task.Delay(2000);
	        ActivityIndicator.IsRunning = false;
	        ActivityIndicator.IsVisible = false;
	        ActivityFrame.IsVisible = false;
	        Created_Button.IsVisible = true;
	        await Task.Delay(1000);
	        Created_Button.IsVisible = false;
	        Create_Button.IsVisible = true;
        }
    }
}