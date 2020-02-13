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
    public partial class AppointmentForum : ContentPage
    {
        Doctor doctor;
        User user;
        public AppointmentForum(Doctor doc, User usr)
        {
            InitializeComponent();
            doctor = doc;
            user = usr;
        }

        async void ButtonClicked(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment()
            {
                aptDate = AppointmentDateEntry.Date.Add(AppointmentTimeEntry.Time).ToString(),
                followUpApt = FollowUpDateEntry.Date.Add(FollowUpTimeEntry.Time).ToString(),
                followUpAdvice = FollowUpRecsEntry.Text,
                prescriptions = PrescriptionEntry.Text,
                DName = doctor.dName,
                uName = user.Name
            };

            //Only adds the document to the database if it has a valid marker for the Doctor's name
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                //Ignored if the table already exists
                conn.CreateTable<Appointment>();
                int rowsAdded = conn.Insert(appointment);
            }
            await Navigation.PopModalAsync();
        }
    }
}