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
    public partial class ScriptList : ContentPage
    {
        int uid;
        public ScriptList(int id)
        {
            InitializeComponent();
            uid = id;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Prescription>();
                var prescriptions = conn.Query<Prescription>("select * from Prescription where uId=?", uid);
                RxListView.ItemsSource = prescriptions;
            }
        }

        private void RxSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Prescription p = e.SelectedItem as Prescription;
            Navigation.PushAsync(new ScriptDetails(p));
        }
    }
}