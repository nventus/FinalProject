using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotifications;
using SQLiteNetExtensions.Extensions;

namespace FinalProject
{
    public partial class App : Application
    {
        public static string FilePath;
        public App(string filePath)
        {
            InitializeComponent();

            MainPage = new MainPage();
            FilePath = filePath;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();
                var users = conn.Table<User>().ToList();
                var doctors = conn.Table<Doctor>();

                if (users?.Any() == true)
                {
                    //Have to resubmit notifications after device restart because they are not saved.
                    var appts = conn.Table<Appointment>().ToList();
                    if (appts?.Any() == true)
                    {
                        foreach (var x in appts)
                        {
                            if (x.reminderTime > DateTime.Now)
                            {
                                var doctor = conn.GetWithChildren<Doctor>(x.dId);
                                CrossLocalNotifications.Current.Show("Appointment Reminder", "You have an appointment with Dr. " + doctor.dName + " at " + x.aptDate.ToShortTimeString(), x.Id, x.reminderTime);
                            }
                        }
                    }
                }
                conn.Close();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
