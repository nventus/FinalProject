using Plugin.LocalNotifications;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FutureAppointmentForum : ContentPage
    {
        Appointment appointment;
        Doctor doctor = new Doctor();
        List<Doctor> doctorlist;
        public FutureAppointmentForum(Appointment appt)
        {
            InitializeComponent();
            appointment = appt;
            AppointmentDateEntry.Date = DateTime.Now;
            AppointmentTimeEntry.Time = DateTime.Now.TimeOfDay;
            BeforeAppt.Time = DateTime.Now.TimeOfDay;

            doctor.dName = "12345";
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                User user = conn.GetWithChildren<User>(appointment.uId);
                List<string> dNames = new List<string>();
                foreach (Doctor d in user.Doctors)
                {
                    dNames.Add(conn.GetWithChildren<Doctor>(d.Id).dName);
                }
                DoctorPicker.ItemsSource = dNames;
                //var doctors = conn.Query<Doctor>("select * from Doctor where Id=?", appointment.dId);
                //doctor = doctors[0];
            }
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    var doctors = conn.Query<Doctor>("select * from Doctor where dName=?", (string)picker.ItemsSource[selectedIndex]);
                    doctor = conn.GetWithChildren<Doctor>(doctors[0].Id);
                }
            }
        }

        async void ButtonClicked(object sender, EventArgs e)
        {
            User user;

            if (doctor.dName.Equals("12345"))
            {
                await DisplayAlert("Oops!", "Your need to choose a doctor", "OK");
            }
            else
            {
                appointment.aptDate = AppointmentDateEntry.Date + AppointmentTimeEntry.Time;
                appointment.reasonForVisit = Reason.Text;
                appointment.dName = doctor.dName;

                //DateTime is immutable, so we will create a new DateTime and set it's date to the current appointment date.
                DateTime RemindTime = appointment.aptDate.Date + BeforeAppt.Time;
                //RemindTime will have the date of the appointment and the reminder time chosen in the BeforeAppt TimePicker.
                //Storing the reminderTime as part of the appointment entry in the database.
                //We will need to re-submit any pending notifications after device reboot.
                appointment.reminderTime = RemindTime;
                if (appointment.aptDate > DateTime.Now)
                {
                    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                    {
                        user = conn.GetWithChildren<User>(appointment.uId);

                        doctor = conn.Query<Doctor>("select * from Doctor where dName=?", doctor.dName)[0];
                        doctor = conn.GetWithChildren<Doctor>(doctor.Id);
                    }
                    appointment.dId = doctor.Id;

                    if (user.Appointments == null)
                    {
                        user.Appointments = new List<Appointment>
                        {
                            appointment
                        };
                    }
                    else
                    {
                        user.Appointments.Add(appointment);
                    }
                        

                    if (doctor.Appointments == null)
                    {
                        doctor.Appointments = new List<Appointment>
                        {
                            appointment
                        };
                    }
                    else
                    {
                        doctor.Appointments.Add(appointment);
                    }
                    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                    {
                        //Ignored if the table already exists
                        conn.Insert(appointment);
                        conn.Update(user);
                        conn.Update(doctor);
                    }

                    CrossLocalNotifications.Current.Show("Appointment Reminder", "You have an appointment with Dr. " + doctor.dName + " at " + appointment.aptDate.ToShortTimeString(), appointment.Id, RemindTime);
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Oops!", "Your appointment needs to have a future date. If you want to schedule " +
                        "a past appointment, go back and click the log appointments button.", "OK");
                }
            }
        }
    }
}