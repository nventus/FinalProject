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
    public partial class FolderForm : ContentPage
    {
        Folders folder = new Folders();
        public FolderForm(Folders f)
        {
            InitializeComponent();
            folder = f;
        }
        async void ButtonClicked(object sender, EventArgs e)
        {
            folder.Name = TitleEntry.Text;
            folder.CreationDate = DateTime.Now;
            Folders parent;
            User user;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                parent = conn.GetWithChildren<Folders>(folder.ParentFolderId);
                user = conn.GetWithChildren<User>(folder.uId);
            }

            if (parent.ChildrenFolders?.Any() == true)
            {
                parent.ChildrenFolders.Add(folder);
            }
            else
            {
                parent.ChildrenFolders = new List<Folders>()
                {
                    folder
                };
            }

            if (user.Folds?.Any() == true)
            {
                user.Folds.Add(folder);
            }
            else
            {
                user.Folds = new List<Folders>()
                {
                    folder
                };
            }

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.Insert(folder);
                conn.UpdateWithChildren(parent);
                conn.UpdateWithChildren(user);
            }
            await Navigation.PopAsync();
        }
    }
}
