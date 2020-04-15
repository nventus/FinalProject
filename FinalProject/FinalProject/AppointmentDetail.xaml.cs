using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentDetail : ContentPage
    {
        Appointment viewedAppt;
        public AppointmentDetail(Appointment appt)
        {
            InitializeComponent();
            viewedAppt = appt;
            if (viewedAppt.aptDate > DateTime.Now)
            {
                DiagnosisLabel.IsVisible = false;
                Diagnosis.IsVisible = false;
                FollowUpRecs.IsVisible = false;
                FollowUpRecsLabel.IsVisible = false;
                PrescriptionLabel.IsVisible = false;
                PrescriptionsReceived.IsVisible = false;
                VaccineLabel.IsVisible = false;
                VaccinesReceived.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            string prescriptions = "";
            string vaccines = "";
            AppointmentDate.Text = viewedAppt.aptDate.ToString();
            Reason.Text = viewedAppt.reasonForVisit;
            
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                Doctor d = conn.GetWithChildren<Doctor>(viewedAppt.dId);
                DoctorName.Text = d.dName;
                Address.Text = d.dAddress;
            }

            if(viewedAppt.Prescriptions?.Any() == true)
            {
                foreach (Prescription p in viewedAppt.Prescriptions)
                {
                    prescriptions = prescriptions + p.RxName + ", ";
                }
                prescriptions = prescriptions.Substring(0, prescriptions.Length - 2);
            }
            PrescriptionsReceived.Text = prescriptions;

            if(viewedAppt.Vaccines?.Any() == true)
            {
                foreach(Vaccine v in viewedAppt.Vaccines)
                {
                    vaccines = vaccines + v.VaccineName + ", ";
                }
                vaccines = vaccines.Substring(0, vaccines.Length - 2);
            }

            Diagnosis.Text = viewedAppt.diagnosis;
            FollowUpRecs.Text = viewedAppt.followUpAdvice;
            PrescriptionsReceived.Text = prescriptions;
            VaccinesReceived.Text = vaccines;
        }

        private void editAppointmentClicked(object sender, EventArgs e)
        {
            if (viewedAppt.aptDate > DateTime.Now)
            {
                Navigation.PushModalAsync(new EditFutureAppointment(viewedAppt));
            }
            else
            {
                Navigation.PushModalAsync(new EditAppointment(viewedAppt));
            }
        }
    }
}