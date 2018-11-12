using DYCApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DYCApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Create_NotificationToDYC : ContentPage
    {
        ObservableCollection<AllFaculty> allFaculty;
        public Create_NotificationToDYC()
        {
            InitializeComponent();
            GetAllFaculty();
        }
        public async void GetAllFaculty()
        {
            try
            {
                string URL = Helpers.Settings.URLSettings + "api/faculty/listAllFaculty";

                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<List<AllFaculty>>(content);
                     allFaculty = new ObservableCollection<AllFaculty>(tmp);
                    PickerFaculty.Items.Add("All");
                    foreach (var item in allFaculty)
                    {
                        if (Helpers.Settings.FacultyIDSettings.Equals(item.ID.ToString())) Helpers.Settings.FacultyNameSettings = item.FacultyName;
                        PickerFaculty.Items.Add(item.FacultyName);
                    }

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("No Notification", "Can not connect to the server. Please check your internet", "OK");
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
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

            SendNotiToDYC();
        }

        public async void SendNotiToDYC()
        {
            //PLACE DATABASE DATA

            HttpClient client = new HttpClient();

            string Url = "https://fcm.googleapis.com/fcm/send";
            string authKey = "AAAAJnnqqio:APA91bEwbXFnX_dU2_6vOHrTknS7WqmX86QC6lPxN8bURIZnAoOybe0_9d6zb-3O2DQdyWcy55nGhbTXAM04iroqF-a3R_WXob7Y1aEDTJ5rrclUNAEaL4BWl845ubW1YpNl7ejk_Syi";

            WebRequest tRequest = WebRequest.Create(Url);
            tRequest.Method = "post";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", authKey));
            tRequest.ContentType = "application/json";

            //Remove white spaces in faculty
            string topic = "", faculty = "";
            faculty = Regex.Replace(PickerFaculty.SelectedItem.ToString(), @"\s+", "");
            faculty = Regex.Replace(faculty, "[a-zA-Z0-9 ]", "");
            
            topic = faculty;

            //Define payload in post request
            var payload = new
            {
                condition = String.Format("'{0}' in topics", topic),
                notification = new
                {
                    title = EntryTitle.Text,
                    body = EntryContent.Text,
                    content_available = true,
                    icon = "alert32"
                },
                data = new
                {
                    title = EntryTitle.Text,
                    body = EntryContent.Text,
                    content_available = true,
                    icon = "alert32"
                },

            };
            //check title & content

            if (EntryTitle.Text.Length == 0)
            {
                DisplayAlert("Please input Title", "", "OK");
                return;
            }
            else if (Regex.IsMatch(EntryTitle.Text, "[^0-9a-zA-Z ]"))
            {
                DisplayAlert("Please don't input special character in Title", "", "OK");
                return;
            }

            if (EntryContent.Text.Length == 0)
            {
                DisplayAlert("Please input Content", "", "OK");
                return;
            }
            else if (Regex.IsMatch(EntryContent.Text, "[^0-9a-zA-Z ]"))
            {
                DisplayAlert("Please don't input special character in Content", "", "OK");
                return;
            }

            //check hazard , datetime
          
            if (datePicker.Date.ToShortDateString().Length == 0)
            {
                DisplayAlert("Please input Date", "", "OK");
                return;
            }
            string tmpFacultyID = "0";
            foreach (var item in allFaculty)
            {
                if (PickerFaculty.SelectedItem.ToString().Equals(item.FacultyName.ToString())) tmpFacultyID = item.ID.ToString();
                PickerFaculty.Items.Add(item.FacultyName);
            }
            //SaveNotificaiton(PickerCountry.SelectedItem.ToString(), Helpers.Settings.FacultyIDSettings, Helpers.Settings.IdSettings, host, EntryTitle.Text.ToString(), EntryContent.Text.ToString(), datePicker.Date.ToShortDateString().Replace('/', '-'), HazardLevel.SelectedItem.ToString());
            SaveNotificaiton( tmpFacultyID, Helpers.Settings.UserIDSettings, EntryTitle.Text.ToString(), EntryContent.Text.ToString(), datePicker.Date.ToString("MM-dd-yyyy"));

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;

            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null)
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                            }
                    }
                }
            }

        }
        public async void SaveNotificaiton( string facultyID, string userID, string title, string content, string dateHazard)
        {

            try
            {
                //string URL = Helpers.Settings.URLSettings + "api/userSendNotification/insertUserSentNotofication/?contentNotification=" + content + "&titleNotification=" + title + "&dateHazard=" + dateHazard + "&levelEmergency=" + level + "&UserID=" + userID + "&country=" + countryName + "&host=" + host + "&facultyID=" + facultyID;
                string URL = Helpers.Settings.URLSettings + "api/userSendNotification/insertAdminSentNotoficationToDYC/?contentNotification="+content+"&titleNotification="+title+"&dateHazard="+dateHazard +"&UserID="+userID+"&facultyID="+facultyID;


                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);

                HttpResponseMessage response = await httpClient.PostAsync(new Uri(URL), null);

                if (response.IsSuccessStatusCode)
                {

                    DisplayAlert("Send Success", "", "OK");
                }
                else
                {
                    DisplayAlert("Date:" + dateHazard, "URL" + URL, "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Send Fail", "Can not connect to the server. Please check your internet", "OK");
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

    }
}