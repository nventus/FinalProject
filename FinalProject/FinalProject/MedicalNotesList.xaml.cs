using FinalProject.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicalNotesList : ContentPage
    {
        User user;
        public MedicalNotesList(User usr)
        {
            InitializeComponent();
            user = usr;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                int result;
                Note note;
                conn.CreateTable<Note>();
                /*
                List<Note> notes = user.Appointments;
                if (appointments.Count > 0)
                {
                    for (int i = 0; i < appointments.Count - 1; i++)
                    {
                        for (int j = 0; j < appointments.Count - i - 1; j++)
                        {
                            result = DateTime.Compare(appointments[j + 1].aptDate, appointments[j].aptDate);
                            if (result > 0)
                            {
                                appt = appointments[j];
                                appointments[j] = appointments[j + 1];
                                appointments[j + 1] = appt;
                            }
                        }
                    }
                }
                appointmentlist.Clear();
            }
            */
            }
        }
    }
}