using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int aId
        {
            get;
            set;
        }

        [NotNull]
        public string aptDate
        {
            get;
            set;
        }

        public string followUpApt
        {
            get;
            set;
        }

        public string followUpAdvice
        {
            get;
            set;
        }

        public string prescriptions
        {
            get;
            set;
        }

        [ForeignKey(typeof(Doctor))]
        public string DName
        {
            get;
            set;
        }

        [ForeignKey(typeof(User))]

        public string uName
        {
            get;
            set;
        }

        [ManyToOne]
        public Doctor Doctor
        {
            get;
            set;
        }
        [ManyToOne]
        public User User
        {
            get;
            set;
        }
    }
}
