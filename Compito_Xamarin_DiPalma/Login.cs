using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Mono.Data.Sqlite;
using Android.Content;

namespace Compito_Xamarin_DiPalma
{
    [Activity(Label = "APP", MainLauncher = true)]
    public class Login : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);
        }

    }
}