using Plugin.LocalNotifications;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    #region Constructor
    public partial class App : Application
    {
        public static string FilePath;
        public App(string filePath)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjMyMTM2QDMxMzgyZTMxMmUzMGpzYzNpT1FVcnp4ZDJwdWhQUDhNdnI0MlNxMTlLTEEyZkRIOStpYkpMTlE9");
            InitializeComponent();

            MainPage = new MainPage();
            FilePath = filePath;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();
                var users = conn.Table<User>().ToList();
                var doctors = conn.Table<Doctor>();

                if (users?.Any() == true)
                {
                    //Have to resubmit notifications after device restart because they are not saved.
                    var appts = conn.Table<Appointment>().ToList();
                    if (appts?.Any() == true)
                    {
                        foreach (var x in appts)
                        {
                            if (x.reminderTime > DateTime.Now)
                            {
                                var doctor = conn.GetWithChildren<Doctor>(x.dId);
                                CrossLocalNotifications.Current.Show("Appointment Reminder", "You have an appointment with Dr. " + doctor.dName + " at " + x.aptDate.ToShortTimeString(), x.Id, x.reminderTime);
                            }
                        }
                    }
                }
                conn.Close();
            }
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
    #endregion
}
