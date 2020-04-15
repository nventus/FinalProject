using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
namespace FinalProject
{
    public class Prescription
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public string RxName
        {
            get;
            set;
        }
        public string startDate
        {
            get;
            set;
        }
        public string endDate
        {
            get;
            set;
        }
        public DateTime reminderTime
        {
            get;
            set;
        }
        [OneToMany]
        public List<PrescriptionNotificationID> PrescriptionNotificationIDs
        {
            get;
            set;
        }
        [ForeignKey(typeof(Doctor))]
        public int dId
        {
            get;
            set;
        }
        [ForeignKey(typeof(User))]
        public int uId
        {
            get;
            set;
        }

        [ForeignKey(typeof(Appointment))]
        public int aId
        {
            get;
            set;
        }
    }
}
