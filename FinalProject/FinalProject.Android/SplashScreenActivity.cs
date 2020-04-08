using Android.App;
using Android.OS;
using Android.Views;
using System.Threading;

namespace FinalProject.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
              MainLauncher = true,
              NoHistory = true)]
    public class SplashScreenActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)((int)Window.DecorView.SystemUiVisibility ^ (int)SystemUiFlags.LayoutStable ^ (int)SystemUiFlags.LayoutFullscreen);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            base.OnCreate(bundle);
            Thread.Sleep(100);
            this.StartActivity(typeof(MainActivity));
        }
    }
}