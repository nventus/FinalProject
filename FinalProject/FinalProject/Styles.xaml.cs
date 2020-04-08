using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views.Forms
{
    /// <summary>
    /// Class helps to reduce repetitive markup, and allows an apps appearance to be more easily changed.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Styles
    {
        public Styles()
        {
            InitializeComponent();
        }
    }
}