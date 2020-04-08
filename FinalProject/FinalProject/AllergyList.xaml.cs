using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllergyList : ContentPage
    {
        User user;
        int uid;
        public AllergyList(int id)
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
                user = conn.GetWithChildren<User>(uid);
            }
            int result;
            Allergy al;
            List<Allergy> allergies = user.Allergies;
            if (user.Allergies.Count > 0)
            {
                for (int i = 0; i < allergies.Count - 1; i++)
                {
                    for (int j = 0; j < allergies.Count - i - 1; j++)
                    {
                        result = String.Compare(allergies[j].Type, allergies[j + 1].Type);
                        if (result > 0)
                        {
                            al = allergies[j];
                            allergies[j] = allergies[j + 1];
                            allergies[j + 1] = al;
                        }
                    }
                }
            }
            allergyListView.ItemsSource = allergies;
        }
    private void newAllergyClicked(object sender, EventArgs e)
        {

            Allergy allergy = new Allergy
            {

            };
            allergy.Users = new List<User>
            {
                user
            };
            Navigation.PushModalAsync(new AllergyForums(allergy));
        }
        private void OnDelete(object sender, EventArgs e)
        {
            Allergy allergy;
            var mi = ((MenuItem)sender);
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                allergy = conn.GetWithChildren<Allergy>(mi.CommandParameter);
                user = conn.GetWithChildren<User>(uid);
                conn.Delete(allergy);
                conn.UpdateWithChildren(user);
            }
            OnAppearing();
        }
    }
}