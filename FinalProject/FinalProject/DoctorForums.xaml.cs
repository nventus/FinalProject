using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorForums : ContentPage
    {
        User user;
        public DoctorForums(User usr)
        {
            InitializeComponent();
            user = usr;
            NameEntry.Text = "";
            PracticeEntry.Text = "";
            TypeEntry.Text = "";
            AddressEntry.Text = "";
            PhoneEntry.Text = "";
            EmailEntry.Text = "";
        }

        //Completes the forum necessary to add a new doctor to the list
        async void ButtonClicked(object sender, EventArgs e)
        {
            Doctor doc;
            Boolean duexists = false;
            Boolean dexists = false;
            Doctor doctor = new Doctor()
            {
                dName = NameEntry.Text,
                dPractice = PracticeEntry.Text,
                dType = TypeEntry.Text,
                dAddress = AddressEntry.Text,
                dPhone = PhoneEntry.Text,
                dEmail = EmailEntry.Text
            };
            doctor.dAddress = doctor.dAddress + " " + StateEntry.Text + " " + ZipCodeEntry.Text;

            //Only adds the document to the database if it has a valid marker for the Doctor's name
            if (doctor.dName.Equals(""))
            {
                await DisplayAlert("Oops!", "Your doctor needs to have a name", "OK");
            }
            else if (doctor.dPractice.Equals(""))
            {
                await DisplayAlert("Oops!", "Your doctor needs to have a practice", "OK");
            }
            else
            {
                //Checks to see whether the doctor already exists                
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Doctor>();
                    var doctors = conn.Query<Doctor>("select * from Doctor where dName=?", doctor.dName);
                    //If the doctor already exists, then make sure that the user accessing this doesn't already see that doctor
                    if (doctors?.Any() == true)
                    {
                        doc = conn.GetWithChildren<Doctor>(doctors[0].Id);
                        dexists = true;
                        foreach (User u in doc.Users)
                        {
                            if (u.Name.Equals(user.Name))
                            {
                                duexists = true;
                            }
                        }
                    }
                }

                //If the doctor-user combination already exists, then display an alert
                if (duexists)
                {
                    await DisplayAlert("Uh-oh!", user.Name + " already sees " + doctor.dName, "OK");
                }

                //If the doctor already exists, but not the doctor-user combination,
                //Then add the user to the doctor's list of patients,
                //and add the doctor to the user's list of doctors
                //Also adds the user's id and the doctor's id to the usersdoctors table required for a manytomany relationship

                //This iterates through a for loop, because it contains a list. But realistically,
                //it will never return a list of a value greater or less than 1
                else if (dexists)
                {

                    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<Doctor>();
                        conn.CreateTable<User>();
                        conn.CreateTable<UsersDoctors>();
                        var doctors = conn.Query<Doctor>("select * from Doctor where dName=? AND dPractice=?", doctor.dName, doctor.dPractice);
                        doc = conn.GetWithChildren<Doctor>(doctors[0].Id);
                        doc.Users.Add(user);
                        conn.UpdateWithChildren(doc);
                    }
                    await Navigation.PopModalAsync();
                }
                else
                {
                    doctor.Users = new List<User>
                    {

                    };
                    doctor.Appointments = new List<Appointment>
                    {

                    };
                    doctor.Prescriptions = new List<Prescription>
                    {

                    };
                    doctor.Users.Add(user);


                    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                    {
                        //Ignored if the table already exists
                        conn.CreateTable<Doctor>();
                        conn.CreateTable<User>();
                        conn.CreateTable<UsersDoctors>();
                        conn.CreateTable<Appointment>();
                        conn.CreateTable<Prescription>();
                        conn.Insert(doctor);
                        conn.UpdateWithChildren(doctor);
                    }
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}