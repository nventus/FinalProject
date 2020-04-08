using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllergyForums : ContentPage
    {
        Allergy allergy;
        public AllergyForums(Allergy al)
        {
            InitializeComponent();
            allergy = al;
        }
        async void ButtonClicked(object sender, EventArgs e)
        {
            bool exists = false;
            Allergy a = new Allergy
            {
                Type = ""
            };
            allergy.Type = NameEntry.Text;
            try
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Allergy>();
                    var allergies = conn.Query<Allergy>("select * from Allergy where Type=?", allergy.Type);
                    a = conn.GetWithChildren<Allergy>(allergy.Type);
                }
            }
            catch (Exception f)
            {

            }
            if (a.Type.Equals(""))
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Allergy>();
                    conn.CreateTable<User>();
                    conn.Insert(allergy);
                    conn.UpdateWithChildren(allergy);
                }
                await Navigation.PopModalAsync();
            }
            else
            {
                foreach (User u in a.Users)
                {
                    if (u.Id == allergy.Users[0].Id)
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists)
                {
                    await DisplayAlert("Oops!", "You already have this allergy listed", "OK");
                }
                else
                {
                    a.Users.Add(allergy.Users[0]);
                    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<Allergy>();
                        conn.UpdateWithChildren(a);
                    }
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}