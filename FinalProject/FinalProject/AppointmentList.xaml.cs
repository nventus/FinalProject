using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Linq;
using SQLiteNetExtensions.Extensions;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentList : ContentPage
    {
        User user;
        Doctor doctor;
        List<Appointment> appointmentlist = new List<Appointment>();
        public AppointmentList(User usr)
        {
            InitializeComponent();
            user = usr;
        }

        //Overloaded constructor for when we want to see an appointment list for a specific doctor/user combination.
        public AppointmentList(User usr, Doctor doc)
        {
            InitializeComponent();
            user = usr;
            doctor = doc;
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
                user = conn.GetWithChildren<User>(user.Id);
                List<Appointment> appointments = user.Appointments;

                //If the doctor var is initialized, then we have been sent here directly from the DoctorList and the user should only see appointments from the doctor they selected there.
                if (doctor != null)
                {
                    appointments = appointments.Where(item => item.dId.Equals(doctor.Id)).ToList();
                }
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
                appointmentlist.Clear();
                for (int i = 0; i < appointments.Count - 1; i ++)
                {
                    appointmentlist.Add(appointments[i]);
                    if (appointmentlist[i].reasonForVisit == null)
                    {
                            appointmentlist[i].reasonForVisit = "";
                    }
                    if (appointmentlist[i].diagnosis == null)
                    {
                        appointmentlist[i].diagnosis = "";
                    }
                    if (appointmentlist[i].dName == null)
                    {
                        appointmentlist[i].dName = "";
                    }
                }
                appointmentListView.ItemsSource = appointments;

            }
        }
        private void newAppointmentClicked(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment
            {
                uId = user.Id
            };
            Navigation.PushModalAsync(new AppointmentForum(appointment));
        }
        private void futureAppointmentClicked(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment
            {
                uId = user.Id
            };
            Navigation.PushModalAsync(new FutureAppointmentForum(appointment));
        }

        private void appointmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Appointment appt = e.SelectedItem as Appointment;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                appt = conn.GetWithChildren<Appointment>(appt.Id);
            }
                Navigation.PushAsync(new AppointmentDetail(appt));
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                appointmentListView.ItemsSource = appointmentlist;
                OnAppearing();
            }
            else
            {
                appointmentListView.ItemsSource = appointmentlist.Where(appt => (appt.diagnosis.ToLower().Contains(e.NewTextValue.ToLower()) ||
                                                                                 appt.reasonForVisit.ToLower().Contains(e.NewTextValue.ToLower()) ||
                                                                                 appt.dName.ToLower().Contains(e.NewTextValue.ToLower())));
            }
        }
    }
}