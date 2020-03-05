using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Tables
{
    public class UsersAllergies
    {
        [ForeignKey(typeof(User))]
        public int uId
        {
            get;
            set;
        }
        [ForeignKey(typeof(Allergy))]
        public string aId
        {
            get;
            set;
        }
    }
}
