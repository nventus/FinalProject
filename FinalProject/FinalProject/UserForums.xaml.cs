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
    public partial class UserForums : ContentPage
    {
        public UserForums()
        {
            InitializeComponent();
        }
        async void ButtonClicked(object sender, EventArgs e)
        {
            User user = new User()
            {
                Name = uNameEntry.Text,
                Birthday = bdayPick.Date,
                Email = uEmailEntry.Text
            };

            if (user.Name == "")
            {
            }
            else
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<User>();
                    int rowsAdded = conn.Insert(user);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}