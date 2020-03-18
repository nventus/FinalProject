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
    public partial class AppointmentList : ContentPage
    {
        Doctor doctor;
        User user;
        List<Appointment> appointments = new List<Appointment>
        {

        };
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
                int result;
                Appointment appt;
                conn.CreateTable<Appointment>();
                appointments = conn.Query<Appointment>("select * from Appointment where dId=? AND uId=?", doctor.Id, user.Id);
                if (appointments.Count > 0)
                {
                    for (int i = 0; i < appointments.Count - 1; i++)
                    {
                        for (int j = 0; j < appointments.Count - i - 1; j++)
                        {
                            result = DateTime.Compare(appointments[j + 1].aptDate, appointments[j].aptDate);
                            if (result > 0)
                            {
                                appt = appointments[j];
                                appointments[j] = appointments[j + 1];
                                appointments[j + 1] = appt;
                            }
                        }
                    }
                }
                appointmentListView.ItemsSource = appointments;
            }
        }
        private void newAppointmentClicked(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment
            {
                dId = doctor.Id,
                uId = user.Id
            };
            Navigation.PushModalAsync(new AppointmentForum(appointment));
        }
        private void futureAppointmentClicked(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment
            {
                dId = doctor.Id,
                uId = user.Id
            };
            Navigation.PushModalAsync(new FutureAppointmentForum(appointment));
        }

        private void appointmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Appointment appt = e.SelectedItem as Appointment;
            Navigation.PushAsync(new AppointmentDetail(appt));
        }
        private void OnSearch(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            appointmentListView.ItemsSource = appointments.Where(appointment => appointment.reasonForVisit.ToUpper().Contains(searchBar.Text.ToUpper()));        
        }
        private void OnEdit(object sender, EventArgs e)
        {
            Appointment apt;
            int result;
            var mi = ((MenuItem)sender);
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                apt = conn.GetWithChildren<Appointment>(mi.CommandParameter);
            }
            result = DateTime.Compare(apt.aptDate, DateTime.Now);
            if (result <= 0)
            {
                Navigation.PushAsync(new EditAppointment(apt));
            }
            else
            {
                Navigation.PushModalAsync(new FutureAppointmentForum(apt));
            }
        }
        private void OnDelete(object sender, EventArgs e)
        {
            Appointment apt;
            int result;
            var mi = ((MenuItem)sender);
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                apt = conn.GetWithChildren<Appointment>(mi.CommandParameter);
            }
            result = DateTime.Compare(apt.aptDate, DateTime.Now);
            if (result <= 0)
            {
                DisplayAlert("Oops!", "You can only delete future appointments", "OK");
            }
            else
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    conn.Delete(apt, recursive: true);
                }
            }
            OnAppearing();
        }
    }
}