using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DYCApp;
using DYCApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Graphics;


[assembly: ExportRenderer(typeof(DatePickerCtrl), typeof(DatePickerCtrlRenderer))]
namespace DYCApp.Droid
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class DatePickerCtrlRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            this.Control.SetTextColor(Android.Graphics.Color.Rgb(62, 60, 60));
            this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            this.Control.SetPadding(20, 0, 0, 0);

            GradientDrawable gd = new GradientDrawable();
            gd.SetCornerRadius(25); //increase or decrease to changes the corner look 
            gd.SetColor(Android.Graphics.Color.Transparent);
            gd.SetStroke(3, Android.Graphics.Color.Black);

            this.Control.SetBackgroundDrawable(gd);

            DatePickerCtrl element = Element as DatePickerCtrl;

            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
            }

            this.Control.TextChanged += (sender, arg) => {
                var selectedDate = arg.Text.ToString();
                if (selectedDate == element.Placeholder)
                {
                    Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            };
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}