using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileInfo : ContentPage
    {
        int uid;
        public ProfileInfo(int id)
        {
            InitializeComponent();
            uid = id;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            User user;
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
                user = conn.GetWithChildren<User>(uid);
            }
            string allergies = "";
            string conditions = "";

            if (!(user.Allergies.Count == 0))
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
                    user = conn.GetWithChildren<User>(uid);
                }
                foreach (Allergy a in user.Allergies)
                {
                    allergies = allergies + a.Type + ", ";
                }
                allergies = allergies.Remove(allergies.Length - 2);
            }
            if (!(user.Conditions.Count == 0))
            {
                foreach (Tables.Conditions c in user.Conditions)
                {
                    conditions = conditions + c.Type + ", ";
                }
                conditions = conditions.Remove(conditions.Length - 2);
            }
            Allergies.Text = allergies;
            Name.Text = user.Name;
            Bday.Text = user.Birthday;
            Conditions.Text = conditions;
            Email.Text = user.Email;
        }
    }
}