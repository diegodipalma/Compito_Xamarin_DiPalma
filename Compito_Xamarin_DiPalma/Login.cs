using Android.App;
using Android.Widget;
using Android.OS;

namespace Compito_Xamarin_DiPalma
{
    [Activity(Label = "APP", MainLauncher = true)]
    public class Login : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

