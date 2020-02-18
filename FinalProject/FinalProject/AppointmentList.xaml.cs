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
    public partial class AppointmentList : ContentPage
    {
        Doctor doctor;
        User user;
        public AppointmentList(Doctor doc, User usr)
        {
            InitializeComponent();
            doctor = doc;
            user = usr;
        }

        //When the appointments list is opened up, this makes a list of all appointments.
        //The embedded sql statement makes it so that only the appointments of the doctor you've listed show up
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Appointment>();
                var appointments = conn.Query<Appointment>("select * from Appointment where dId=? AND uId=?", doctor.Id, user.Id);
                appointmentListView.ItemsSource = appointments;
            }
        }
        private void newAppointmentClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AppointmentForum(doctor, user));
        }

        private void appointmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Appointment appt = e.SelectedItem as Appointment;
            Navigation.PushAsync(new AppointmentDetail(appt));
        }
    }
}