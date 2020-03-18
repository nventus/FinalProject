﻿using FinalProject.Tables;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Birthday
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
        [ManyToMany(typeof(UsersDoctors))]
        public List<Doctor> Doctors
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
        [OneToMany]
        public List<Vaccine> Vaccines
        {
            get;
            set;
        }
        [ManyToMany(typeof(UsersAllergies))]
        public List<Allergy> Allergies
        {
            get;
            set;
        }
        [ManyToMany(typeof(UsersConditions))]
        public List<Conditions> Conditions
        {
            get;
            set;
        }
    }
}
