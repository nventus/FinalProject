using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FinalProject
{
    public class Prescription
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public string RxName
        {
            get;
            set;
        }
        public string startDate
        {
            get;
            set;
        }
        public string endDate
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
        [ForeignKey(typeof(User))]
        public int uId
        {
            get;
            set;
        }
    }
}
