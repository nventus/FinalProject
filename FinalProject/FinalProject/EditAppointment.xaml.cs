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
    public partial class EditAppointment : ContentPage
    {
        Appointment appointment, apt;
        public EditAppointment(Appointment appt)
        {
            InitializeComponent();
            appointment = appt;
            apt = appt;
            AppointmentDateEntry.Date = appt.aptDate;
            AppointmentTimeEntry.Time = appt.aptDate.TimeOfDay;
            Reason.Text = appt.reasonForVisit;
            Diagnosis.Text = appt.diagnosis;
            FollowUpRecsEntry.Text = appt.followUpAdvice;
        }

        async void ButtonClicked(object sender, EventArgs e)
        {
            appointment.aptDate = AppointmentDateEntry.Date.Add(AppointmentTimeEntry.Time);
            appointment.reasonForVisit = Reason.Text;
            appointment.diagnosis = Diagnosis.Text;
            appointment.followUpAdvice = FollowUpRecsEntry.Text;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                User user;
                Doctor doctor;
                user= conn.GetWithChildren<User>(appointment.uId);
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
                    await DisplayAlert("Oops!", "Your appointment needs to have a future date. If you want to schedule " +
                    "a past appointment, go back and click the log appointments button.", "OK");
                    user.Appointments = new List<Appointment>
                    {
                        appointment
                    };
                }
                else
                {
                    await DisplayAlert("Oops!", "Your appointment needs to have a future date. If you want to schedule " +
                    "a past appointment, go back and click the log appointments button.", "OK");
                    user.Appointments.Add(appointment);
                }
                doctor = conn.GetWithChildren<Doctor>(appointment.dId);
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
                conn.Update(user);
                conn.Update(doctor);
                conn.Update(appointment);
            }
            await Navigation.PopModalAsync();
        }
    }
}