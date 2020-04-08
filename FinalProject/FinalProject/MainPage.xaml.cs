using System.ComponentModel;
using System.Linq;
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
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            User selectedUser = e.SelectedItem as User;
            Detail = new NavigationPage(new OptionList(selectedUser.Id));

            //Navigation.PushModalAsync(new NavigationPage(new OptionList(selectedUser.Id)));

            IsPresented = false;
        }
    }
}
