using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Plugin.LocalNotifications;
using System.Linq;
using SQLiteNetExtensions.Extensions;

namespace FinalProject
{
    class PrescriptionNotifClass
    {
        //Pass this class a Prescription and it will refresh the prescription reminder notifications
        public static void PrescriptionNotifHandler(Prescription p0)
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<PrescriptionNotificationID>();

                int PrescriptionReminderID = Preferences.Get("PrescriptionReminderNotifID", 1000000);

                DateTime today = DateTime.Now;
                DateTime startDay = DateTime.Parse(p0.startDate).Date + p0.reminderTime.TimeOfDay;
                DateTime endDay = DateTime.Parse(p0.endDate).Date + p0.reminderTime.TimeOfDay;
                int numDaysToRemind = (endDay.Date - startDay.Date).Days;
                DateTime reminderDayAndTime = startDay;


                //for testing purposes. remove later
                //int mytempid = 298347234;


                //If today is past or equal to the start day, then we only need to begin reminders today if we are before the reminder timeofday. Otherwise we begin reminders the next day.
                if (today.Date >= startDay.Date)
                {   //Remind the number of days between today and the end date
                    numDaysToRemind = (endDay.Date - DateTime.Now.Date).Days;
                    if (DateTime.Now.TimeOfDay >= reminderDayAndTime.TimeOfDay)
                    {   //We are past the reminder time for today, begin reminding tomorrow.

                        reminderDayAndTime = DateTime.Now.Date.AddDays(1) + reminderDayAndTime.TimeOfDay;
                    }
                    else
                    {   //We are before the reminder time today, begin reminders today
                        reminderDayAndTime = DateTime.Now.Date + reminderDayAndTime.TimeOfDay;
                        numDaysToRemind++;

                    }
                }
                else
                {
                    //the startdate is in the future.
                    numDaysToRemind++;
                }
                if (today > endDay)
                {
                    numDaysToRemind = 0;
                }

                //for testing purposes. remove later
                //CrossLocalNotifications.Current.Show("TempRemind", "Creating " + numDaysToRemind + " Reminders. reminderstart " + reminderDayAndTime.Date, mytempid, DateTime.Now.AddSeconds(15));
                //mytempid++;

                while (numDaysToRemind > 0)
                {

                    PrescriptionReminderID++;

                    //These are in a one to many relationship with Prescription. One prescription will track all unique notification IDs that it
                    //has created so they can be canceled later if the reminder time is changed.
                    PrescriptionNotificationID pnid0 = new PrescriptionNotificationID()
                    {
                        Id = PrescriptionReminderID,
                        pId = p0.Id
                    };
                    conn.Insert(pnid0);

                    if (p0.PrescriptionNotificationIDs == null)
                    {
                        p0.PrescriptionNotificationIDs = new List<PrescriptionNotificationID> { pnid0 };
                    }
                    else
                    {
                        p0.PrescriptionNotificationIDs.Add(pnid0);
                    }

                    CrossLocalNotifications.Current.Show("Prescription Reminder", "This is a reminder to take " + p0.RxName, PrescriptionReminderID, reminderDayAndTime);
                    //track the unique notification IDs used in case we have to cancel the notifications when the user edits the time they want to be reminded at.

                    //for testing purposes. remove later
                    //CrossLocalNotifications.Current.Show("TempRemind", "Created Reminder for day " + reminderDayAndTime, mytempid, DateTime.Now.AddSeconds(10));
                    numDaysToRemind--;
                    reminderDayAndTime = reminderDayAndTime.AddDays(1);
                    //mytempid++;
                    Preferences.Set("PrescriptionReminderNotifID", PrescriptionReminderID);
                    conn.Update(p0);
                }

            }

        }
    }
}
