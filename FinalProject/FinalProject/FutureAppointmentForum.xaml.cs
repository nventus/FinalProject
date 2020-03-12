using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FutureAppointmentForum : ContentPage
    {
        bool editing;
        Appointment appointment;
        public FutureAppointmentForum(Appointment appt)
        {
            InitializeComponent();
            appointment = appt;
            Reason.Text = appt.reasonForVisit;
            if (appt.aptDate.Year < 2000)
            {
                AppointmentDateEntry.Date = DateTime.Now;
                AppointmentTimeEntry.Time = DateTime.Now.TimeOfDay;
                BeforeAppt.Time = DateTime.Now.TimeOfDay;
                editing = false;
            }
            else
            {
                AppointmentDateEntry.Date = appointment.aptDate;
                AppointmentTimeEntry.Time = appointment.aptDate.TimeOfDay;
                BeforeAppt.Time = DateTime.Now.TimeOfDay;
                editing = true;
            }
        }

        async void ButtonClicked(object sender, EventArgs e)
        {
            User user;
            Doctor doctor;
            appointment.aptDate = AppointmentDateEntry.Date.Add(AppointmentTimeEntry.Time);
            appointment.reasonForVisit = Reason.Text;

            if(appointment.aptDate > DateTime.Now)
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    user = conn.GetWithChildren<User>(appointment.uId);
                    doctor = conn.GetWithChildren<Doctor>(appointment.dId);
                }

                if (!editing)
                {
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
                    DateTime RemindTime = appointment.aptDate;
                    RemindTime = RemindTime.Date + BeforeAppt.Time;
                    CrossLocalNotifications.Current.Show("Appointment Reminder", "You have an appointment with Dr. " + doctor.dName + " at " + appointment.aptDate.ToShortTimeString(), appointment.Id, RemindTime);
                    //Will have to call CrossLocalNotifications.Current.Cancel(appointment.Id); to remove reminder if we add the option to delete appointments.
                }
                else
                {
                    for (int i = 0; i < user.Appointments.Count; i++)
                    {
                        if (user.Appointments[i].Id == appointment.Id)
                        {
                            user.Appointments.RemoveAt(i);
                            break;
                        }
                    }
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

                    for (int i = 0; i < doctor.Appointments.Count; i++)
                    {
                        if (doctor.Appointments[i].Id == appointment.Id)
                        {
                            doctor.Appointments.RemoveAt(i);
                            break;
                        }
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
                        conn.Update(appointment);
                        conn.Update(user);
                        conn.Update(doctor);
                    }
                }
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