using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConditionList : ContentPage
    {
        int uid;
        User user;
        public ConditionList(int id)
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
                conn.CreateTable<Tables.Conditions>();
                conn.CreateTable<UsersConditions>();
                user = conn.GetWithChildren<User>(uid);
            }
            int result;
            Conditions al;
            List<Conditions> conditions = user.Conditions;
            if (user.Conditions.Count > 0)
            {
                for (int i = 0; i < conditions.Count - 1; i++)
                {
                    for (int j = 0; j < conditions.Count - i - 1; j++)
                    {
                        result = String.Compare(conditions[j].Type, conditions[j + 1].Type);
                        if (result > 0)
                        {
                            al = conditions[j];
                            conditions[j] = conditions[j + 1];
                            conditions[j + 1] = al;
                        }
                    }
                }
            }
            conditionListView.ItemsSource = conditions;
        }

        protected void newConditionClicked(object sender, EventArgs e)
        {
            Tables.Conditions condition = new Tables.Conditions
            {

            };
            condition.Users = new List<User>
            {
                user
            };
            Navigation.PushModalAsync(new ConditionForum(condition));
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Tables.Conditions condition = new Tables.Conditions
            {

            };
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                condition = conn.GetWithChildren<Conditions>(mi.CommandParameter);
                user = conn.GetWithChildren<User>(uid);
                conn.Delete(condition);
                conn.UpdateWithChildren(user);
            }
            OnAppearing();
        }
    }
}