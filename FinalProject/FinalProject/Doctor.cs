using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject
{
    public class Doctor
    {
        [PrimaryKey, Unique]
        public String dName
        {
            get;
            set;
        }

        public string dPractice
        {
            get;
            set;
        }
        public string dType
        {
            get;
            set;
        }
        public string dAddress
        {
            get;
            set;
        }
        [MaxLength(10)]
        public string dPhone
        {
            get;
            set;
        }
        public string dEmail
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
        public User User
        {
            get;
            set;
        }
    }
}
