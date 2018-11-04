using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.FirebasePushNotification;
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
            StudentId.Text = Helpers.Settings.StudentIDSettings;
            FullName.Text = Helpers.Settings.FullNameSettings;
            Email.Text = Helpers.Settings.EmailSettings;
            PhoneNumber.Text = Helpers.Settings.ContactNumberSettings;
            NewNumber.Text = Helpers.Settings.NewPhoneNumberSettings;
            Faculty.Text = Helpers.Settings.FacultySettings;
		}

       
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            CrossFirebasePushNotification.Current.UnsubscribeAll();

            Helpers.Settings.TokenSettings = "";
            Application.Current.MainPage = new MainPage()
            {
                IsGestureEnabled = false
            };
            
        }

        private async void Button_UpdatePhoneNumber(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ChangeNumber());
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    popupLoginView.IsVisible = false;
        //}
    }
}