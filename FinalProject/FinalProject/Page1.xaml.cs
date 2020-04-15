using FinalProject.Tables;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void newUserClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new UserForums()));
        }
    }
}