using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
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
    public partial class NoteForm : ContentPage
    {
        bool editing = false;
        Note note = new Note();
        public NoteForm(Note n)
        {
            InitializeComponent();
            note = n;
            if(note.Name == null)
            {
                note.Name = "";
            }
            else
            {
                editing = true;
            }
            if(note.Description == null)
            {
                note.Description = "";
            }
            else
            {
                editing = true;
            }
            TitleEntry.Text = note.Name;
            DescriptionEntry.Text = note.Description;
        }
        async void ButtonClicked(object sender, EventArgs e)
        {
            Folders parent;
            User user;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                parent = conn.GetWithChildren<Folders>(note.ParentFolderId);
                user = conn.GetWithChildren<User>(note.uId);
                if (editing)
                {
                    conn.Delete<Note>(note.Id);
                    user.Notes.Remove(note);
                    parent.Notes.Remove(note);
                }
            }
            note.Name = TitleEntry.Text;
            note.Description = DescriptionEntry.Text;
            note.CreationDate = DateTime.Now;

            if(parent.Notes?.Any() == true)
            {
                parent.Notes.Add(note);
            }
            else
            {
                parent.Notes = new List<Note>()
                {
                    note
                };
            }

            if(user.Notes?.Any() == true)
            {
                user.Notes.Add(note);
            }
            else
            {
                user.Notes = new List<Note>()
                {
                    note
                };
            }

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.Insert(note);
                conn.UpdateWithChildren(parent);
                conn.UpdateWithChildren(user);
            }
            await Navigation.PopAsync();
        }
    }
}