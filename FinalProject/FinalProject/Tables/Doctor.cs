﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject
{
    public class Doctor
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        [NotNull]
        public string dName
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
        [ManyToMany(typeof(UsersDoctors))]
        public List<User> Users
        {
            get;
            set;
        }
        [OneToMany]
        public List<Appointment> Appointments
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