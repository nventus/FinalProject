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
    public partial class AppointmentDetail : ContentPage
    {
        Appointment viewedAppt;
        public AppointmentDetail(Appointment appt)
        {
            InitializeComponent();
            viewedAppt = appt;
            if(viewedAppt.aptDate > DateTime.Now)
            {
                DiagnosisLabel.IsVisible = false;
                Diagnosis.IsVisible = false;
                FollowUpRecs.IsVisible = false;
                FollowUpRecsLabel.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppointmentDate.Text = viewedAppt.aptDate.ToString();
            FollowUpRecs.Text = viewedAppt.followUpAdvice;
            Reason.Text = viewedAppt.reasonForVisit;
            Diagnosis.Text = viewedAppt.diagnosis;
        }
    }
}