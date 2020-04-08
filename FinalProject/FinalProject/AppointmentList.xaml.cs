using System;

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
                int result;
                Appointment appt;
                conn.CreateTable<Appointment>();
                var appointments = conn.Query<Appointment>("select * from Appointment where dId=? AND uId=?", doctor.Id, user.Id);
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
    }
}