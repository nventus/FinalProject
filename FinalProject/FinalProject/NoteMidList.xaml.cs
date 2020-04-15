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
    public partial class NoteMidList : ContentPage
    { 
        Folders folder;
        public NoteMidList(Folders f)
        {
            InitializeComponent();
            folder = f;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            noteListView.SelectedItem = null;
            List<NoteFolder> allItems = new List<NoteFolder>();
            List<NoteFolder> temp = new List<NoteFolder>();
            NoteFolder nf;
            int result;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                folder = conn.GetWithChildren<Folders>(folder.Id);
            }
            //Sorts the child folders alphabetically
            if (folder.ChildrenFolders?.Any() == true)
            {
                foreach (NoteFolder n in folder.ChildrenFolders)
                {
                    temp.Add(n);
                }

                for (int i = 0; i < folder.ChildrenFolders.Count - 1; i++)
                {
                    for (int j = 0; j < folder.ChildrenFolders.Count - i - 1; j++)
                    {
                        result = String.Compare(temp[j].Name, temp[j + 1].Name);
                        if (result > 0)
                        {
                            nf = temp[j];
                            temp[j] = temp[j + 1];
                            temp[j + 1] = nf;
                        }
                    }
                }
                foreach (NoteFolder n in temp)
                {
                    allItems.Add(n);
                }
                temp.Clear();
            }

            //Sorts all the files in the directory alphabetically
            if (folder.Notes?.Any() == true)
            {
                foreach (NoteFolder n in folder.Notes)
                {
                    temp.Add(n);
                }
                for (int i = 0; i < folder.Notes.Count - 1; i++)
                {
                    for (int j = 0; j < folder.Notes.Count - i - 1; j++)
                    {
                        result = DateTime.Compare(temp[j].CreationDate, temp[j + 1].CreationDate);
                        if (result < 0)
                        {
                            nf = temp[j];
                            temp[j] = temp[j + 1];
                            temp[j + 1] = nf;
                        }
                    }
                }
            }

            if (temp != null)
            {
                foreach (NoteFolder n in temp)
                {
                    allItems.Add(n);
                }
            }

            //Binds all of the items to the list
            noteListView.ItemsSource = allItems;
        }
        private void newFolderClicked(object sender, EventArgs e)
        {
            Folders f = new Folders()
            {
                ParentFolderId = folder.Id,
                uId = folder.uId,
                ParentFolder = folder
            };

            Navigation.PushAsync(new FolderForm(f));
        }
        private void newNoteClicked(object sender, EventArgs e)
        {

            Note note = new Note()
            {
                ParentFolderId = folder.Id,
                uId = folder.uId
            };

            Navigation.PushAsync(new NoteForm(note));
        }
        private void itemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Folders f = e.SelectedItem as Folders;
            Note n = e.SelectedItem as Note;

            if (f != null)
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    f = conn.GetWithChildren<Folders>(f.Id);
                }
                Navigation.PushAsync(new NoteMidList(f));
            }
            else if(n != null)
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    n = conn.GetWithChildren<Note>(n.Id);
                }
                Navigation.PushAsync(new NoteForm(n));
            }
            else
            {
                return;
            }
        }
    }
}