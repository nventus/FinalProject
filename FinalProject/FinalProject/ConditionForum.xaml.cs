using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConditionForum : ContentPage
    {
        Conditions condition;
        bool exists = false;
        public ConditionForum(Tables.Conditions cond)
        {
            InitializeComponent();
            condition = cond;
        }
        async void ButtonClicked(object sender, EventArgs e)
        {
            Conditions cond = new Conditions
            {
                Type = ""
            };
            condition.Type = NameEntry.Text;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Conditions>();
                {
                    try
                    {
                        var c = conn.Query<Conditions>("select * from Conditions where Type=?", condition.Type);
                        cond = conn.GetWithChildren<Conditions>(condition.Type);
                    }
                    catch (Exception f)
                    {

                    }
                }
            }
            if (cond.Type.Equals(""))
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Conditions>();
                    conn.CreateTable<User>();
                    conn.Insert(condition);
                    conn.UpdateWithChildren(condition);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                foreach (User u in cond.Users)
                {
                    if (u.Id == condition.Users[0].Id)
                    {
                        exists = true;
                    }
                }
                if (exists)
                {
                    await DisplayAlert("Oops!", "You already have this condition listed", "OK");
                }
                else
                {
                    cond.Users.Add(condition.Users[0]);
                    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<Conditions>();
                        conn.UpdateWithChildren(cond);
                    }
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}