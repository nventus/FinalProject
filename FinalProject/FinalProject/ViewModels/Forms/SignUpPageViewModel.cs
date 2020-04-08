using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FinalProject.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private string name;



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel()
        {
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.NotifyPropertyChanged();
            }
        }


        #endregion

        #region Command

        public Command LoginCommand { get; set; }


        public Command SignUpCommand { get; set; }

        #endregion

        #region Methods


        private void LoginClicked(object obj)
        {
            // Do something
        }

        private void SignUpClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}