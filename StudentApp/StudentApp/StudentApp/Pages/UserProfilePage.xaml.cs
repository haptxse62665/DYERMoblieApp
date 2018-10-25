using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Student std1=new Student("16b2197", "16b2197@ubd.edu.bn", 8800271, "Wafi Hs","FOS", 8888888);
		    Student std2 = new Student("16b2197", "16b2197@ubd.edu.bn", 8800271, "Wafi Hs", "FOS", 8888888);
		    Student std3 = new Student("16b2197", "16b2197@ubd.edu.bn", 8800271, "Wafi Hs", "FOS", 8888888);
		    Student std4 = new Student("16b2197", "16b2197@ubd.edu.bn", 8800271, "Wafi Hs", "FOS", 8888888);
		    
		}

       
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage()
            {
                IsGestureEnabled = false
            };
        }
    }
}