using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
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
        int uid;
        public DoctorList(int id)
        {
            InitializeComponent();
            uid = id;          
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();
                //Ignored if the table already exists
                conn.CreateTable<Doctor>();
                conn.CreateTable<UsersDoctors>();
                conn.CreateTable<Vaccine>();
                user = conn.GetWithChildren<User>(uid);
            }
            doctorListView.ItemsSource = user.Doctors;
        }

        //Brings the user to the new doctor forum
        private void newDoctorClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DoctorForums(user));
        }

        //Sends the user to the appointment listings, with the chosen doctor's name as a parameter
        private void doctorSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Doctor selectedDoctor = e.SelectedItem as Doctor;
            Navigation.PushAsync(new AppointmentList(selectedDoctor, user));
        }

    }
}
