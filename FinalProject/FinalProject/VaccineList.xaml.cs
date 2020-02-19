using FinalProject.Tables;
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
                conn.CreateTable<Vaccine>();
                var vaccines = conn.Query<Vaccine>("select * from Vaccine where uId=?", uid);
                VaccineListView.ItemsSource = vaccines;
            }
        }
        private void vaccineSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Vaccine v = e.SelectedItem as Vaccine;
            Navigation.PushAsync(new VaccineDetail(v));
        }
    }
}