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
    public partial class OptionList : ContentPage
    {
        int uid;
        public OptionList(int id)
        {
            InitializeComponent();
            uid = id;
        }

        private void LogAptClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DoctorList(uid));
        }

        async void RxClicked(object sender, EventArgs e)
        {

        }
    }
}