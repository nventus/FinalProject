using Plugin.LocalNotifications;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

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

                //Each prescription reminder requires a unique ID which we will track and save to settings.
                bool hasPrescriptionReminderKey = Preferences.ContainsKey("PrescriptionReminderNotifID");
                int PrescriptionReminderID = 1000000;
                //If the setting does not exist, create it and set it with a starting value.
                if (!hasPrescriptionReminderKey)
                {
                    Preferences.Set("PrescriptionReminderNotifID", PrescriptionReminderID);
                }
                else
                {
                    PrescriptionReminderID = Preferences.Get("PrescriptionReminderNotifID", 1000000);
                    //If the ID number gets within 10000 of overflowing, we will reset it.
                    if ((PrescriptionReminderID + 10000) >= int.MaxValue)
                    {
                        PrescriptionReminderID = 1000000;

                    }
                }

                if (users?.Any() == true)
                {
                    //Have to resubmit notifications after device restart because they are not saved.
                    var appts = conn.Table<Appointment>().ToList();
                    var prescriptions = conn.Table<Prescription>().ToList();

                    //We need to set up reminders for any prescriptions currently being taken.
                    prescriptions = prescriptions.Where(item => (DateTime.Parse(item.startDate).Date <= DateTime.Now.Date) && (DateTime.Parse(item.endDate).Date >= DateTime.Now.Date)).ToList();
                    if (prescriptions?.Any() == true)
                    {
                        foreach (var prescript in prescriptions)
                        {   //Refresh the reminder notifications for each prescription if it is not entirely past the time that it must be taken.
                            if (DateTime.Parse(prescript.endDate) >= DateTime.Now)
                            {
                                PrescriptionNotifClass.PrescriptionNotifHandler(prescript);
                            }
                        }
                    }

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
