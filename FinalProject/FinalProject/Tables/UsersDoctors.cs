using SQLiteNetExtensions.Attributes;

namespace FinalProject
{
    public class UsersDoctors
    {
        [ForeignKey(typeof(User))]
        public int uId
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

    }
}
