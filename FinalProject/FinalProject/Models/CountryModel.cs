using Xamarin.Forms.Internals;

namespace FinalProject.Models
{

    [Preserve(AllMembers = true)]
    public class CountryModel
    {
        #region Properties

        public string Country { get; set; }

        public string[] States { get; set; }
        public string[] ZipCode { get; set; }



    }

        #endregion
}