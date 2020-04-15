using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Tables
{
    public class Note : NoteFolder
    {
        public string Description
        {
            get;
            set;
        }
    }
}
