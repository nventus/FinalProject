using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FinalProject.Tables
{
    public class Vaccine
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public string VaccineName
        {
            get;
            set;
        }
        public string Date
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
        [ForeignKey(typeof(Appointment))]
        public int aId
        {
            get;
            set;
        }
    }
}

