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
	public partial class Create_Notification : ContentPage
	{
		public Create_Notification ()
		{
			InitializeComponent ();
		    this.BindingContext = new List<string>()
		    {
		        "Test 1",
		        "Test 2",
		        "Test 3"
		    };
        }

	    //private void Button_CreateNotification(object sender, EventArgs e)
	    //{
	    //    popupLoginView.IsVisible = true;
	    //}

     //   private void Button_Clicked(object sender, EventArgs e)
     //   {
     //       popupLoginView.IsVisible = false;
     //   }
    }
}