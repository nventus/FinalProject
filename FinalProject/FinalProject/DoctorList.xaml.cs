using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;

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
                conn.CreateTable<Allergy>();
                conn.CreateTable<UsersAllergies>();
                conn.CreateTable<Prescription>();
                conn.CreateTable<Tables.Conditions>();
                conn.CreateTable<UsersConditions>();
                user = conn.GetWithChildren<User>(uid);
            }
            int result;
            Doctor doc;
            if (user.Doctors.Count > 0)
            {
                List<Doctor> doctors = user.Doctors;
                for (int i = 0; i < doctors.Count - 1; i++)
                {
                    for (int j = 0; j < doctors.Count - i - 1; j++)
                    {
                        result = String.Compare(doctors[j].dName, doctors[j + 1].dName);
                        if (result > 0)
                        {
                            doc = doctors[j];
                            doctors[j] = doctors[j + 1];
                            doctors[j + 1] = doc;
                        }
                    }
                }
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
            Navigation.PushModalAsync(new AppointmentList(user, selectedDoctor));
        }
    }
}
