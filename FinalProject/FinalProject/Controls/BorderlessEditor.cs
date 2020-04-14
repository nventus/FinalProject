using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FinalProject.Controls
{
    [Preserve(AllMembers = true)]
    public class BorderlessEditor : Editor
    {

        public BorderlessEditor()
        {
            this.TextChanged += this.ExtendableEditor_TextChanged;
        }

        #region Methods

        private void ExtendableEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.InvalidateMeasure();
        }
        #endregion
    }
}