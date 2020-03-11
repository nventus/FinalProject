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
        List<Doctor> doctors = new List<Doctor>
        {

        };
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
                doctors = user.Doctors;
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
            Doctor doctor = new Doctor
            {

            };
            doctor.Users = new List<User>
            {
                user
            };
            Navigation.PushModalAsync(new DoctorForums(doctor));
        }

        //Sends the user to the appointment listings, with the chosen doctor's name as a parameter
        private void doctorSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Doctor selectedDoctor = e.SelectedItem as Doctor;
            Navigation.PushAsync(new AppointmentList(selectedDoctor, user));
        }
        private void OnSearch(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            doctorListView.ItemsSource = doctors.Where(doctor => (doctor.dName.ToUpper().Contains(searchBar.Text.ToUpper()) || doctor.dPractice.ToUpper().Contains(searchBar.Text.ToUpper())));
        }
        private void OnEdit(object sender, EventArgs e)
        {
            Doctor doc;
            var mi = ((MenuItem)sender);
            using(SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                doc = conn.GetWithChildren<Doctor>(mi.CommandParameter);
            }
            Navigation.PushAsync(new DoctorForums(doc));
        }
    }
}
