using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Tables
{
    public class NoteFolder
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public DateTime CreationDate
        {
            get;
            set;
        }

        [ForeignKey(typeof(Folders))]
        public int ParentFolderId
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
