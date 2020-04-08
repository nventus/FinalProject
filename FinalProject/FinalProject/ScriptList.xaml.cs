//using FinalProject.Misc;  This was causing an error, so I commented it out - Pat
using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScriptList : ContentPage
    {
        int uid;
        public ScriptList(int id)
        {
            InitializeComponent();
            uid = id;
        }
        protected async override void OnAppearing()
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
                var prescriptions = conn.Query<Prescription>("select * from Prescription where uId=?", uid);

                int result;
                Prescription script;
                if (prescriptions.Count > 0)
                {
                    for (int i = 0; i < prescriptions.Count - 1; i++)
                    {
                        for (int j = 0; j < prescriptions.Count - i - 1; i++)
                        {
                            result = DateTime.Compare(DateTime.Parse(prescriptions[j + 1].endDate), DateTime.Parse(prescriptions[j].endDate));
                            if (result > 0)
                            {
                                script = prescriptions[j];
                                prescriptions[j] = prescriptions[j + 1];
                                prescriptions[j + 1] = script;
                            }
                        }
                    }
                }
                RxListView.ItemsSource = prescriptions;
            }
        }

        private void RxSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Prescription p = e.SelectedItem as Prescription;
            Navigation.PushAsync(new ScriptDetails(p));
        }
        private void OnEdit(object sender, EventArgs e)
        {
            Prescription script;
            var mi = ((MenuItem)sender);
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                script = conn.GetWithChildren<Prescription>(mi.CommandParameter);
            }
            Navigation.PushAsync(new ScriptEdit(script));
        }
    }
}