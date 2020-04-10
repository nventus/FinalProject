using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;

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
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();
                //Ignored if the table already exists
                conn.CreateTable<Doctor>();
                conn.CreateTable<UsersDoctors>();
                conn.CreateTable<Vaccine>();
                conn.CreateTable<Allergy>();
                conn.CreateTable<UsersAllergies>();
                conn.CreateTable<Prescription>();
                conn.CreateTable<Tables.Conditions>();
                conn.CreateTable<UsersConditions>();
                User user = conn.GetWithChildren<User>(uid);
                Navigation.PushAsync(new AppointmentList(user));
            }
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
        private void DoctorClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DoctorList(uid));
        }
    }
}