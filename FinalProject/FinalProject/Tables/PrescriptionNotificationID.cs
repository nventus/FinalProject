using SQLite;
using SQLiteNetExtensions.Attributes;
namespace FinalProject
{
    public class PrescriptionNotificationID
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        [ForeignKey(typeof(Prescription))]
        public int pId
        {
            get;
            set;
        }

    }
}
