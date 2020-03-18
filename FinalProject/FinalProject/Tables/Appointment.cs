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
    }
}
