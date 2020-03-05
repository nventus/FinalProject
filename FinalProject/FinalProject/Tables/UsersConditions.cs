using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Tables
{
    public class UsersConditions
    {
        [ForeignKey(typeof(User))]
        public int uId
        {
            get;
            set;
        }
        [ForeignKey(typeof(Conditions))]
        public string cId
        {
            get;
            set;
        }
    }
}

