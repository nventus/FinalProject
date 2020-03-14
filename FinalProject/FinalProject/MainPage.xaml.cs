using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.LocalNotifications;
using SQLiteNetExtensions.Extensions;

namespace FinalProject
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            /*
             * Initially opens up a database with all registered users. If the database is empty, then open up the user signup forum
             */
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();
                var users = conn.Table<User>().ToList();
                var doctors = conn.Table<Doctor>();
                if (users?.Any() == false)
                {
                    Navigation.PushModalAsync(new NavigationPage(new UserForums()));
                }
                else
                {
                    ListMenu.ItemsSource = users;

                    //Have to resubmit notifications after device restart because they are not saved.
                    var appts = conn.Table<Appointment>().ToList();
                    if(appts?.Any() == true)
                    {
                        foreach(var x in appts)
                        {
                            DateTime remindTime = x.reminderTime;
                            var doctor = conn.GetWithChildren<Doctor>(x.dId);

                            if (remindTime > DateTime.Now)
                            {
                                CrossLocalNotifications.Current.Show("Appointment Reminder", "You have an appointment with Dr. " + doctor.dName + " at " + x.aptDate.ToShortTimeString(), x.Id, remindTime);
                            }
                        }
                    }
                }
            }

        }
        private void UserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            User selectedUser = e.SelectedItem as User;
            Navigation.PushModalAsync(new NavigationPage(new OptionList(selectedUser.Id)));
        }

    }
}
