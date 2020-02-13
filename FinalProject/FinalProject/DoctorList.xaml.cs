using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorList : ContentPage
    {
        User user;
        public DoctorList(User usr)
        {
            InitializeComponent();
            user = usr;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            /*
             * Initially opens up a database with all registered users. If the database is empty, then open up the user signup forum
             */


            //Creates the Doctor table if it doesn't exist. Sends them all to a list at the mainpage.
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                //Ignored if the table already exists
                conn.CreateTable<Doctor>();
                var doctors = conn.Query<Doctor>("select * from Doctor where uName=?", user.Name);
                doctorListView.ItemsSource = doctors;
            }
        }

        //Brings the user to the new doctor forum
        private void newDoctorClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new DoctorForums(user));
        }

        //Sends the user to the appointment listings, with the chosen doctor's name as a parameter
        private void doctorSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Doctor selectedDoctor = e.SelectedItem as Doctor;
            Navigation.PushAsync(new AppointmentList(selectedDoctor, user));
        }
    }
}
