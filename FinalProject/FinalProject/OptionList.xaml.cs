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
    public partial class OptionList : ContentPage
    {
        int uid;
        public OptionList(int id)
        {
            InitializeComponent();
            uid = id;
        }

        private void newUserClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new UserForums()));
        }
        private void LogAptClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DoctorList(uid));
        }

        private void RxClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScriptList(uid));
        }

        private void VaccineClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VaccineList(uid));
        }

        private void AllergyClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AllergyList(uid));
        }
        private void accountInfoClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfileInfo(uid));
        }
        private void ConditionClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConditionList(uid));
        }
        private void CalendarClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Calendar(uid));
        }
    }
}