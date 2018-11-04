using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationPage : ContentPage
	{
        string notiID;
		public NotificationPage (Notification noti)
		{
            
            InitializeComponent ();
            BindingContext = noti;
            notiID = noti.ID.ToString();
        }
        //Disable back-button
	    //protected override bool OnBackButtonPressed()

	    //{

	    //    return true;

	    //}

        private  void Button_Ok(object sender, EventArgs e)
        {
            UpdateStatusAsync(notiID, "OK");
            Navigation.PopModalAsync();
            
        }

        private  void Button_NotOk(object sender, EventArgs e)
        {
            UpdateStatusAsync(notiID, "Not OK");
            Navigation.PopModalAsync();
        }

        public async void UpdateStatusAsync(string Id, string status)
        {
            try
            {
                string URL = Helpers.Settings.URLSettings + "api/student/updateStudentResponseStatus?id=" + Id + "&status=" + status;

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    //var content = await response.Content.ReadAsStringAsync();
                    //var tmp = JsonConvert.DeserializeObject<List<Notification>>(content);
                    
                    DisplayAlert("Success", " ", "OK");
                }
                else
                {
                    DisplayAlert("Fail", " ", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Fail", "Check your connection ", "OK");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}