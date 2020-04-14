using FinalProject.Tables;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace FinalProject
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id
        {
            get;
            set;
        }

        [NotNull]
        public DateTime aptDate
        {
            get;
            set;
        }

        //Need to save the time that the user wants to be reminded of their appointment
        //This will be used to resubmit the notifications after device restart, because they are not saved otherwise.
        public DateTime reminderTime
        {
            get;
            set;
        }
        public string followUpAdvice
        {
            get;
            set;
        }
        public string reasonForVisit
        {
            get;
            set;
        }

        public string dName
        {
            get;
            set;
        }
        public string diagnosis
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
        [OneToMany]
        public List<Vaccine> Vaccines
        {
            get;
            set;
        }
        [OneToMany]
        public List<Prescription> Prescriptions
        {
            get;
            set;
        }
    }
}
