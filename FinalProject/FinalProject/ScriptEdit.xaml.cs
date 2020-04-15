using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;
using System.Collections.Generic;
namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScriptEdit : ContentPage
    {
        Prescription script;
        public ScriptEdit(Prescription p)
        {
            InitializeComponent();
            script = p;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RxName.Text = script.RxName;
            RxStart.Text = script.startDate;
            RxEnd.Date = DateTime.Parse(script.endDate);
        }
        async void ButtonClicked(object sender, EventArgs e)
        {
            script.endDate = RxEnd.Date.ToShortDateString();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                if (script.PrescriptionNotificationIDs != null)
                {
                    foreach (PrescriptionNotificationID notifID in script.PrescriptionNotificationIDs)
                    {
                        CrossLocalNotifications.Current.Cancel(notifID.Id);
                    }
                    //Delete the old notification IDs from the table
                    conn.Execute("DELETE FROM PrescriptionNotificationID WHERE pId = " + script.Id);
                }

                //Set the new reminders using the new reminder time.
                PrescriptionNotifClass.PrescriptionNotifHandler(script);

                conn.Update(script);
            }
           await Navigation.PopAsync();
        }
    }
}