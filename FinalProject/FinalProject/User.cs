using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject
{
    public class User
    {
        [PrimaryKey]

        public string Name
        {
            get;
            set;
        }

        public DateTime Birthday
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
    }
}
