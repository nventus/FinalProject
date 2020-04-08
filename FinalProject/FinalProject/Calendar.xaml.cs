using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentPage
    {
        int uid;
        public Calendar(int id)
        {
            InitializeComponent();
            uid = id;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                var appointments = conn.Query<Appointment>("select * from Appointment where uId=?", uid);
                List<XamForms.Controls.SpecialDate> specialDates = new List<XamForms.Controls.SpecialDate>
                {

                };
                List<DateTime> d = new List<DateTime>
                {

                };
                foreach (Appointment a in appointments)
                {
                    if (a.aptDate > DateTime.Now)
                    {
                        Doctor doc = conn.Get<Doctor>(a.dId);
                        specialDates.Add(
                           new XamForms.Controls.SpecialDate(a.aptDate)
                           {
                               Selectable = true,
                               BackgroundPattern = new XamForms.Controls.BackgroundPattern(1)
                               {
                                   Pattern = new List<XamForms.Controls.Pattern>
                                   {
                                        new XamForms.Controls.Pattern { WidthPercent = 1f, HightPercent = 0.4f, Color = Color.Transparent },
                                        new XamForms.Controls.Pattern{ WidthPercent = 1f, HightPercent = 0.6f, Color = Color.CornflowerBlue, Text = doc.dName, TextColor=Color.Black, TextSize=11,TextAlign=XamForms.Controls.TextAlign.Middle},
                                   }
                               }
                           });

                    }
                    MyCal.SpecialDates = specialDates;
                }
            }
        }
        public Command DateChosen()
        {
            DisplayAlert("D", "A", "OK");
            return new Command((obj) =>
            {
                DisplayAlert("D", "A", "OK");
                System.Diagnostics.Debug.WriteLine(obj as DateTime?);
            });
        }
    }
}