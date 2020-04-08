using SQLiteNetExtensions.Attributes;

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
