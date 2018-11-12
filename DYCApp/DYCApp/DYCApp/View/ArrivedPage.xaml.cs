﻿using DYCApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DYCApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArrivedPage : ContentPage
	{
        ObservableCollection<Country> listCountry;

        public ArrivedPage ()
		{
			InitializeComponent ();

            GetListCountry(Helpers.Settings.FacultyIDSettings);
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem == null) return;
            var country = e.SelectedItem as Country;
            await Navigation.PushAsync(new ListArrivedPage(country.CountryName));
            listView.SelectedItem = null;
        }
        public async void GetListCountry(string facultyID)
        {

            try
            {

                string URL = Helpers.Settings.URLSettings + "api/country/listCountryAndArrivedStudent?facultyID=" + facultyID;


                HttpClient httpClient = new HttpClient();

                // add header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helpers.Settings.TokenSettings);
                //httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer " + Helpers.Settings.TokenSettings);


                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<List<Country>>(content);
                     listCountry = new ObservableCollection<Country>(tmp);


                    listView.ItemsSource = listCountry;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("No Notification", "Can not connect to the server. Please check your internet", "OK");
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
            {
                GetListCountry(Helpers.Settings.FacultyIDSettings);
            }
            else
            {
                listView.ItemsSource = GetSearch(e.NewTextValue).OrderBy(x => x.CountryName).ToList();
            }
        }

        IEnumerable<Country> GetSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return listCountry;
            return listCountry.Where(c => c.CountryName.ToUpper().Contains(searchText.ToUpper()));
        }
    }
}