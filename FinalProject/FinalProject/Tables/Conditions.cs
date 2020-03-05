using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Tables
{
    public class Conditions
    {
        [PrimaryKey, NotNull]
        public string Type
        {
            get;
            set;
        }

        [ManyToMany(typeof(UsersConditions))]
        public List<User> Users
        {
            get;
            set;
        }
    }
}
