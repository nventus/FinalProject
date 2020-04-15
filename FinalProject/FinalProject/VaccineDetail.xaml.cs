using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
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
            Doctor doctor;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Prescription>();
                doctor = conn.GetWithChildren<Doctor>(vax.dId);
                DName.Text = doctor.dName;
            }
        }
    }
}