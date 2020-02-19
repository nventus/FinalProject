﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScriptDetails : ContentPage
    {
        Prescription script;
        public ScriptDetails(Prescription p)
        {
            InitializeComponent();
            script = p;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ScriptName.Text = script.RxName;
            ScriptDate.Text = script.startDate;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Prescription>();
                var docs = conn.Query<Doctor>("select * from Doctor where Id=?", script.dId);
                DName.Text = docs[0].dName;
            }
            LScriptDate.Text = script.endDate;
        }
    }
}