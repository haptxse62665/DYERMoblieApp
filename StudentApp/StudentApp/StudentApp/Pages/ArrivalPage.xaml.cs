using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArrivalPage : ContentPage
	{
		public ArrivalPage ()
		{
			InitializeComponent ();
		}

        public async void UpdateNumberAsync(string Id, string phone)
        {
            try
            {
                string URL = Helpers.Settings.URLSettings + "api/student/updateNewPhoneNumber/?id=" + Id + "&phoneNumber=" + phone;

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.PostAsync(new Uri(URL), null);

                if (response.IsSuccessStatusCode)
                {
                    //var content = await response.Content.ReadAsStringAsync();
                    //var tmp = JsonConvert.DeserializeObject<List<Notification>>(content);

                    Helpers.Settings.NewPhoneNumberSettings = phone;
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

        //public async void UpdateByPostAsync(string Id, string phone)
        //{
        //    string URL = Helpers.Settings.URLSettings;

        //    var dict = new Dictionary<string, string>();
        //    dict.Add("id", Id);
        //    dict.Add("phoneNumber", phone);
        //    var client = new HttpClient();

        //    try
        //    {
        //        //post
        //        var response = await client.PostAsync(URL, new FormUrlEncodedContent(dict));

        //        if (response.IsSuccessStatusCode)
        //        {
        //            //var content = await response.Content.ReadAsStringAsync();
        //            //var tmp = JsonConvert.DeserializeObject<List<Notification>>(content);

        //            Helpers.Settings.NewPhoneNumberSettings = phone;
        //            DisplayAlert("Success", " ", "OK");
        //        }
        //        else
        //        {
        //            DisplayAlert("Fail", " ", "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayAlert("Fail", "Check your connection ", "OK");
        //        System.Diagnostics.Debug.WriteLine(ex.ToString());
        //    }

        //}

            //update status arrival
        public async void UpdateArrivalAsync(string Id)
        {
            try
            {
                string URL = Helpers.Settings.URLSettings + "api/student/updateArrival/?id=" + Id;

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.PostAsync(new Uri(URL), null);

                if (response.IsSuccessStatusCode)
                {
                    //var content = await response.Content.ReadAsStringAsync();
                    //var tmp = JsonConvert.DeserializeObject<List<Notification>>(content);
                    Helpers.Settings.ArrivalSettings = bool.TrueString;
                    DisplayAlert("Update Arrival Success", " ", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Fail", "Check your connection ", "OK");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (newNumber.Text != null)
            {
                UpdateNumberAsync(Helpers.Settings.IdSettings, newNumber.Text);
            }
            //Helpers.Settings.ArrivalSettings = bool.TrueString;
            UpdateArrivalAsync(Helpers.Settings.IdSettings);
            Application.Current.MainPage = new MainPage();
        }
    }
}