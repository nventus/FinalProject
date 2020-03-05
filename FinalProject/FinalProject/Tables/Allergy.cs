using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Tables
{
    public class Allergy
    {
        [PrimaryKey, NotNull]
        public string Type
        {
            get;
            set;
        }

        [ManyToMany(typeof(UsersAllergies))]
        public List<User> Users
        {
            get;
            set;
        }
    }
}
