using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserForums : ContentPage
    {
        public UserForums()
        {
            InitializeComponent();
        }
        async void ButtonClicked(object sender, EventArgs e)
        {

            User user = new User()
            {
                Name = nameEntry.Text,
                Birthday = (bdayPick.Date).ToShortDateString(),
                Email = emailEntry.Text,
            };

            if (user.Name == "")
            {
            }
            else
            {
                user.Appointments = new List<Appointment>
                {

                };
                user.Prescriptions = new List<Prescription>
                {

                };
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<User>();
                    conn.CreateTable<Appointment>();
                    conn.CreateTable<Prescription>();
                    conn.Insert(user);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}