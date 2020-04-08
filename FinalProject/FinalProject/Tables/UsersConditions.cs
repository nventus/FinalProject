using SQLiteNetExtensions.Attributes;

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

