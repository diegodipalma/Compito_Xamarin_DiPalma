using Android.App;
using Android.Widget;
using Android.OS;
using SQLite;
using System;
namespace Compito_Xamarin_DiPalma
{
    [Activity(Label = "APP", MainLauncher = true)]
    public class Login : Activity
    {
        Button btnLogin, btnRecover;
        TextView txtMail, txtPass;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            #region data binding
            btnLogin = FindViewById<Button>(Resource.Id._login);
            btnRecover = FindViewById<Button>(Resource.Id._recoverPass);
            txtMail = FindViewById<TextView>(Resource.Id._userMail);
            txtPass = FindViewById<TextView>(Resource.Id._userPass);
            #endregion

            btnLogin.Click += BtnLogin_Click;
            btnRecover.Click += BtnRecover_Click;
        }

        private void BtnRecover_Click(object sender, EventArgs e)
        {
            var connString=new SQLiteConnectionString()
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

}

