using FinalProject.Tables;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VaccineDetail : ContentPage
    {
        Vaccine vax;
        public VaccineDetail(Vaccine v)
        {
            InitializeComponent();
            vax = v;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VaccineName.Text = vax.VaccineName;
            VaccineDate.Text = vax.Date;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Prescription>();
                var docs = conn.Query<Doctor>("select * from Doctor where Id=?", vax.dId);
                DName.Text = docs[0].dName;
            }
        }
    }
}