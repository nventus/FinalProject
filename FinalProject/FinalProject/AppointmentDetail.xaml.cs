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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppointmentDate.Text = viewedAppt.aptDate.ToString();
            FollowUpDate.Text = viewedAppt.followUpApt.ToString();
            FollowUpRecs.Text = viewedAppt.followUpAdvice;
        }

        private void editAppointmentClicked(object sender, EventArgs e)
        {

        }
    }
}