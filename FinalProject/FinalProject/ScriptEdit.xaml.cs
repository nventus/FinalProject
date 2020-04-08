using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScriptEdit : ContentPage
    {
        Prescription script;
        public ScriptEdit(Prescription p)
        {
            InitializeComponent();
            script = p;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RxName.Text = script.RxName;
            RxStart.Text = script.startDate;
            RxEnd.Date = DateTime.Parse(script.endDate);
        }
        async void ButtonClicked(object sender, EventArgs e)
        {
            script.endDate = RxEnd.Date.ToShortDateString();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.Update(script);
            }
            Navigation.PopAsync();
        }
    }
}