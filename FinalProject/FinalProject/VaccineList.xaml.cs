using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VaccineList : ContentPage
    {
        int uid;
        public VaccineList(int id)
        {
            InitializeComponent();
            uid = id;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();
                conn.CreateTable<Allergy>();
                conn.CreateTable<UsersAllergies>();
                conn.CreateTable<Doctor>();
                conn.CreateTable<UsersDoctors>();
                conn.CreateTable<Vaccine>();
                conn.CreateTable<Prescription>();
                conn.CreateTable<Conditions>();
                conn.CreateTable<UsersConditions>();
                var vaccines = conn.Query<Vaccine>("select * from Vaccine where uId=?", uid);
                Vaccine vax;
                int result;
                if (vaccines.Count > 0)
                {
                    for (int i = 0; i < vaccines.Count - 1; i++)
                    {
                        for (int j = 0; i < vaccines.Count - i - 1; i++)
                        {
                            result = DateTime.Compare(DateTime.Parse(vaccines[j + 1].Date), DateTime.Parse(vaccines[j].Date));
                            if (result > 0)
                            {
                                vax = vaccines[j];
                                vaccines[j] = vaccines[j + 1];
                                vaccines[j + 1] = vax;
                            }
                        }
                    }
                }
                VaccineListView.ItemsSource = vaccines;
            }
        }
        private void vaccineSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Vaccine v = e.SelectedItem as Vaccine;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                v = conn.GetWithChildren<Vaccine>(v.Id);
            }
            Navigation.PushAsync(new VaccineDetail(v));
        }
    }
}