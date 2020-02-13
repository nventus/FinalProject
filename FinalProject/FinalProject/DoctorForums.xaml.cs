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
    public partial class DoctorForums : ContentPage
    {
        User user;
        public DoctorForums(User usr)
        {
            InitializeComponent();
            user = usr;
        }

        //Completes the forum necessary to add a new doctor to the list
        async void ButtonClicked(object sender, EventArgs e)
        {
            Doctor doctor = new Doctor()
            {
                dName = NameEntry.Text,
                dPractice = PracticeEntry.Text,
                dType = TypeEntry.Text,
                dAddress = AddressEntry.Text,
                dPhone = PhoneEntry.Text,
                dEmail = EmailEntry.Text,
                uName = user.Name
            };

            //Only adds the document to the database if it has a valid marker for the Doctor's name
            if (doctor.dName == "")
            {

            }
            else
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    //Ignored if the table already exists
                    conn.CreateTable<Doctor>();
                    int rowsAdded = conn.Insert(doctor);

                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}