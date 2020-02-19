using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Tables
{
    public class Vaccine
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public string VaccineName
        {
            get;
            set;
        }
        public string Date
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

