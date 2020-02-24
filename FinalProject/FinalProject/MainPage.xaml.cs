using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinalProject
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            /*
             * Initially opens up a database with all registered users. If the database is empty, then open up the user signup forum
             */
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();
                var users = conn.Table<User>().ToList();
                if (users?.Any() == false)
                {
                    Navigation.PushModalAsync(new NavigationPage(new UserForums()));
                }
                else
                {
                    ListMenu.ItemsSource = users;
                }
            }
        }
        private void UserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            User selectedUser = e.SelectedItem as User;
            Navigation.PushAsync(new OptionList(selectedUser.Id));
        }
    }
}
