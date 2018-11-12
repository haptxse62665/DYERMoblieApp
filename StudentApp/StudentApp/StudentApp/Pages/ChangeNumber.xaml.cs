using Newtonsoft.Json;
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
	public partial class ChangeNumber : ContentPage
	{
		public ChangeNumber ()
		{
			InitializeComponent ();
            oldNumber.Placeholder = Helpers.Settings.NewPhoneNumberSettings;
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (newNumber.Text != null)
            {
                UpdateNumberAsync(Helpers.Settings.IdSettings, newNumber.Text);
            }
            await Navigation.PopModalAsync();
        }

        //path
        //public async Task<bool> PathPhoneNumberAsync()
        //{
        //    string request = "";
        //    string body = "";

        //    using (HttpClientHandler ClientHandler = new HttpClientHandler())
        //    using (HttpClient Client = new HttpClient(ClientHandler))
        //    {
        //        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Settings.TokenSettings);
        //        using (HttpRequestMessage RequestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), request))
        //        {
        //            RequestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
        //            using (HttpResponseMessage ResponseMessage = await Client.SendAsync(RequestMessage))
        //            {
        //                string result = await ResponseMessage.Content.ReadAsStringAsync();

        //                if (ResponseMessage.StatusCode == HttpStatusCode.NoContent)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                      DisplayAlert("Fail", "Check your connection ", "OK");
        //                    //await Error.Send(ResponseMessage.StatusCode, request, result);
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //}

        //set Update

        public async void UpdateNumberAsync(string Id, string phone)
        {
            try
            {
                string URL = Helpers.Settings.URLSettings + "api/student/updateNewPhoneNumber/?id=" + Id+ "&phoneNumber="+phone;

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.PostAsync(new Uri(URL),null);

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
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}